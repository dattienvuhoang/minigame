using Destructible2D;
using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace VuTienDat
{
    public class DragController_Level_10 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerPos;
        [SerializeField] private List<GameObject> listItem, listQtip;
        [SerializeField] private bool isDragging = false;
        [Header("Clean")]
        [SerializeField] private Sprite spTowelOn;
        [SerializeField] private Sprite spTowelOff;
        [SerializeField] private Vector3 posTowel, lastPos;
        [SerializeField] private GameObject towel, qtip;
        [SerializeField] private GameObject glassStain, sinkStain ;
        public List<D2dDestructibleSprite> listD2D;
        public BoxCollider2D boxDoor;
        
        private GameObject itemParent, itemChild;
        private Camera cam;
        private int idItem, idBox;
        private bool isPause;
        public GameObject emojiLike;
        private bool isShowDone = false, isCloseQtip = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;

        public static DragController_Level_10 ins;
        private void Awake()
        {
            ins = this;

        }
        private void Start()
        {
            cam = Camera.main;
            posTowel = towel.transform.position;
            StartCoroutine(OffSinkStain());
            btnContinue.onClick.RemoveAllListeners();
            btnContinue.onClick.AddListener(CloseHint);
            StartCoroutine(delay());
        }

        private void Update()
        {
            isPause = GameManager_Level_10.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0) && Input.touchCount == 1 && !isPause)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                /*if (hit.collider != null)
                {
                    isDragging = true;
                    TagGameObject tag = hit.collider.GetComponent<TagGameObject>();
                    if (tag != null && tag.tagValue != "Tool_3")
                    {
                        //if (hit.collider.gameObject.CompareTag("Tool_1"))
                        if (tag.tagValue == "Tool_1")
                        {
                            RottenGlass.instance.OnOff();
                            itemParent = null;
                            itemChild = null;
                        }
                        //else if (hit.collider.gameObject.CompareTag("Tool_2"))
                        else if (tag.tagValue =="Tool_2")
                        {
                            WaterTap.instance.Tap();
                            itemParent = null;
                            itemChild = null;
                        }
                        
                    }
                    else
                    {
                        itemParent = hit.collider.gameObject;
                        itemChild = itemParent.transform.GetChild(0).gameObject;
                        Item_Level_10 item = itemParent.GetComponent<Item_Level_10>();
                        idItem = item.idItem;
                        idBox = item.idBox;
                        MouseDown(1.2f);
                    }



                    //Debug.Log("ID Box: " + idBox);
                }*/
                if (hit.collider != null)
                {
                    TagGameObject tag = hit.collider.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        if (tag.tagValue == "Item" || tag.tagValue == "Towel")
                        {
                            isDragging = true;
                            itemParent = hit.collider.gameObject;
                            itemChild = itemParent.transform.GetChild(0).gameObject;
                            lastPos = itemParent.transform.position;
                            MouseDown(1.2f);
                        }
                        else if (tag.tagValue == "Faucet")
                        {
                                WaterTap.instance.Tap();
                        }
                        else if (tag.tagValue == "Door")
                        {
                            RottenGlass.instance.OnOff();
                        }
                    }
                }
                
            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                if (itemParent != null)
                {
                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        if (tag.tagValue == "Item")
                        {
                            TruePos truePos = itemParent.GetComponent<TruePos>();
                            if (truePos != null)
                            {
                                if (Vector3.Distance(itemParent.transform.position, truePos.truePos) < truePos.distance)
                                {
                                    truePos.Move();
                                    listItem.Remove(itemParent);
                                }
                                else
                                {
                                    itemParent.transform.DOMove(lastPos, 0.15f);
                                }
                            }
                            TruePos_2 truePos2 = itemParent.GetComponent<TruePos_2>();
                            if (truePos2 != null)
                            {
                                if (truePos2.Check() && truePos2.isMove)
                                {
                                    truePos2.Move();    
                                    listItem.Remove(itemParent);
                                    if (listQtip.Contains(itemParent))
                                    {
                                        listQtip.Remove(itemParent);
                                    }
                                }
                            }

                        }
                    }

                    itemParent = null;
                    isDragging = false;
                }
                /*if (itemParent != null)
                {
                    MouseUp();
                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        //if (itemParent.CompareTag("Tool_3"))
                        if (tag.tagValue =="Tool_3")
                        {
                            towel.transform.DOMove(posTowel, 0.15f);
                        }
                    }

                    RaycastHit2D[] hitPos = Physics2D.RaycastAll(mousePosition,Vector3.forward,Mathf.Infinity, layerPos);
                    if (hitPos.Length > 0)
                    {
                        //Debug.Log("--------");
                        for (int i = 0; i < hitPos.Length; i++)
                        {
                            GameObject pos = hitPos[i].collider.gameObject;
                            Position position = pos.GetComponent<Position>();
                            int idBoxPos = position.idBoxPos;
                            //Debug.Log("ID Box Pos: " + idBoxPos);
                            if (idBox == idBoxPos)
                            {
                                for (int j = 0; j < position.listPosition.Count;j++)
                                {
                                    int idPos = position.listPosition[j].GetComponent<Position>().idPos;
                                    if (idPos == idItem)
                                    {

                                        SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
                                        spriteRe.sortingOrder = position.listPosition[j].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
                                        itemParent.transform.DORotate(Vector3.zero, 0.15f);
                                        itemParent.transform.DOScale(1f, 0.15f);
                                        itemParent.transform.DOMove(position.listPosition[j].transform.position, 0.15f).OnComplete(() =>
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
                            }
                        }
                    }
                    isDragging = false;
                }*/

            }
            if (isDragging && itemParent != null)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition);
                itemParent.transform.position = new Vector3(newPosition.x, newPosition.y);
            }

            if (listQtip.Count == 0 && isCloseQtip == false)
            {
                isCloseQtip = true;
                qtip.transform.DORotate(Vector3.zero, 0.1f);
            }
            if (listItem.Count == 2 && boxDoor.enabled == false)
            {
                boxDoor.enabled = true; 
                ShowDone(); 
            }
            if (CheckDes())
            {
                if (listItem.Contains(glassStain) && listItem.Contains(sinkStain) )
                {
                    listItem.Remove(glassStain);
                    listItem.Remove(sinkStain);
                }
               
            }
            /*if (CheckDes(sinkStain.transform.GetChild(1).gameObject))
            {
                if (listItem.Contains(sinkStain))
                {
                    listItem.Remove(sinkStain);
                    //ShowDone();
                }
            }*/
            if (listItem.Count == 0 && !isPause)
            {

                Debug.Log("WINNNNNNNNNNNNNNNNNNNNNN");
                PopupManager.ShowToast("Win");
                ShowDone();
                GameManager_Level_10.instance.setIsGamePause(true);
                /*itemParent = null;
                itemChild = null;*/
            }
        }
        private void MouseDown(float scale)
        {
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
           /* TagGameObject tag = itemParent.GetComponent<TagGameObject>();
            if (tag != null)
            {
                //if (itemParent.gameObject.CompareTag("Tool_3"))
                if (tag.tagValue =="Tool_3")
                {
                    spriteRe.sprite = spTowelOn;
                }
                else
                {
                    itemParent.transform.DOScale(scale, 0.15f);
                }
            }*/
                
        }
        private void MouseUp()
        {
            itemParent.transform.DORotate(itemParent.GetComponent<Item_Level_10>().rotation, 0.15f);
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 5;
            
            /*TagGameObject tag = itemParent.GetComponent<TagGameObject>();
            if (tag != null)
            {
                if (tag.tagValue == "Tool_3")
                {
                    spriteRe.sprite = spTowelOff;
                }
               
            }*/
        }
        IEnumerator OffSinkStain()
        {
            yield return new WaitForSeconds(0.05f);
            sinkStain.transform.GetChild(1).GetComponent<D2dDestructibleSprite>().enabled = false;
        }
        private bool CheckDes()
        {
            int index = 0;
            for (int i = 0; i < listD2D.Count; i++)
            {
                if (listD2D[i].AlphaRatio < 0.01f)
                {
                    index++;
                }
            }

            if (index == listD2D.Count)
            {
                return true;
            }

            return false;
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
            GameManager_Level_10.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Level_10.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_Level_10.instance.AddButton();
        }
    }
}
