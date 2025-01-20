using Destructible2D;
using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VuTienDat
{
    public class DragController_7_2 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerPos;
        [SerializeField] private List<GameObject> listItem, listCake, listCup, listTool , listTrash;
        [SerializeField] private List<D2dDestructibleSprite> listDirt;
        [SerializeField] private GameObject bin, item_5, refUp, refBelow, apple ;
        [SerializeField] private bool isDragging = false;
        [SerializeField] private Sprite spMenu;
        [SerializeField] private Collider2D boxRefUp, boxRefBelow;
        public int indexMisson;
        private int sorting;
        private GameObject itemParent, itemChild;
        private Vector3 lastPos;
        private Camera cam;
        private bool isToolMove = false;
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;
        private bool isPause;
        public static DragController_7_2 instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            btnContinue.onClick.RemoveAllListeners();
            btnContinue.onClick.AddListener(CloseHint);
            indexMisson = 0;
            StartCoroutine(delay());
            cam = Camera.main;
        }

        private void Update()
        {
            isPause = GameManager_Level_7_2.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0)  && Input.touchCount == 1 && !isPause)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    TagGameObject tag = hit.collider.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        GameObject o = hit.collider.gameObject;
                        if (tag.tagValue == "Bin")
                        {
                            TurnOnOffFan.Instance.OnOff();
                        }
                        else if (tag.tagValue =="Ref_Up")
                        {
                            o.GetComponent<OpenOrClose>().OnOff();
                            /*item_5.SetActive(false);*/

                        }
                        else if (tag.tagValue == "Ref_Below")
                        {
                            if (!listItem.Contains(apple))
                            {
                                o.GetComponent<OpenOrClose>().OnOff();
                                refUp.GetComponent<OpenOrClose>().OnOff();
                            }
                            

                        }
                        else
                        {
                            isDragging = true;
                            itemParent = hit.collider.gameObject;
                            itemChild = itemParent.transform.GetChild(0).gameObject;
                            lastPos = itemParent.transform.position;
                            if (tag.tagValue == "Tool")
                            {
                                if (itemParent.name != "Towel")
                                {
                                    itemParent.transform.DORotate(new Vector3(0, 0, -20), 0.15f);
                                    itemParent.transform.GetChild(1).gameObject.SetActive(true);
                                }
                                else
                                {
                                    if (listTrash.Count == 0)
                                    {
                                        itemParent.transform.DORotate(new Vector3(0, 0, -20), 0.15f);
                                        itemParent.transform.GetChild(1).gameObject.SetActive(true) ;
                                    }
                                }
                               
                            }
                            sorting = itemChild.GetComponent<SpriteRenderer>().sortingOrder;
                            MouseDown(1.1f);
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
                        if (tag.tagValue == "Tool")
                        {
                            itemParent.transform.DORotate(new Vector3(0, 0, 0), 0.15f);
                            itemParent.transform.GetChild(1).gameObject.SetActive(false);
                            itemParent.transform.DOMove(lastPos,0.15f);     
                        }
                        else 
                        if (tag.tagValue == "Item")
                        {
                            TruePos truePos = itemParent.GetComponent<TruePos>();
                            if (truePos != null)
                            {
                                if (Vector3.Distance(itemParent.transform.position, truePos.truePos) < truePos.distance)
                                {
                                    if (itemParent.name == "Item_5")
                                    {
                           
                                        if (!refUp.GetComponent<OpenOrClose>().isOn)
                                        {
                                            truePos.Move();
                                        }
                                    }
                                    else
                                    {
                                        truePos.Move();
                                    }
                                    truePos.Move();
                                    listItem.Remove(itemParent);
                                    if (listCake.Contains(itemParent))
                                    {
                                        itemParent.transform.SetParent(null);
                                        listCake.Remove(itemParent);
                                    }
                                    else if (listCup.Contains(itemParent))
                                    {
                                        itemParent.transform.SetParent(null);
                                        listCup.Remove(itemParent);
                                    }
                                    if (itemParent.name == "Item_4")
                                    {
                                        itemChild.GetComponent<SpriteRenderer>().sprite = spMenu;
                                        //EnableBoxBin.ins.EnableBox();
                                    }
                                    else if (itemParent.name == "Item_5")
                                    {
                                        item_5.transform.SetParent(refUp.transform);
                                    }
                                   


                                }
                                else
                                {
                                    itemParent.transform.DOMove(lastPos, 0.2f);

                                }
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
            CheckTrash();
            CleanDirt();
            if (indexMisson == 2 && listCake.Count == 2)
            {
                //ShowDone();
                boxRefBelow.enabled = true;
                indexMisson++;
            }
            if (indexMisson == 3 && listCup.Count == 0 && listItem.Count==0)
            {
                indexMisson++;
                ShowDone();
                GameManager_Level_7_2.instance.setIsGamePause(true);
                refBelow.GetComponent<OpenOrClose>().OnOff();
                //refUp.GetComponent<OpenOrClose>().OnOff();
                PopupManager.ShowToast("Win");
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
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            itemParent.transform.DOScale(1, 0.15f);
            itemChild.GetComponent<SpriteRenderer>().sortingOrder = sorting;
           
        }
        public void RemoveTrash(GameObject o)
        {
            if (listTrash.Contains(o))
            {
                listTrash.Remove(o);
            }
        }
        public void CheckTrash()
        {
            if (listTrash.Count == 0 && indexMisson == 0)
            {
                ShowDone();
                indexMisson++;
                TurnOnOffFan.Instance.OnOff();
                bin.transform.DOMoveX(10, 1f);
                listTool[2].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        public void CleanDirt()
        {
            if (listDirt.Count != 0 && indexMisson == 1)
            {
                for (int i = 0; i < listDirt.Count; i++)
                {
                    if (listDirt[i].AlphaRatio < 0.01f)
                    {
                        //listDirt[i].Clear();
                        listDirt[i].gameObject.SetActive(false);
                        listDirt.RemoveAt(i);
                    }
                }
            }
            if (listDirt.Count == 0 && indexMisson == 1)
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
                    listTool[0].transform.DOMoveX(-10, 1);
                    listTool[1].transform.DOMoveX(-10, 1);
                    listTool[2].transform.DOMoveX(-10, 1);
                    boxRefUp.enabled = true;
                    item_5.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
        public void RemoveItem(GameObject o)
        {
            listItem.Remove(o);
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
            GameManager_Level_7_2.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Level_7_2.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_Level_7_2.instance.AddButton();
        }
    }
}
