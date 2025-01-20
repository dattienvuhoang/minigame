using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VuTienDat
{
    public class DragController_Level_13 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerPos;
        [SerializeField] private List<GameObject> listItem, listPos;
        [SerializeField] private bool isDragging = false;
        private GameObject itemParent, itemChild;
        private Camera cam;

        private bool isPause;
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;

        public static DragController_Level_13 instance;
        private void Awake()
        {
            instance = this;
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
            isPause = GameManager_Level_13.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0) && Input.touchCount == 1 && !isPause)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    TagGameObject tag = hit.collider.GetComponent<TagGameObject>();
                    if (tag!=null)
                    {
                        //if (hit.collider.CompareTag("Tool_1"))
                        if (tag.tagValue == "Tool_1")
                        {
                            hit.collider.gameObject.transform.DOScale(0.8f, 0.1f).OnComplete(() =>
                            {
                                hit.collider.gameObject.transform.DOScale(1f, 0.1f);
                                ClothesBasketController.instance.PushItem();
                            });
                        }
                        //else if (hit.collider.CompareTag("Tool_2"))
                        else if (tag.tagValue == "Tool_2")
                        {
                            GameObject gameObject = hit.collider.gameObject;
                            gameObject.GetComponent<CabinetController>().Open();
                        }
                    }
                    else
                    {
                        itemParent = hit.collider.gameObject;
                        itemChild = itemParent.transform.GetChild(0).gameObject;
                        if (itemParent.GetComponent<Item_Level_10>().positionItem != null)
                        {
                            Position itemPos = itemParent.GetComponent<Item_Level_10>().positionItem;

                            for (int i = 0; i < itemPos.listGameObject.Count; i++)
                            {
                                if (itemParent == itemPos.listGameObject[i])
                                {
                                    itemParent.GetComponent<Item_Level_10>().positionItem.listGameObject[i] = null;
                                    itemParent.GetComponent<Item_Level_10>().positionItem = null;
                                }
                            }
                        }
                        if (!listItem.Contains(itemParent))
                        {
                            listItem.Add(itemParent);
                        }
                        MouseDown(1.2f);
                        isDragging = true;
                    }
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                if (itemParent != null)
                {
                    //if (!itemParent.CompareTag("Tool_1"))
                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    if (tag == null ||  tag.tagValue != "Tool_1") 
                    {
                        MouseUp();
                        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                        if (hit.Length > 1)
                        {
                            GameObject gameObject = hit[0].collider.gameObject;
                            itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, gameObject.transform.position.z - 0.001f);

                        }

                        RaycastHit2D[] posHit = Physics2D.RaycastAll(mousePosition, Vector3.forward, Mathf.Infinity,layerPos);
                        if (posHit.Length > 0 )
                        {
                            //Debug.Log("Length: " + posHit.Length);

                            for (int j = 0; j <posHit.Length; j++)
                            {
                                Item_Level_10 item = itemParent.GetComponent<Item_Level_10>();
                                int idBox = item.idBox;

                                Position pos = posHit[j].collider.gameObject.GetComponent<Position>();
                                if (idBox == pos.idBoxPos)
                                {

                                    for (int i = 0; i < pos.listGameObject.Count; i++)
                                    {
                                        if (pos.listGameObject[i] == null && Vector3.Distance(itemParent.transform.position, pos.listPosition[i].transform.position) < 1f)
                                        {
                                            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
                                            spriteRe.sortingOrder = pos.listPosition[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
                                            itemParent.transform.DORotate(Vector3.zero, 0.15f);
                                            itemParent.transform.DOScale(1f, 0.15f);
                                            itemParent.transform.DOMove(pos.listPosition[i].transform.position, 0.1f);
                                            pos.listGameObject[i] = itemParent;
                                            if (idBox == 11 || idBox == 12)
                                            {
                                                pos.listGameObject[i].SetActive(false);
                                                GameObject cabinetController = posHit[j].collider.gameObject.GetComponent<OpenCloseCabinet>().cabinet;
                                                cabinetController.GetComponent<CabinetController>().upIndex();
                                                //Debug.Log("Check");
                                            }
                                            if (idBox == 8 || idBox == 9 || idBox  == 10)
                                            {
                                                spriteRe.sprite = itemParent.GetComponent<Item_Level_10>().spItemFold;
                                            }
                                            item.positionItem = pos;
                                            if (listItem.Contains(itemParent))
                                            {
                                                listItem.Remove(itemParent);
                                            }
                                            break;
                                        }
                                    }

                                }
                            }
                            
                        }
                        isDragging = false;
                        itemParent = null;
                    }
                   
                }
            }
            if (isDragging && itemParent != null)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition);
                itemParent.transform.position = new Vector3(newPosition.x, newPosition.y);
            }
            if (listItem.Count == 0)
            {
                if (!isShowDone)
                {
                    Debug.Log("Winnnnnnnnnnnnnn");
                    PopupManager.ShowToast("Win");
                    ShowDone();
                    GameManager_Level_13.instance.setIsGamePause(true);
                }
                

            }
        }
        private void MouseDown(float scale)
        {
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            itemParent.transform.DOScale(scale, 0.15f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
            spriteRe.sprite = itemParent.GetComponent<Item_Level_10>().spItemFold;
        }
        private void MouseUp()
        {
            itemParent.transform.DORotate(itemParent.GetComponent<Item_Level_10>().rotation, 0.15f);
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 5;
            spriteRe.sprite = itemParent.GetComponent<Item_Level_10>().spItem;
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
            GameManager_Level_13.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Level_13.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_Level_13.instance.AddButton();
        }
    }
}
