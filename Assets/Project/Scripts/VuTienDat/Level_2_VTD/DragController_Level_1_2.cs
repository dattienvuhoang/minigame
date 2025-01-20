using Destructible2D;
using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace VuTienDat
{
    public class DragController_Level_1_2 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerPos;
        public GameObject hand, nailBox;
        [SerializeField] private List<GameObject> listItem;
        [SerializeField] private List<Image> imageList;
        [SerializeField] private List<GameObject> listTool;
        [SerializeField] private List<D2dDestructibleSprite> listD2D, listHurt;
        [SerializeField] private List<GameObject> listHandSkin, listGlue;
        public List<GameObject> listNailHead;
        [SerializeField] private List<GameObject> listNail, listNailBox;
        [SerializeField] public bool isDragging = false;
        public GameObject emojiHeart;
        public int indexMisson = 0, heartNumber;
        public GameObject itemParent, itemChild;
        public Vector3 lastPos;
        private Camera cam;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;
        private bool isPause;
        public static DragController_Level_1_2 ins;
        private void Awake()
        {
            ins = this;
        }
        private void Start()
        {
            btnContinue.onClick.RemoveAllListeners();
            btnContinue.onClick.AddListener(CloseHint);
            heartNumber = 5;
            cam = Camera.main;

            //Debug.Log(UIController_level_1_2.instance);
            StartCoroutine(delay());
        }

        private void Update()
        {
            isPause = GameManager_level_1_2.instance.IsGamePause();

            if (Input.GetMouseButtonDown(0) /*&& Input.touchCount == 1*/ && !isPause)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    TagGameObject tag = hit.collider.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        /*if (tag.tagValue == "Tool_1")
                        {
                            hit.collider.transform.DORotate(new Vector3(0, 0, 15), 0.15f);
                        }
                        else*/
                        if (tag.tagValue == "Water")
                        {
                            MoveWater_Bow();
                        }
                        else if (tag.tagValue == "Hand")
                        {
                            MoveHand();
                        }
                        else
                        {
                            isDragging = true;
                            itemParent = hit.collider.gameObject;
                            itemChild = itemParent.transform.GetChild(0).gameObject;
                            lastPos = itemParent.transform.position;
                            MouseDown(1.1f);
                            if (tag.tagValue == "Tool_1")
                            {
                                itemParent.transform.DORotate(new Vector3(0, 0, 15), 0.15f);
                            }
                            else if (tag.tagValue == "Tool_3" || tag.tagValue == "Tool_4")
                            {
                                itemParent.transform.DORotate(new Vector3(0, 0, 0), 0.15f);
                            }
                            else if (tag.tagValue == "Tool_5")
                            {
                                itemParent.transform.DORotate(new Vector3(0, 0, -15), 0.15f);
                            }
                            else if (tag.tagValue == "NailBox")
                            {
                                RotationNail nail = itemParent.GetComponent<RotationNail>();
                                itemParent.transform.DORotate(nail.rotation, 0.15f);
                                itemChild.GetComponent<SpriteRenderer>().sprite = nail.spVisualDone;
                            }
                            else if (tag.tagValue == "Glue")
                            {
                                itemParent.transform.DORotate(new Vector3(0, 0, -15), 0.15f);
                            }
                        }

                    }


                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                if (itemParent != null)
                {
                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    if (tag != null && tag.tagValue == "NailBox" && listGlue.Count == 0)
                    {
                        //Debug.Log("Check");
                        TruePos pos = itemParent.GetComponent<TruePos>();
                        /*Debug.Log(pos);
                        Debug.Log(pos.truePos);
                        Debug.Log(pos.distance);*/

                        {
                            if (Vector3.Distance(itemParent.transform.position, pos.truePos) < pos.distance)
                            {
                                //Debug.Log("Move True Pos");

                                pos.Move();
                                listNailBox.Remove(itemParent);
                            }
                            else
                            {
                                //Debug.Log("Move Last Pos");

                                itemParent.transform.DOMove(lastPos, 0.15f);
                                itemParent = null;
                            }
                        }

                    }
                    else
                    {
                        //Debug.Log("Don't Check");

                        MouseUp();
                        itemParent.transform.DOMove(lastPos, 0.15f);
                        itemParent = null;
                    }
                    isDragging = false;
                }
            }
            if (isDragging && itemParent != null)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition);
                itemParent.transform.position = new Vector3(newPosition.x, newPosition.y);
            }

            if (indexMisson == 0)
            {
                for (int i = 0; i < listD2D.Count; i++)
                {
                    if (listD2D[i].AlphaRatio < 0.1f)
                    {
                        listD2D[i].gameObject.SetActive(false);
                        listD2D.Remove(listD2D[i]);
                    }
                }
                if (listD2D.Count == 0)
                {
                    if (!isShowDone)
                    {
                        isShowDone = true;
                        ShowDone();

                    }
                    if (!isDragging)
                    {
                        isShowDone = false;
                        indexMisson++;
                        listTool[indexMisson - 1].GetComponent<BoxCollider2D>().enabled = false;
                        listTool[indexMisson - 1].transform.DOMoveY(-10, 0.3f).OnComplete(() =>
                    {
                        listTool[indexMisson].transform.DOMoveY(-4f, 0.25f);
                    });
                    }

                }
            }
            if (listHurt.Count != 0)
            {
                for (int i = 0; i < listHurt.Count; i++)
                {
                    if (listHurt[i].AlphaRatio < 0.1f)
                    {
                        //listHurt[i].Clear();
                        listHurt[i].gameObject.SetActive(false);
                        listHurt.Remove(listHurt[i]);
                    }
                }
            }
            if (indexMisson == 3 )
            {
                //ShowDone();

                if (listHurt.Count == 0)
                {
                    if (!isShowDone)
                    {
                        isShowDone = true;
                        ShowDone();

                    }
                    if (!isDragging)
                    {
                        isShowDone = false;
                        indexMisson++;
                        listTool[4].transform.DOMoveY(-30, 0.25f).OnComplete(() =>
                        {
                            listTool[5].transform.DOMoveY(-2f, 0.25f);
                        });
                    }

                }
            }
            if (indexMisson == 4 && listGlue.Count == 0)
            {
                ShowDone();
                indexMisson++;
                for (int i = 0; i < listNailBox.Count; i++)
                {
                    listNailBox[i].GetComponent<TruePos>().enabled = true;
                }
            }
            if (indexMisson == 5 && listNailBox.Count == 0)
            {
                ShowDone();

                indexMisson++;
                PopupManager.ShowToast("Win");
                GameManager_level_1_2.instance.setIsGamePause(true);
            }
        }
        private void MouseDown(float scale)
        {
            itemParent.transform.DOScale(scale, 0.15f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
        }
        private void MouseUp()
        {
            RotationController rotationController = itemParent.GetComponent<RotationController>();
            if (rotationController != null)
            {
                itemParent.transform.DORotate(rotationController.rotation, 0.15f);

            }
            else
                itemParent.transform.DORotate(Vector3.zero, 0.15f);
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 5;
        }
        private void MoveWater_Bow()
        {
            listTool[1].transform.DOMoveY(2f, 0.4f);
            hand.transform.DOMove(new Vector3(0f, 2.2f, 0), 0.4f).OnComplete(() =>
            {
                hand.GetComponent<BoxCollider2D>().enabled = true;
            });
        }
        private void MoveHand()
        {
            hand.GetComponent<BoxCollider2D>().enabled = false;
            hand.transform.DOMove(new Vector3(0f, 0.3f, 0), 0.4f).OnComplete(() =>
            {
                listTool[1].transform.DOMoveY(-10f, 0.4f).SetDelay(2f);
                hand.transform.DOMove(new Vector3(0f, 2, 0), 0.4f).SetDelay(2f).OnComplete(() =>
                {
                    hand.transform.DOMove(new Vector3(0f, 0, 0), 0.4f);
                    listTool[2].transform.DOMoveY(-3, 1f);
                });

            });
        }
        public void RemoveHandSkin(GameObject o)
        {
            if (listHandSkin.Contains(o))
            {
                listHandSkin.Remove(o);
            }
        }
        public void Move_Clipper()
        {
            if (indexMisson == 1 && listHandSkin.Count == 0)
            {
                indexMisson++;
                listTool[2].transform.DOMoveY(-10, 0.25f).OnComplete(() =>
                {
                    listTool[3].transform.DOMoveY(-2f, 0.25f);
                });
            }
        }
        public void RemoveNailHead(GameObject o)
        {
            if (listNailHead.Contains(o))
            {
                listNailHead.Remove(o);
            }
        }
        public void MoveTool(int i, int j)
        {
            {
                Debug.Log(listTool[i].name + "     " + listTool[j].name);
                listTool[i].transform.DOMoveY(-10, 0.25f).OnComplete(() =>
                {
                    listTool[j].transform.DOMoveY(-2f, 0.25f);
                });
            }

        }
        public void AddD2DHurt(D2dDestructibleSprite hurt)
        {
            if (!listHurt.Contains(hurt))
            {
                DecreasedHeart();
                listHurt.Add(hurt);
            }
            else
            {
                DecreasedHeart();
            }
        }
        public void RemoveNail(GameObject o)
        {
            if (listNail.Contains(o))
            {
                listNail.Remove(o);
            }
            if (listNail.Count == 0)
            {
                listTool[5].transform.DOMoveY(-10, 0.2f).OnComplete(() =>
                {
                    nailBox.transform.DOMoveY(0f, 0.2f);
                });
            }
        }
        public void RemoveGlue(GameObject o)
        {
            if (listGlue.Contains(o))
            {
                listGlue.Remove(o);
            }
        }
        public void DecreasedHeart()
        {
            heartNumber--;
            if (heartNumber == 0)
            {
                PopupManager.ShowToast("LOSE");
                GameManager_level_1_2.instance.setIsGamePause(true);
                imageList[heartNumber].enabled = false;
            }
            else
                imageList[heartNumber].enabled = false;
        }
        public void ShowDone()
        {
            emojiHeart.transform.DOScale(1, 0.2f).From(0).SetEase(Ease.OutBack).OnComplete(() =>
            {
                emojiHeart.transform.DOScale(0, 0.2f).From(1).SetEase(Ease.InBack).SetDelay(1f);

            });
            emojiHeart.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(1, 0.2f).From(0).SetEase(Ease.OutBack).OnComplete(() =>
            {
                emojiHeart.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 0.2f).From(1).SetEase(Ease.InBack).SetDelay(1f);
            });
        }
        public void OpenHint()
        {
            hint.SetActive(true);
            GameManager_level_1_2.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_level_1_2.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
       
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_level_1_2.instance.AddButton();
        }
    }
}
