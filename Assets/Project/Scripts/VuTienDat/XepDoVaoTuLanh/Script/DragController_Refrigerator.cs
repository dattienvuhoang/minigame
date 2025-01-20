using Destructible2D;
using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using VuTienDat_Game2;
namespace VuTienDat
{
    public class DragController_Refrigerator : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerPos;
        [SerializeField] private LayerMask layerDrawer;
        [SerializeField] private List<GameObject> listItem, listDoor;
        [SerializeField] private bool isDragging = false;
        public D2dDestructibleSprite spDirtyD2D;
        public GameObject doorOpen, doorClose, item;
        private GameObject itemParent, itemChild;
        private Vector3 lastPos;
        private Camera cam;

        private bool isPause;
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;

        public static DragController_Refrigerator instance;
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
            isPause = GameManager_Ref.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0) && !isPause )
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    isDragging = true;
                    
                    TagGameObject tag = hit.collider.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        if (tag.tagValue == "Drawer")
                        {
                            hit.collider.GetComponent<DrawerController>().CheckOpen();
                        }
                        else if (tag.tagValue == "Door")
                        {
                            doorOpen.SetActive(false);
                            item.SetActive(false);
                            doorClose.SetActive(true);
                        }
                       
                    }
                    else
                    {
                        itemChild = hit.collider.gameObject;
                        itemParent = itemChild.transform.parent.gameObject;
                        if (itemParent.GetComponent<TagGameObject>() != null)
                        {
                            if (itemParent.GetComponent<TagGameObject>().tagValue == "Towel")
                            {
                                itemChild.GetComponent<SpriteRenderer>().sprite = itemParent.GetComponent<ChangeSprite>().spriteFold;
                                lastPos = itemParent.transform.position;
                            }
                        }
                        MouseDown(1.2f);
                    }


                   
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                if (itemParent != null)
                {
                    // Check Raycast
                   
                    RaycastHit2D []hitItemAll = Physics2D.RaycastAll(mousePosition, Vector3.forward, Mathf.Infinity,layerItem);
                    if (hitItemAll.Length > 1)
                    {
                        float z = 0;
                        for (int i = 0; i < hitItemAll.Length; i++)
                        {
                            //Debug.Log(i + "   " + hitItemAll[i].collider.name);
                            if (z > hitItemAll[i].transform.position.z)
                            {
                                z = hitItemAll[i].transform.position.z;
                            }
                        }
                        //Debug.Log(hitItemAll[1].collider.name);
                        itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, z - 0.001f);


                        // Check cua tu dang mo hay khong 
                       
                        /* for (int i = 0; i < hitItemAll.Length; i++)
                         {
                             TagGameObject tag = hitItemAll[i].collider.GetComponent<TagGameObject>();
                             if (tag != null)
                             {
                                 if (tag.tagValue == "Drawer")
                                 {
                                     if (hitItemAll[i].collider.GetComponent<DrawerController>().isOpen)
                                     {
                                         Debug.Log("Opening");
                                     }
                                     else
                                     {
                                         Debug.Log("Closing");
                                     }
                                 }
                             }

                         }*/
                    }
                    else
                    {
                        itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, 0);
                    }

                    bool isMove = false;

                    if (itemParent.GetComponent<TagGameObject>() != null)
                    {
                        TagGameObject tagObject = itemParent.GetComponent<TagGameObject>();
                        if ( tagObject.tagValue == "InsideItem")
                        {
                            Debug.Log("Inside");
                            RaycastHit2D hitDrawer = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerDrawer);
                            if (hitDrawer.collider != null)
                            {
                                Debug.Log("Raycasttt");
                                TagGameObject tag = hitDrawer.collider.GetComponent<TagGameObject>();
                                if (tag != null)
                                {
                                    if (tag.tagValue == "Drawer")
                                    {
                                        if (hitDrawer.collider.GetComponent<CheckOpen>().isOpen)
                                        {
                                            Debug.Log("Opening");

                                            if (itemParent.GetComponent<TruePos_2>() != null)
                                            {
                                                TruePos_2 truePos = itemParent.GetComponent<TruePos_2>();
                                                Debug.Log(truePos);

                                                if (truePos.Check() && truePos.isMove)
                                                {
                                                    truePos.Move();
                                                    isMove = true;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            Debug.Log("Closing");
                                        }
                                    }

                                }
                            }
                        }
                        else if (tagObject.tagValue == "OutsideItem")
                        {
                            Debug.Log("Outside");
                            if (itemParent.GetComponent<TruePos_2>() != null)
                            {
                                TruePos_2 truePos = itemParent.GetComponent<TruePos_2>();
                                Debug.Log(truePos);

                                if (truePos.Check() && truePos.isMove)
                                {
                                    truePos.Move();
                                    isMove = true;
                                }
                            }
                        }
                    }

                    if (!isMove)
                    {
                        if (itemParent.GetComponent<TagGameObject>() != null)
                        {
                            if (itemParent.GetComponent<TagGameObject>().tagValue == "Towel")
                            {
                                itemChild.GetComponent<SpriteRenderer>().sprite = itemParent.GetComponent<ChangeSprite>().sprite;
                                itemParent.transform.DOMove(lastPos, 0.3f);
                            }
                        }
                        MouseUp();
                    }
                    else
                    {
                        listItem.Remove(itemParent);
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
            if (listItem.Count == 0  && listDoor.Count == 0)
            {
                if (doorOpen.GetComponent<BoxCollider2D>().enabled == false)
                {
                    doorOpen.GetComponent<BoxCollider2D>().enabled = true;
                    ShowDone();
                }
                
            }
            if (doorClose.active == true)
            {
                if (listItem.Count == 0 && spDirtyD2D.AlphaRatio < 0.01f && !isPause)
                {
                    if (!isShowDone)
                    {
                        ShowDone();
                        isShowDone = true;
                    }
                    if (!isDragging)
                    {
                        PopupManager.ShowToast("Win");
                        GameManager_Ref.instance.setIsGamePause(true);
                    }
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
            itemParent.transform.DOScale(1, 0.15f);
            Item_Refrigerator item = itemParent.GetComponent<Item_Refrigerator>();
            if (item != null)
            {
                itemParent.transform.DORotate(item.rotation, 0.15f);
            }
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 4;
        }
        public void CheckItem(GameObject itemCheck)
        {
            if (listItem.Contains(itemCheck))
            {
                listItem.Remove(itemCheck);
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
            GameManager_Ref.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Ref.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_TuLanh.instance.AddButton();
        }
        public void AddDoor(GameObject o)
        {
            if (!listDoor.Contains(o))
            {
                listDoor.Add(o);
            }
        }
        public void RemoveDoor(GameObject o)
        {
            if (listDoor.Contains(o))
            {
                listDoor.Remove(o);
            }
        }
    }
}
