using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using VuTienDat_Game2;
using SR4BlackDev.UISystem;
using System.Collections;

namespace VuTienDat
{
    public class DragController_Level_7 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerPos;
        [SerializeField] private List<GameObject> listItem;
        [SerializeField] private GameObject bin;
        [SerializeField] private bool isDragging = false;
        public bool isOpen = false;
        private GameObject itemParent,itemChild;
        private Camera cam;
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;
        private bool isPause;
        public static DragController_Level_7 ins;
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
            isPause = GameManager_Level_7.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0) && Input.touchCount == 1 && !isPause)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    TagGameObject tag = hit.collider.GetComponent<TagGameObject>();
                    if (tag!=null && tag.tagValue == "Bin")
                    {
                        bin.GetComponent<TurnOnOffFan>().OnOff();
                        isOpen = true;
                    }
                    else
                    {
                        itemParent = hit.collider.gameObject;
                        itemChild = itemParent.transform.GetChild(0).gameObject;
                        MouseDown(1.2f);
                        isDragging = true;
                    }
                }
               /* if (hit.collider != null)
                {
                    isDragging = true;
                    itemParent = hit.collider.gameObject;
                    itemChild = itemParent.transform.GetChild(0).gameObject;
                    MouseDown(1.4f);
                }
                if (itemParent!=null)
                {
                    if (!listItem.Contains(itemParent))
                    {
                        listItem.Add(itemParent);
                    }
                }*/
            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                if (itemParent != null)
                {
                    /*RaycastHit2D[] hit = Physics2D.RaycastAll(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                    if (hit.Length > 1)
                    {
                        GameObject gameObject = hit[1].collider.gameObject;
                        Debug.Log(gameObject);
                        itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, gameObject.transform.position.z - 0.001f);

                    }
                    int idItem = itemParent.GetComponent<Item_Level_7>().id;
                    RaycastHit2D[] hitPos = Physics2D.RaycastAll(mousePosition, Vector3.forward, Mathf.Infinity,layerPos);
                    if (hitPos.Length > 0)
                    {
                        for (int i = 0; i < hitPos.Length; i++)
                        {
                            if (hitPos[i].collider.gameObject.GetComponent<ItemPosition>().id == idItem)
                            {
                                SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
                                spriteRe.sortingOrder = listPos[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
                                itemParent.transform.DORotate(Vector3.zero, 0.15f);
                                itemParent.transform.DOScale(1.2f, 0.15f);
                                itemParent.transform.DOMove(hitPos[i].transform.position, 0.2f).OnComplete(() =>
                                {
                                    itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, 0.0001f);
                                    if (listItem.Contains(itemParent))
                                    {
                                        listItem.Remove(itemParent);
                                    }
                                    itemParent = null;
                                    itemChild = null;
                                });
                                
                                break;
                            }
                        }
                    }*/
                    TruePos truePos = itemParent.GetComponent<TruePos>();
                    if (truePos != null)
                    {
                        Debug.Log("Check");
                        if (Vector3.Distance(itemParent.transform.position, truePos.truePos) < truePos.distance)
                        {
                            Debug.Log("Check 222222222222");

                            truePos.Move();
                            itemParent.transform.DOScale(1, 0.15f);
                            listItem.Remove(itemParent);
                        }
                        else
                        {
                            MouseUp();
                        }
                    }
                    else
                    {
                        MouseUp();
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
            if (listItem.Count == 0 && !isPause)
            {
                bin.GetComponent<TurnOnOffFan>().OnOff();
                PopupManager.ShowToast("Win");
                GameManager_Level_7.instance.setIsGamePause(true);
                if (!isShowDone)
                {
                    ShowDone();
                }
            }
        }
        private void MouseDown(float scale)
        {
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            itemParent.transform.DOScale(scale, 0.15f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
        }
        private void MouseUp()
        {
            itemParent.transform.DORotate(itemParent.GetComponent<Item_Level_7>().rotation, 0.15f);
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 5;
        }
        public void RemoveRotten(GameObject gameObject)
        {
            if (listItem.Contains(gameObject))
            {
                listItem.Remove(gameObject);
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
            GameManager_Level_7.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Level_7.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_Level_7.instance.AddButton();
        }
    }
}
