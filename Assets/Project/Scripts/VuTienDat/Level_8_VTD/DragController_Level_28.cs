using Destructible2D;
using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace VuTienDat
{
    public class DragController_Level_28 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerPos;
        [SerializeField] private bool isDragging = false;
        [SerializeField] private int indexWave = 1;
        [SerializeField] private List<D2dDestructibleSprite> listD2D;

        [Header("Wave 1")]
        [SerializeField] private GameObject wave_1;
        [SerializeField] private List<SpriteRenderer> listSpriteWave1;
        [SerializeField] private List<GameObject> listItemWave_1;

        [Header("Wave 2")]
        [SerializeField] private GameObject wave_2;
        [SerializeField] private List<SpriteRenderer> listSpriteWave2;
        [SerializeField] private List<GameObject> listItemWave_2;

        [Header("Wave 3")]
        [SerializeField] private GameObject wave_3;
        [SerializeField] private List<SpriteRenderer> listSpriteWave3;
        [SerializeField] private List<GameObject> listItemWave_3;

        [Header("Wave 4")]
        [SerializeField] private GameObject wave_4;
        [SerializeField] private List<SpriteRenderer> listSpriteWave4;
        [SerializeField] private List<GameObject> listItemWave_4; 
        
        [Header("Wave 5")]
        [SerializeField] private GameObject wave_5;
        [SerializeField] private List<SpriteRenderer> listSpriteWave5;
        [SerializeField] private List<GameObject> listItemWave_5;
        [SerializeField] private int indexItem = 1;
        [SerializeField] private BoxCollider2D turnOnBox;
        [SerializeField] private Animator anim;
       

        private GameObject itemParent, itemChild;
        private Camera cam;
        private Vector3 lastPos;
        private int indexOrder;

        private bool isPause;
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;

        public static DragController_Level_28 ins;
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
            /*for (int i = 0; i < listD2D.Count; i++)
            {
                listD2D[i].Rebuild();
            }*/
        }

        private void Update()
        {
            isPause = GameManager_Level_28.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0) && !isPause /*&& Input.touchCount == 1*/)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    /*if (hit.collider.gameObject.CompareTag("Misson_1"))
                    {
                        TurnOnOffFan.Instance.OnOff();
                        if (!TurnOnOffFan.Instance.isOn)
                        {
                            for (int i = 5; i < 8; i++)
                            {
                                listItemWave_1[i].GetComponent<BoxCollider2D>().enabled = true;
                            }
                        }
                    }
                    else
                    {
                        isDragging = true;
                        itemParent = hit.collider.gameObject;
                        itemChild = itemParent.transform.GetChild(0).gameObject;
                        lastPos = itemParent.transform.position;
                        MouseDown(1.3f);
                    }*/
                    TagGameObject tag = hit.collider.gameObject.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        if (tag.tagValue == "Switch")
                        {
                            Debug.Log("Click");
                            //TurnOnOffFan.Instance.OnOff();
                            hit.collider.GetComponent<TurnOnOffFan>().OnOff();
                            ShowDone();
                            if (!TurnOnOffFan.Instance.isOn && indexWave == 1)
                            {
                                for (int i = 5; i < 8; i++)
                                {
                                    listItemWave_1[i].GetComponent<BoxCollider2D>().enabled = true;
                                }
                            }
                        }
                        else
                        {
                            
                            isDragging = true;
                            itemParent = hit.collider.gameObject;
                            itemChild = itemParent.transform.GetChild(0).gameObject;
                            if (tag.tagValue == "Soap")
                            {
                                itemParent.transform.GetChild(1).gameObject.SetActive(true);
                            }
                            if (tag.tagValue == "Item_2")
                            {
                                if (itemParent.name == "Screw")
                                {
                                    itemParent.transform.GetChild(1).gameObject.SetActive(false);
                                    itemParent.transform.GetChild(0).gameObject.SetActive(true);
                                }
                            }
                            lastPos = itemParent.transform.position;
                            MouseDown(1.3f);
                        }
                    }

                }
                

            }
            if (Input.GetMouseButtonUp(0))
            {
                if (itemParent != null)
                {
                    MouseUp();
                    isDragging = false;

                    TagGameObject tag = itemParent.gameObject.GetComponent<TagGameObject>();
                    if ( tag != null )
                    {
                        if (tag.tagValue == "Tool" || tag.tagValue == "Soap")
                        {
                            if (tag.tagValue == "Soap")
                            {
                                itemParent.transform.GetChild(1).gameObject.SetActive(false);
                            }
                            itemParent.transform.DOMove(lastPos, 0.15f);
                        }
                        else if (tag.tagValue == "Item_2")
                        {
                           
                            TruePos truePos = itemParent.GetComponent<TruePos>();
                            if (Vector3.Distance(truePos.truePos, itemParent.transform.position) < truePos.distance)
                            {
                                if (listItemWave_5.IndexOf(itemParent) == indexItem|| indexItem >= 6 )
                                {
                                    truePos.Move();
                                    itemChild.GetComponent<SpriteRenderer>().sortingOrder = indexItem;
                                    indexItem++;
                                }
                                else
                                {
                                    if (itemParent.name == "Screw")
                                    {
                                        itemParent.transform.GetChild(0).gameObject.SetActive(true);
                                        itemParent.transform.GetChild(1).gameObject.SetActive(false);
                                    }
                                    itemParent.transform.DOLocalMove(lastPos, 0.15f);

                                }
                            }
                            else
                            {
                                if (itemParent.name == "Screw")
                                {
                                    itemParent.transform.GetChild(1).gameObject.SetActive(true);
                                    itemParent.transform.GetChild(0).gameObject.SetActive(false);
                                }
                                itemParent.transform.DOLocalMove(lastPos, 0.15f);
                            }
                        }
                        else if (Vector3.Distance(itemParent.transform.position, lastPos) > 0.5f)
                        {
                            if (itemParent.transform.position.x <= 0)
                            {
                                itemParent.transform.DOMoveX(-10, 0.2f);
                            }
                            else
                            {
                                itemParent.transform.DOMoveX(10, 0.2f);
                            }
                            listItemWave_1.Remove(itemParent);
                            if (listItemWave_1.Count < 6 && listItemWave_1.Count > 0)
                            {
                                listItemWave_1[listItemWave_1.Count - 1].GetComponent<Collider2D>().enabled = true;
                            }
                        }
                        else
                        {
                            Debug.Log("Move To Last 2");

                            itemParent.transform.DOMove(lastPos, 0.15f);
                        }
                    }

                    itemParent = null;
                }
            }
            if (isDragging && itemParent != null)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition);
                itemParent.transform.position = new Vector3(newPosition.x, newPosition.y);
            }
            if (indexWave == 1 && listItemWave_1.Count == 0)
            {
                ShowDone();
                indexWave++;
                wave_3.SetActive(true);
                NextToWave2();
            }
            if (indexWave == 2)
            {
                if (wave_2.GetComponent<CleanWave>().isDone)
                {
                    if (!isShowDone)
                    {
                        isShowDone = true;
                        ShowDone();
                    }
                    if (!isDragging)
                    {
                        {
                            wave_4.SetActive(true); 
                            isShowDone = false;
                            Debug.Log("Done Wave 2");
                            NextToWave3();
                            indexWave++;
                        }
                    }
                }
            }
            if (indexWave == 3/* && !isDragging*/)
            {
                /*if (!isShowDone)
                {
                    isShowDone = true;
                    ShowDone();
                }*/
                if (wave_3.GetComponent<CleanWave>().isDone)
                {
                    if (!isShowDone)
                    {
                        isShowDone = true;
                        ShowDone();
                    }
                    if (!isDragging)
                    {
                        if (wave_3.GetComponent<CleanWave>().isDone)
                        {
                            isShowDone = false;
                            wave_5.SetActive(true);
                            Debug.Log("Donw Wave 3");
                            NextToWave4();
                            indexWave++;
                        }
                    }
                }
                    
                    
            }
            if (indexWave == 4 /*&& !isDragging*/)
            {
                /*if (!isShowDone)
                {
                    isShowDone = true;
                    ShowDone();
                }*/
                if (wave_4.GetComponent<CleanWave>().isDone)
                {
                    if (!isShowDone)
                    {
                        isShowDone = true;
                        ShowDone();
                    }
                    if (!isDragging)
                    {
                        {
                            isShowDone = false;
                            Debug.Log("Donw Wave 4");
                            NextToWave5();
                            indexWave++;
                        }
                    }
                }
            }
            if (indexWave == 5 /*&& !isDragging*/)
            {
                /*if (!isShowDone)
                {
                    isShowDone = true;
                    ShowDone();
                }*/
                if (!isDragging)
                {
                    isShowDone = false;
                    if (indexItem == 9)
                    {
                        indexWave++;
                        turnOnBox.enabled = true;
                    }
                }

                    
            }
            if (indexWave == 6)
            {
                if (TurnOnOffFan.Instance.isOn)
                {
                    ShowDone();
                    GameManager_Level_28.instance.setIsGamePause(true);
                    anim.enabled = true;
                    turnOnBox.enabled = false;
                    PopupManager.ShowToast("Win");
                    indexWave++;
                }
                else
                {
                    anim.enabled = false;
                }
            }
            
        }
        private void MouseDown(float scale)
        {
            itemParent.transform.DOScale(scale, 0.15f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            indexOrder = spriteRe.sortingOrder;
            spriteRe.sortingOrder = 10;

            //spriteRe.sprite = itemParent.GetComponent<Item_Level_10>().spItemFold;
        }
        private void MouseUp()
        {
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = indexOrder;
            //spriteRe.sprite = itemParent.GetComponent<Item_Level_10>().spItem;
        }
        private void NextToWave2()
        {
            for (int i = 0; i < listSpriteWave1.Count; i++)
            {
                listSpriteWave1[i].DOFade(0, 0.25f);
            }
            StartCoroutine(SetActiveGameObject(wave_1, 0.5f));
            listItemWave_2[0].transform.DOMoveX(-0.5f, 0.5f);
            listItemWave_2[1].transform.DOMoveX(-0.8f, 0.5f);
            listItemWave_2[2].transform.DOMoveX(1.2f, 0.5f);
        }
        private void NextToWave3()
        {
            for (int i = 0;i < listSpriteWave2.Count;i++)
            {
                listSpriteWave2[i].DOFade(0, 1f);
            }
            StartCoroutine(SetActiveGameObject(wave_2, 1.05f));
            listItemWave_3[0].transform.DOMoveX(-0.5f, 1.25f);
            listItemWave_3[1].transform.DOMoveX(-0.8f, 1.25f);
            listItemWave_3[2].transform.DOMoveX(1.2f, 1.25f);
        }
        private void NextToWave4()
        {
            for (int i = 0; i < listSpriteWave3.Count; i++)
            {
                listSpriteWave3[i].DOFade(0, 1f);
            }
            StartCoroutine(SetActiveGameObject(wave_3, 1.05f));
            listItemWave_4[0].transform.DOMoveX(0f, 1.25f);
            listItemWave_4[1].transform.DOMoveX(1f, 1.25f);
        }
        private void NextToWave5()
        {
            for (int i = 0; i < listSpriteWave4.Count; i++)
            {
                listSpriteWave4[i].DOFade(0, 1f);
            }
            StartCoroutine(SetActiveGameObject(wave_4, 1.05f));
            listItemWave_5[0].transform.DOMoveX(0f, 1.25f);
            listItemWave_5[1].transform.DOMoveX(-1.8f, 1.25f); // Back
            listItemWave_5[4].transform.DOMoveX(-1.84f, 1.25f); // Front
            listItemWave_5[5].transform.DOMoveX(2.35f, 1.25f); // Circle 
            listItemWave_5[2].transform.DOMoveX(2.3f, 1.25f); // Fan
            listItemWave_5[3].transform.DOMoveX(-0.6f, 1.25f); // Lid
            listItemWave_5[6].transform.DOMoveX(-1.4f, 1.25f);
            listItemWave_5[7].transform.DOMoveX(0.22f, 1.25f);
            listItemWave_5[8].transform.DOMoveX(1f, 1.25f);
        }

        IEnumerator SetActiveGameObject(GameObject go , float time)
        {
            yield return new WaitForSeconds(time);
            go.SetActive(false);
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
            GameManager_Level_28.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            Debug.Log("Close");
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Level_28.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_Level_28.instance.AddButton();
        }
    }
}
