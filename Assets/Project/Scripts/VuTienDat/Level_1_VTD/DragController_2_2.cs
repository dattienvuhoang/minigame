using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VuTienDat
{
    public class DragController_2_2 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private List<GameObject> listItem, listPos;
        [SerializeField] private GameObject shirt;
        [SerializeField] private BoxCollider2D boxItem;   
        [SerializeField] private bool isDragging = false;
        public bool isOpen;
        private GameObject itemParent, itemChild;
        private Camera cam;
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;
        private bool isPause;
        public static DragController_2_2 instance;
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
            isOpen = OpenCloseCabin.instance.isOn;
            if (isOpen)
            {
                shirt.SetActive(false);
            }
            else
            {
                shirt.SetActive(true);
            }
            isPause = GameManager_Level_2_2.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0) && !isPause && Input.touchCount == 1)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    TagGameObject tag = hit.collider.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        if (tag.tagValue == "Door")
                        {
                            OpenCloseCabin.instance.OnOff();
                        }
                        else
                        {
                            isDragging = true;
                            itemParent = hit.collider.gameObject;
                            itemChild = itemParent.transform.GetChild(0).gameObject;
                            MouseDown(1.2f);
                        }
                    }
                    
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                if (itemParent != null)
                {
                    Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                    RaycastHit2D[] hitAll = Physics2D.RaycastAll(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                    if (hitAll.Length > 1)
                    {
                        Debug.Log("True");
                        float z = 0;
                        for (int i = 0; i < hitAll.Length; i++)
                        {
                            if (hitAll[i].transform.position.z < z )
                            {
                                z = hitAll[i].transform.position.z;
                            }
                        }
                        itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, z - 0.001f);
                    }
                    else
                    {
                        Debug.Log("Falsw");

                        itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, 0);
                    }

                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        RaycastHit2D hit  = Physics2D.Raycast(mousePosition,Vector3.forward, Mathf.Infinity, layerItem);


                        bool shouldMove = false;
                        int sortingOrder = 0;

                        TruePos truePos = itemParent.GetComponent<TruePos>();

                        switch (tag.tagValue)
                        {
                            case "Item":
                                if (Vector3.Distance(itemParent.transform.position, truePos.truePos) < truePos.distance)
                                {
                                    shouldMove = true;
                                    //sortingOrder = 2;
                                }
                                break;

                            case "Item_2":
                                if (isOpen && Vector3.Distance(itemParent.transform.position, truePos.truePos) < truePos.distance)
                                {
                                    shouldMove = true;
                                    //sortingOrder = 1;
                                }
                                break;

                            case "Box":
                                if (!isOpen && listItem.Count <= 4 && Vector3.Distance(itemParent.transform.position, truePos.truePos) < truePos.distance)
                                {
                                    boxItem.enabled = false;
                                    shouldMove = true;
                                    //sortingOrder = 3;
                                }
                                break;
                        }

                        if (shouldMove)
                        {
                            truePos.Move();
                            itemParent.transform.DORotate(Vector3.zero, 0.15f);
                            itemParent.transform.DOScale(1, 0.15f);
                            itemChild.GetComponent<SpriteRenderer>().sortingOrder = itemParent.GetComponent<TruePos>().sortingLayer;
                            listItem.Remove(itemParent);
                        }
                        else
                        {
                            MouseUp();
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
            if (listItem.Count == 0)
            {
                listItem.Add(null);
                ShowDone();
                GameManager_Level_2_2.instance.setIsGamePause(true);
                PopupManager.ShowToast("Win");
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
            itemParent.transform.DORotate(itemParent.GetComponent<RotationController>().rotation, 0.15f);
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 3;
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
            Debug.Log("Hint");
            hint.SetActive(true);
            GameManager_Level_2_2.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Level_2_2.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_Level_2_2.instance.AddButton();
        }
    }
}
