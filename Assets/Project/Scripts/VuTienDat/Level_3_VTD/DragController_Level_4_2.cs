using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VuTienDat
{
    public class DragController_Level_4_2 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerBox;
        [SerializeField] private List<GameObject> listItem, listPos,listTrash;
        [SerializeField] private GameObject listBox,snowman, prefabSnow;
        public int indexSnow;
        public bool isDragging = false;
        private Vector3 lastPos;
        private int indexLayer, indexMisson;
        private GameObject itemParent, itemChild;
        private Camera cam;
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;
        private bool isPause;
        public static DragController_Level_4_2 instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            indexMisson = 1;
            cam = Camera.main;
            btnContinue.onClick.RemoveAllListeners();
            btnContinue.onClick.AddListener(CloseHint);
            StartCoroutine(delay());
            
        }

        private void Update()
        {
            isPause = GameManager_Level_4_2.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0) && Input.touchCount == 1 && !isPause)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    TagGameObject tag = hit.collider.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        if (tag.tagValue == "Snow")
                        {
                            Debug.Log("Snow");
                            isDragging = true;
                            prefabSnow.transform.position = mousePosition;
                            prefabSnow.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(1, 0.2f).SetEase(Ease.OutBack);
                            itemParent = prefabSnow;
                            itemChild = itemParent.transform.GetChild(0).gameObject;
                        }
                        else if (tag.tagValue == "Body")
                        {
                            itemParent = hit.collider.gameObject;
                            itemParent.GetComponent<PlayAnim>().Play();
                        }
                        else
                        {
                            isDragging = true;
                            itemParent = hit.collider.gameObject;
                            itemChild = itemParent.transform.GetChild(0).gameObject;
                            lastPos = itemParent.transform.position;
                            indexLayer = itemChild.GetComponent<SpriteRenderer>().sortingOrder;
                            MouseDown(1.2f);
                            
                        }
                    }
                    
                }
                

            }
            if (Input.GetMouseButtonUp(0))
            {
                if (itemParent != null)
                {
                    MouseUp();
                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        if (tag.tagValue == "Trash")
                        {
                            if (Vector3.Distance(itemParent.transform.position,lastPos) > 1)
                            {
                                float x, y;
                                float moveX = itemParent.transform.position.x - lastPos.x;
                               
                                if (moveX >= 0)
                                {
                                    x = 10;
                                }
                                else
                                {
                                    x = -10;
                                }
                                float moveY = itemParent.transform.position.y - lastPos.y;
                                if (moveY >= 0)
                                {
                                    y = 5;
                                }
                                else
                                {
                                    y = -10;
                                }
                                listTrash.Remove(itemParent);
                                //itemParent.transform.DOMoveX(moveX, 0.35f);
                                itemParent.transform.DOJump(new Vector3(x, y, 0), 0.5f, 1, 1f);
                            }
                            else
                            {
                                itemParent.transform.DOMove(lastPos, 0.2f);
                            }
                        }
                        else if (tag.tagValue == "Snow")
                        {
                            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward,Mathf.Infinity,layerBox);
                            if (hit.collider != null)
                            {
                                TagGameObject tagGameObject = hit.collider.GetComponent<TagGameObject>();
                                if (tagGameObject != null)
                                {
                                    GameObject o = hit.collider.gameObject;
                                    itemParent.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 0.1f).SetEase(Ease.InBack);
                                    Debug.Log("In");

                                    if (tagGameObject.tagValue == "DragHead")
                                    {
                                        Debug.Log("Head");
                                        o.GetComponent<DragBody>().UpIndex();
                                    }
                                    else if (tagGameObject.tagValue == "DragBody")
                                    {
                                        Debug.Log("Body");
                                        o.GetComponent<DragBody>().UpIndex();
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("Out");
                                itemParent.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0, 0.1f).SetEase(Ease.InBack);
                                //itemParent = null;
                            }
                        }
                        else if (tag.tagValue == "Body")
                        {
                            itemParent.GetComponent<PlayAnim>().Stop();
                        }
                        else if (tag.tagValue == "SnowMan")
                        {
                            TruePos truePos = itemParent.GetComponent<TruePos>();
                            if (Vector3.Distance(itemParent.transform.position, truePos.truePos) < truePos.distance)
                            {
                                truePos.Move();
                                for (int i = 0; i < listItem.Count; i++)
                                {
                                    listItem[i].GetComponent<BoxCollider2D>().enabled = true;
                                }
                            }
                            else
                            {
                                itemParent.transform.DOMove(lastPos, 0.25f);
                            }
                        }
                        else if (tag.tagValue == "Item")
                        {
                            TruePos truePos = itemParent.GetComponent<TruePos>();
                            if (Vector3.Distance(itemParent.transform.position, truePos.truePos) < truePos.distance)
                            {
                                truePos.Move();
                                listItem.Remove(itemParent);
                                if (itemParent.name == "Item_2" )
                                {
                                    itemParent.transform.DORotate(new Vector3(0,0,30),0.15f);
                                }
                                else if (itemParent.name == "Item_3")
                                {
                                    itemParent.transform.DORotate(new Vector3(0,0,-15), 0.15f);
                                }
                                SpriteRenderer spVisual = itemChild.GetComponent<SpriteRenderer>();
                                spVisual.sortingOrder = truePos.sortingLayer;
                                spVisual.sprite = itemParent.GetComponent<SpriteTrue>().spTrue;
                            }
                            else
                            {
                                itemParent.transform.DOMove(lastPos, 0.25f);
                            }
                        }
                        else
                        {
                            itemParent.transform.DOMove(lastPos, 0.2f);
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
            CheckListTrash();
            if (indexSnow == 2)
            {
                listBox.SetActive(false);
            }
            if (indexMisson == 2 && listItem.Count == 0)
            {
                indexMisson++;
                GameManager_Level_4_2.instance.setIsGamePause(true);
                ShowDone();
                PopupManager.ShowToast("Win");
            }
        }
        private void MouseDown(float scale)
        {
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            itemParent.transform.DOScale(scale, 0.15f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
            //spriteRe.sprite = itemParent.GetComponent<Item_Level_10>().spItemFold;
        }
        private void MouseUp()
        {
            if (itemParent.GetComponent<RotationController>() != null)
            {
                itemParent.transform.DORotate(itemParent.GetComponent<RotationController>().rotation, 0.15f);
            }
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = indexLayer;
            // spriteRe.sprite = itemParent.GetComponent<Item_Level_10>().spItem;
        }
        private void CheckListTrash()
        {
            if (indexMisson == 1 && listTrash.Count == 0)
            {
                ShowDone();
                indexMisson++;
                listBox.SetActive(true);
                snowman.SetActive(true);
            }
        }
        public void UpIndexSnow()
        {
            indexSnow++;
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
            GameManager_Level_4_2.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Level_4_2.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_Level_4_2.instance.AddButton();
        }
    }
}
