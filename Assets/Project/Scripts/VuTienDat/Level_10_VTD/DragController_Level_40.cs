using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Destructible2D;
using VuTienDat_Game2;
using SR4BlackDev.UISystem;
namespace VuTienDat
{
    public class DragController_Level_40 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private List<GameObject> listItem, listTool;
        [SerializeField] private bool isDragging = false;
        [SerializeField] private BoxCollider2D boxButton;
        [SerializeField] private SpriteRenderer spLipstickCook, spEye;
        [SerializeField] private GameObject glass_1, glass_2, lipDone;
        public GameObject itemParent, itemChild;
        private Vector3 lastPos;
        private Camera cam;
        private int indexLip = 0;
        [Header("D2D")]
        [SerializeField] private List<D2dDestructibleSprite> listD2D;

        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;
        private bool isPause;
        public static DragController_Level_40 ins;
        private void Awake()
        {
            ins = this;
        }
        private void Start()
        {
            btnContinue.onClick.RemoveAllListeners();
            btnContinue.onClick.AddListener(CloseHint);
            StartCoroutine(delay());
            cam = Camera.main;
        }

        private void Update()
        {
            isPause = GameManager_Level_40.instance.IsGamePause();  
            if (Input.GetMouseButtonDown(0) && !isPause && Input.touchCount == 1)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    isDragging = true;
                    
                    TagGameObject tag = hit.collider.gameObject.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        
                        if (tag.tagValue == "Button")
                        {
                            isShowDone = false;
                            TurnOnOffFan.Instance.OnOff();
                            for (int i = 0; i < listItem.Count;i++)
                            {
                                listItem[i].transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 3f);
                            }
                            spLipstickCook.DOFade(1, 3.5f).OnComplete(() =>
                            {
                                TurnOnOffFan.Instance.OnOff();
                                glass_1.layer = 6;
                                glass_2.GetComponent<BoxCollider2D>().enabled = true;
                                if (!isShowDone)
                                {
                                    ShowDone();
                                }
                            });
                        }
                        else if (tag.tagValue == "Open")                        
                        {
                            OpenDoor.ins.OpenOrClose();
                        }
                        else
                        {
                            itemParent = hit.collider.gameObject;
                            if (itemParent.transform.childCount > 0)
                            {
                                itemChild = itemParent.transform.GetChild(0).gameObject;
                            }
                            lastPos = itemParent.transform.position;
                            if (tag.tagValue == "Tool_1")
                            {
                                itemParent.transform.DORotate(new Vector3(0, 0, 135), 0.25f);
                            }
                            else if (tag.tagValue =="LipDone")
                            {
                                itemParent.transform.DORotate(new Vector3(0, 0, 0), 0.25f);
                                itemParent.GetComponent<SpriteRenderer>().sortingOrder = 5;
                            }
                            if (tag.tagValue != "LipDone")
                                MouseDown(1.5f);

                        }
                    }
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                if (itemParent != null)
                {
                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    if (tag.tagValue != "LipDone")
                        MouseUp();

                    if (tag != null)
                    {
                        if (tag.tagValue == "Glass_2")
                        {
                            TruePos truePos = itemParent.GetComponent<TruePos>();
                            if (OpenDoor.ins.isOpen && Vector3.Distance(itemParent.transform.position, truePos.truePos) < truePos.distance)
                            {
                                truePos.Move();
                                itemParent.transform.DOScale(0.8f, 0.15f).OnComplete(() =>
                                {
                                    OpenDoor.ins.OpenOrClose();
                                    ChangeSprite();
                                    StartCoroutine(Open());
                                });
                            }
                            else
                            {
                                itemParent.transform.DOMove(lastPos, 0.25f);
                            }
                        }
                        else if (tag.tagValue == "LipDone")
                        {
                            TruePos truePos = itemParent.GetComponent<TruePos>();
                            if (Vector3.Distance(itemParent.transform.position, truePos.truePos) < truePos.distance)
                            {
                                truePos.Move();
                                StartCoroutine(ShowEye());
                            }
                            else
                            {
                                itemParent.transform.DOMove(lastPos, 0.25f);
                                itemParent.transform.DORotate(new Vector3(0, 0, 180), 0.25f);
                            }
                        }
                        else
                        {
                            itemParent.transform.DOMove(lastPos, 0.25f);
                        }
                    }

                    isDragging = false;
                    itemParent = null;
                }
            }
            if (isDragging && itemParent != null)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition);
                itemParent.transform.position = new Vector3(newPosition.x, newPosition.y);
            }
            if (indexLip == 3)
            {
                boxButton.enabled = true;
                indexLip++;
            }
            if (listTool[3].GetComponent<Cleanner>().enabled == false && listD2D[0].AlphaRatio < 0.001f)
            {
                listTool[3].GetComponent<Cleanner>().enabled = true;
            }
            for (int i = 0; i < listD2D.Count; i++)
            {
                if (listD2D[i].AlphaRatio < 0.001f)
                {
                    listD2D.Remove(listD2D[i]);
                }
            }
            if (listD2D.Count == 0)
            {
                glass_2.transform.GetChild(1).gameObject.layer = 6;
                if (!isShowDone)
                {
                    ShowDone();
                    isShowDone = true;
                }
                //lipDone.GetComponent<BoxCollider2D>().enabled = true;
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
            itemParent.transform.DOScale(1, 0.15f);
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 5;
        }
        public void EnableBox()
        {
            if (indexLip < listItem.Count)
            {
                indexLip++;
                Debug.Log("Index : " + indexLip);
                if (indexLip == 2)
                {
                    listTool[0].transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
                    listTool[1].transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
                }
                if (indexLip < 3)
                    listItem[indexLip].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        IEnumerator Open()
        {
            yield return new WaitForSeconds(3f);
            OpenDoor.ins.OpenOrClose();
            glass_2.transform.DOScale(1, 0.3f).SetDelay(0.3f);
            glass_2.transform.DOMove(new Vector3(1.584f, -1.2f, 0), 0.3f).SetDelay(0.3f).OnComplete(() =>
            {
                OpenDoor.ins.OpenOrClose();
                lipDone.GetComponent<BoxCollider2D>().enabled = true;
            });
        }
        public void ChangeSprite()
        {
            glass_2.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(1, 2f);
            glass_2.transform.GetChild(1).GetComponent<SpriteRenderer>().DOFade(1, 2f);
            glass_2.transform.GetChild(2).GetComponent<SpriteRenderer>().DOFade(0, 2f);
        }
        IEnumerator ShowEye()
        {
            yield return new WaitForSeconds(1f);
            spEye.DOFade(1, 2f).OnComplete(() =>
            {
                PopupManager.ShowToast("Win");
                ShowDone();
                GameManager_Level_40.instance.setIsGamePause(true);
            });
        }
        public void ShowDone()
        {
            emojiLike.transform.DOScale(1, 0.2f).From(0).SetEase(Ease.OutBack).OnComplete(() =>
            {
                emojiLike.transform.DOScale(0, 0.2f).From(1).SetEase(Ease.InBack).SetDelay(1f);

            });
            emojiLike.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(1, 0.2f).From(0).SetEase(Ease.OutBack).OnComplete(() =>
            {
                emojiLike.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 0.2f).From(1).SetEase(Ease.InBack).SetDelay(1f);
            });
        }
        public void OpenHint()
        {
            hint.SetActive(true);
            GameManager_Level_40.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Level_40.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_Level_40.instance.AddButton();
        }
    }
}
