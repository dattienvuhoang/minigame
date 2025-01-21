using Destructible2D;
using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VuTienDat
{
    public class DragController_Level_31 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerTray, layerPos;
        [Header("Wave_1")]
        [SerializeField] private GameObject wave_1;
        [SerializeField] private List<GameObject> listTool;
        [SerializeField] private List<GameObject> listMiss_1;
        [SerializeField] private List<D2dDestructibleSprite> listD2D;
        [Header("Wave_2")]
        [SerializeField] private GameObject wave_2;
        [SerializeField] private List<GameObject> listFood;
        [SerializeField] private List<GameObject> listFoodSucces;
        [SerializeField] SpriteRenderer spCooking;
        [SerializeField] private bool isDragging = false;
        [SerializeField] private int indexMisson = -1;
        [SerializeField] private bool isWin = false;
        private GameObject itemParent, itemChild;
        private Vector3 lastPos;
        private Camera cam;

        private bool isPause;
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;

        public static DragController_Level_31 instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            btnContinue.onClick.RemoveAllListeners();
            btnContinue.onClick.AddListener(CloseHint);
            StartCoroutine(delay());
            cam = Camera.main;
            for (int i = 0; i < listD2D.Count; i++)
            {
                listD2D[i].GetComponent<D2dDestructibleSprite>().Rebuild();
            }
            StartCoroutine(setOffD2D());
            indexMisson = -1;
        }

        private void Update()
        {
            isPause = GameManager_Level_31.instance.IsGamePause();
            // Check commit
            if (Input.GetMouseButtonDown(0) && !isPause && Input.touchCount == 1)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    isDragging = true;
                    itemParent = hit.collider.gameObject;
                    itemChild = itemParent.transform.GetChild(0).gameObject;
                    lastPos = itemParent.transform.position;
                    MouseDown(1.2f);
                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();

                    //if (itemParent.tag != "Trash")
                    if (tag == null || tag.tagValue != "Trash")
                    {
                        lastPos = itemParent.transform.position;
                        if (indexMisson >= 0 && indexMisson < 10)
                        {
                            if (itemParent == listTool[indexMisson].gameObject)
                            {
                                itemParent.transform.GetChild(1).gameObject.SetActive(true);
                            }
                        }

                    }
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                if (itemParent != null)
                {
                    MouseUp();  
                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    //if (itemParent.tag == "Tool_1")
                    if (tag != null && tag.tagValue == "Tool_1")
                    {
                        itemParent.transform.DOMove(lastPos, 0.15f);
                    }
                    else if (tag != null && tag.tagValue == "Item")
                    {
                        /*Item_Level_10 item = itemParent.GetComponent<Item_Level_10>();
                        RaycastHit2D hitFoodPos = Physics2D.Raycast(mousePosition,Vector3.forward,Mathf.Infinity, layerPos);
                        if (hitFoodPos.collider != null)
                        {
                            Position position = hitFoodPos.collider.GetComponent<Position>();
                            if (item.idBox == position.idBoxPos)
                            {
                                for (int i = 0; i < position.listPosition.Count; i++)
                                {
                                    if (item.idItem == position.listPosition[i].GetComponent<Position>().idPos)
                                    {
                                        if (item.idItem ==  3)
                                        {
                                            itemParent.transform.DORotate(new Vector3(0,0,30), 0.15f);
                                        }
                                        else
                                        {
                                            itemParent.transform.DORotate(Vector3.zero, 0.15f);
                                        }
                                        itemParent.transform.DOMove(position.listPosition[i].transform.position, 0.15f).OnComplete(() =>
                                        {
                                            listFoodSucces.Add(itemParent);
                                            itemParent.GetComponent<BoxCollider2D>().enabled = false;
                                            SpriteRenderer spriteItem = itemParent.transform.GetChild(0).GetComponent<SpriteRenderer>();
                                            spriteItem.sprite = item.spItem;
                                            itemParent = null;
                                        });
                                        break;
                                    }
                                }
                            }
                        }*/
                        TruePos truePos = itemParent.GetComponent<TruePos>();
                        if (truePos != null)
                        {
                            if (Vector3.Distance(itemParent.transform.position, truePos.truePos) < truePos.distance)
                            {
                                truePos.Move();
                                if (!listFoodSucces.Contains(itemParent.gameObject))
                                {
                                    listFoodSucces.Add(itemParent.gameObject);
                                }
                                if (itemParent.name =="Item_6" || itemParent.name =="Item_7")
                                {
                                    itemChild.transform.GetComponent<SpriteRenderer>().sprite = itemParent.GetComponent<ChangeSprite>().spriteFold;
                                }
                                
                            }
                            else
                            {
                                itemParent.transform.DOMove(lastPos, 0.15f);
                                RotationController rotationController = itemParent.GetComponent<RotationController>();
                                if (rotationController != null)
                                {
                                    itemParent.transform.DORotate(rotationController.rotation, 0.15f);
                                }
                            }
                        }
                    }
                    else
                    {
                        RaycastHit2D tray = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerTray);
                        if (tray.collider == null)
                        {
                            Vector3 itemParentPos = itemParent.transform.position;
                            if (itemParentPos.x > 0)
                            {
                                itemParent.transform.DOMoveX(10, 0.2f);
                            }
                            else
                            {
                                itemParent.transform.DOMoveX(-10, 0.2f);
                            }
                            listMiss_1.Remove(itemParent);
                            itemParent = null;
                        }
                    }
                    
                    isDragging = false;
                }
                if (listMiss_1.Count == 0 && indexMisson == -1)
                {
                    ShowDone();
                    listD2D[0].enabled = true;
                    indexMisson++;
                }
                if (indexMisson >= 0 && indexMisson < listD2D.Count)
                {
                    if (listD2D[indexMisson].AlphaRatio < 0.01f)
                    {
                        listTool[indexMisson].transform.GetChild(1).gameObject.SetActive(false);
                        listD2D[indexMisson].gameObject.SetActive(false);
                        indexMisson++;
                        ShowDone();
                        if (indexMisson < listD2D.Count)
                        {
                            listD2D[indexMisson].enabled = true;
                        }

                    }
                }


            }
            if (isDragging && itemParent != null)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition);
                itemParent.transform.position = new Vector3(newPosition.x, newPosition.y);
            }
            if (listFoodSucces.Count == listFood.Count && isWin == false)
            {
                isWin = true;
                spCooking.DOFade(1, 1.5f).OnComplete(() =>
                {
                    for (int i = 0; i < listFoodSucces.Count; i++)
                    {
                        ChangeSprite changeSprite =  listFoodSucces[i].GetComponent<ChangeSprite>();
                        listFoodSucces[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = changeSprite.spriteFold;
                    }
                    ShowDone();
                    PopupManager.ShowToast("Win");
                    GameManager_Level_31.instance.setIsGamePause(true);
                });
            }
            if (indexMisson == 10 && wave_2.active == false && !isDragging)
            {
                Wave_1.instance.FadeWave();
                wave_2.SetActive(true);
                Wave_2.instance.FadeWave();
                StartCoroutine(DisableWave1());
            }
        }
        private void MouseDown(float scale)
        {
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            itemParent.transform.DOScale(scale, 0.15f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
            TagGameObject tag = itemParent.GetComponent<TagGameObject>();
            if (tag != null && tag.tagValue == "Tool_1")
            {
                spriteRe.sprite = itemParent.GetComponent<Tool_Level_31>().spOn;
            }

        }
        private void MouseUp()
        {
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
            TagGameObject tag = itemParent.GetComponent<TagGameObject>();
            if (tag != null && tag.tagValue == "Tool_1")
            {
                spriteRe.sprite = itemParent.GetComponent<Tool_Level_31>().spOff;
            }
            /*else if (tag != null && tag.tagValue == "Item")
            {
                RotationController rotationController = itemParent.GetComponent<RotationController>();
                if (rotationController != null)
                {
                    itemParent.transform.DORotate(rotationController.rotation, 0.15f);
                }
               
            }*/
            /*else
            {
                itemParent.transform.DORotate(itemParent.GetComponent<Item>().rotation, 0.15f);
            }*/
        }
        IEnumerator setOffD2D()
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < listD2D.Count; i++)
            {
                //listD2D[i].Rebuild();
                listD2D[i].enabled = false;
            }
        }

        IEnumerator DisableWave1()
        {
            yield return new WaitForSeconds(1f);
            wave_1.SetActive(false);
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
            Debug.Log("Open");
            hint.SetActive(true);
            GameManager_Level_31.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            Debug.Log("Close");
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Level_31.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_Level_31.instance.AddButton();
        }
    }
}
