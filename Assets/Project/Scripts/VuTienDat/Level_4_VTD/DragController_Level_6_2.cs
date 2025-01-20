using Destructible2D;
using DG.Tweening;
using dinhvt;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VuTienDat
{
    public class DragController_Level_6_2 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private List<GameObject> listItem, listPos;
        //public List<Vector3> rotationList;
        [SerializeField] private List<GameObject> listLeaf, listTool;
       // [SerializeField] private Sprite spLed, spRybon;
        [SerializeField] private GameObject box;
        [SerializeField] private D2dDestructibleSprite d2dSnow;
        [SerializeField] private bool isDragging = false;
       // [SerializeField] private Animator ledAnim;
        [SerializeField] private GameObject items_Hanger;
        public int indexMisson = 1;
        private Vector3 lastPos;
        private GameObject itemParent, itemChild;
        private int sortingLayer;
        private Camera cam;
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;
        private bool isPause;
        public static DragController_Level_6_2 ins;
        private void Awake()
        {
            ins = this;
        }
        private void Start()
        {
            cam = Camera.main;
            btnContinue.onClick.RemoveAllListeners();
            btnContinue.onClick.AddListener(CloseHint);
            StartCoroutine(delay());
        }

        private void Update()
        {
            isPause = GameManager_Level_6_2.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0) && Input.touchCount == 1 && !isPause)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    isDragging = true;

                    TagGameObject tag = hit.collider.gameObject.GetComponent<TagGameObject>();
                    if (tag!=null)
                    {
                        if (tag.tagValue == "Leaf")
                        {
                            hit.collider.transform.DOMoveX(-7, 0.5f);
                            listLeaf.Remove(hit.collider.gameObject);
                        }
                        else if (tag.tagValue == "Box")
                        {
                            ScaleBox();
                            SeclectBox();
                        }
                        else
                        {
                            itemParent = hit.collider.gameObject;
                            itemChild = itemParent.transform.GetChild(0).gameObject;
                            sortingLayer = itemChild.GetComponent<SpriteRenderer>().sortingOrder;
                            lastPos = itemParent.transform.position;
                            MouseDown(1.1f);
                            if (tag.tagValue == "Tool_1")
                            {
                                itemParent.transform.DORotate(new Vector3(0, 0, -150), 0.15f);
                            }
                        }
                    }
                   
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                if (itemParent != null)
                {
                    MouseUp();
                    TagGameObject tag = itemParent.gameObject.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        if (tag.tagValue == "Item")
                        {
                            /*TruePos truePos = itemParent.GetComponent<TruePos>();
                            if (Vector3.Distance(itemParent.transform.position,truePos.truePos) < truePos.distance)
                            {
                                truePos.Move();
                               *//* if (itemParent.name == "Item_1")
                                {
                                    itemChild.GetComponent<SpriteRenderer>().sprite = spLed; 
                                }
                                else if (itemParent.name == "Item_2")
                                {
                                    itemChild.GetComponent<SpriteRenderer>().sprite = spRybon;
                                }*//*
                                listPos.Remove(itemParent);
                            }
                            else
                            {
                                itemParent.transform.DOMove(lastPos, 0.25f);
                            }*/
                            MoveTruePos truePos = itemParent.GetComponent<MoveTruePos>();
                            if (truePos != null)
                            {
                                if (truePos.Check() && truePos.isMove)
                                {
                                    truePos.Move();
                                    listPos.Remove(itemParent);
                                    if (itemParent.name == "Item_1")
                                    {
                                        items_Hanger.SetActive(true);
                                    }
                                }
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

            if (indexMisson == 1 && d2dSnow.AlphaRatio < 0.001f)
            {
                indexMisson++;
                ShowDone();
                d2dSnow.gameObject.SetActive(false);
                Debug.Log("Clear");
                for (int i = 0; i < listLeaf.Count; i++)
                {
                    listLeaf[i].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            if (indexMisson == 2 && listLeaf.Count == 0)
            {
                indexMisson++;
                ShowDone();
                for (int i = 0; i < listTool.Count; i++)
                {
                    listTool[i].transform.DOMoveX(-10, 1f);
                }
                box.transform.DOMoveY(-3.2f, 0.5f).SetDelay(1f);
            }
            if (indexMisson == 3 && !isPause && listPos.Count == 0)
            {
                ShowDone();
                PopupManager.ShowToast("Win");
                GameManager_Level_6_2.instance.setIsGamePause(true);
                indexMisson++;
            }
        }
        private void MouseDown(float scale)
        {
            itemChild.GetComponent<SpriteRenderer>().sortingOrder = 10;
            itemParent.transform.DOScale(scale, 0.15f);
        }
        private void MouseUp()
        {
            itemChild.GetComponent<SpriteRenderer>().sortingOrder = sortingLayer;
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            itemParent.transform.DOScale(1, 0.15f);
           
        }
        public void ScaleBox()
        {
            box.transform.DOScaleY(0.9f, 0.15f).SetEase(Ease.InBack). OnComplete(() =>
            {
                box.transform.DOScaleY(1.1f, 0.15f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    box.transform.DOScaleY(1f, 0.15f);
                });
            });
        }  
        public void SeclectBox()
        {
            int i = 0;
            if (indexMisson == 3 && listItem.Count>0)
            {
                if (listItem.Count > 17)
                {
                    box.GetComponent<BoxCollider2D>().enabled = false;
                    i = 0;
                    SpriteRenderer spItem = listItem[i].transform.GetChild(0).GetComponent<SpriteRenderer>();
                    spItem.DOFade(1, 0.2f);
                    listItem[i].transform.DOMoveY(0.8f, 0.2f).SetDelay(0.15f).OnComplete(() =>
                    {
                        listItem[i].GetComponent<BoxCollider2D>().enabled = true;
                        listItem.Remove(listItem[i]);
                        box.GetComponent<BoxCollider2D>().enabled = true;
                    });
                }
                else if (listPos.Count <= 17)
                {
                    box.GetComponent<BoxCollider2D>().enabled = false;
                    i = Random.Range(0, listItem.Count);
                    SpriteRenderer spItem = listItem[i].transform.GetChild(0).GetComponent<SpriteRenderer>();
                    spItem.DOFade(1, 0.2f);
                    listItem[i].transform.DOMoveY(0.8f, 0.2f).SetDelay(0.15f).OnComplete(() =>
                    {
                        listItem[i].GetComponent<BoxCollider2D>().enabled = true;
                        //listItem[i].GetComponent<SpriteRenderer>().sortingOrder = 
                        listItem.Remove(listItem[i]);
                        box.GetComponent<BoxCollider2D>().enabled = true;
                    });
                }
                
            }
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
            GameManager_Level_6_2.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Level_6_2.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_Level_6_2.instance.AddButton();
        }
    }
}
