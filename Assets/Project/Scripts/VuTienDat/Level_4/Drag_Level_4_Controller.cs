using Destructible2D;
using DG.Tweening;
using PaintCore;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VuTienDat_Game2;

namespace VuTienDat
{
    public class Drag_Level_4_Controller : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private List<GameObject> listItem, listWave;
        [SerializeField] private List<GameObject> listTool;
        [SerializeField] private List<GameObject> listSucces;
        [SerializeField] private GameObject paint,outPaint;
        public bool isDragging = false;
        public int indexTool = 1;
        public bool isWin = false;
        
        private GameObject item;
        private Camera cam;
        private Vector3 lastPos;

        private bool isPause;
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;

        public static Drag_Level_4_Controller ins;
        private void Awake()
        {
            ins = this;
        }
        private void Start()
        {
            cam = Camera.main;
            listWave[0].GetComponent<D2dDestructibleSprite>().Rebuild();

            for (int i = 1; i < listWave.Count; i++)
            {
                listWave[i].GetComponent<D2dDestructibleSprite>().Rebuild();
                StartCoroutine(EnabledD2D(i));
            }
            btnContinue.onClick.RemoveAllListeners();
            btnContinue.onClick.AddListener(CloseHint);
            StartCoroutine(delay());
        }

        private void Update()
        {
            isPause = GameManager_Level_4.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0) && !isPause && Input.touchCount == 1)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    isDragging = true;
                    item = hit.collider.gameObject;
                    lastPos = item.transform.position;
                    TagGameObject tag = item.GetComponent<TagGameObject>();
                    if (tag!=null)
                    {
                        //if (item.CompareTag("Tool_2"))
                        if (tag.tagValue == "Tool_2")
                        {
                            listTool[0].SetActive(true);
                        }
                        //else if (item.CompareTag("Tool_3"))
                        else if (tag.tagValue =="Tool_3")
                        {
                            if (listWave.Count == 5)
                            {
                                listTool[1].SetActive(true);
                            }
                            else if (listWave.Count == 3)
                            {
                                listTool[3].SetActive(true);
                            }
                            else if (listWave.Count == 1)
                            {
                                listTool[4].SetActive(true);
                            }

                        }
                        //else if (item.CompareTag("Tool_4"))
                        else if (tag.tagValue =="Tool_4")
                        {
                            listTool[2].SetActive(true);
                            listItem[3].transform.GetComponent<Cleanner>().enabled = true;

                        }
                    }
                    
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                if (item != null)
                {
                    TagGameObject tag = item.GetComponent<TagGameObject>();
                    if (tag!=null)
                    {
                        //if (item.CompareTag("Tool_4"))
                        if (tag.tagValue == "Tool_4")
                        {
                            listItem[3].transform.GetComponent<Cleanner>().enabled = false;
                        }
                    }
                    
                    item.transform.DOMove(lastPos, 0.1f);
                    lastPos = Vector3.zero;
                    for (int i = 0; i < listTool.Count; i++)
                    {
                        listTool[i].SetActive(false);
                    }
                    isDragging = false;
                    item = null;
                }

            }
            if (isDragging && item != null)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition);
                item.transform.position = new Vector3(newPosition.x, newPosition.y);
            }

        }
        public void Tool_1()
        {
            if (listWave.Count > 1)
            {
                if (listWave[1].transform.childCount == 1)
                {
                    listWave[1].transform.GetChild(0).gameObject.SetActive(true);
                }
            }
           
            if (!isDragging)
            {
                D2dDestructibleSprite wave = listWave[0].GetComponent<D2dDestructibleSprite>();
                if (wave.AlphaRatio < 0.01f && indexTool == 1)
                {
                    indexTool++;
                    wave.Clear(); // CLear
                    MoveTool(0, 1);
                    if (!listSucces.Contains(listWave[0]))
                    {
                        listSucces.Add(listWave[0]);
                        listWave[0].SetActive(false);
                        listWave.Remove(listWave[0]);       
                    }
                }
            }    
        }
        public void Tool_2()
        {
            if (!isDragging)

                if (paint.GetComponent<CwChannelCounter>().RatioA > 0.97f && indexTool == 2)
                {
                    indexTool++;
                    MoveTool(1,2);
                    paint.GetComponent<CwGraduallyFade>().enabled = true;
                }
            outPaint.GetComponent<CwGraduallyFade>().enabled = false;
            //paint.GetComponent<CwGraduallyFade>().enabled = false; // add

        }
        public void Tool_3()
        {
            paint.GetComponent<CwGraduallyFade>().enabled = false;
            if (!isDragging)
            {
                if (listWave.Count == 1 && indexTool == 3)
                {
                    if (paint.GetComponent<CwChannelCounter>().RatioA>0.97f)
                    {
                        MoveTool(2, 3);
                        indexTool++;

                    }
                }
                else if (indexTool == 3)
                if (paint.GetComponent<CwChannelCounter>().RatioB < 0.1f && indexTool == 3)
                {
                    indexTool++;
                    MoveTool(2, 3);
                }
                //Debug.Log(indexTool);
            }
               
        }
        public void Tool_4()
        {
           
            D2dDestructibleSprite wave = listWave[0].GetComponent<D2dDestructibleSprite>();
            wave.enabled = true;

            if (!isDragging)
            {
                if (wave.AlphaRatio < 0.01f && paint.GetComponent<CwChannelCounter>().RatioA < 0.1f)
                {
                    wave.Clear(); //Clear
                    if (indexTool == 4)
                    {
                        indexTool = 1;
                        GameObject water = listWave[0].transform.GetChild(0).gameObject;
                        if (listWave.Count != 1)
                        {
                            outPaint.GetComponent<CwGraduallyFade>().enabled = true;
                            //paint.GetComponent<CwGraduallyFade>().enabled = true; // add
                            if (!listSucces.Contains(listWave[0]))
                            {
                                listSucces.Add(listWave[0]);
                                water.GetComponent<SpriteRenderer>().DOFade(0, 1.5f);
                                listWave[0].SetActive(false);
                                listWave.Remove(listWave[0]);
                            }
                            MoveTool(3, 0);
                        }
                        else
                        {
                            if (!isWin)
                            {
                                isWin = true;
                                outPaint.GetComponent<CwGraduallyFade>().enabled = true;
                                paint.GetComponent<CwGraduallyFade>().enabled = true;
                                water.GetComponent<SpriteRenderer>().DOFade(0, 1.5f);
                                PopupManager.ShowToast("Win");
                                GameManager_Level_4.instance.setIsGamePause(true);
                                ShowDone();
                            }
                        }

                    }
                }
            }
            
               
        }

        IEnumerator EnabledD2D(int id)
        {
            yield return new WaitForSeconds(0.1f);
            listWave[id].GetComponent<D2dDestructibleSprite>().enabled = false;

        }
        private void MoveTool(int tool_1 , int tool_2)
        {
           listItem [tool_1].transform.DOMoveX(5, 0.15f).OnComplete(() =>
            {
                listItem[tool_1].SetActive(false);
                listItem[tool_2].SetActive(true);
                listItem[tool_2].transform.DOMoveX(0, 0.15f);
                listItem[tool_1].transform.position = new Vector3(-5, listItem[tool_1].transform.position.y,0);
               
            });
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
            GameManager_Level_4.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_Level_4.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_Level_4.instance.AddButton();
        }
    }
}
