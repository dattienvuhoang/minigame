using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VuTienDat
{
    public class DragController_Stained_Glass : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerTool;
        [SerializeField] private LayerMask layerFail;
        [SerializeField] private LayerMask layerPos;
        [SerializeField] private List<GameObject> listItem, listPos;
        [SerializeField] private bool isDragging = false;
        private Vector3 lastPosTool, currentPosTool;
        private GameObject itemParent, itemChild;
        private Camera cam;

        [Header("Hint")]
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;
        private bool isPause;


        public static DragController_Stained_Glass instance;
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
            isPause = GameManager_StainedGlass.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0) && !isPause)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerTool);
                if (hit.collider != null)
                {
                    isDragging = true;
                    itemParent = hit.collider.gameObject;
                    itemChild = itemParent.transform.GetChild(0).gameObject;
                    MouseDown(1);
                    
                    lastPosTool = itemParent.transform.position;
                }

                RaycastHit2D hitFail = Physics2D.Raycast(mousePosition,Vector3.forward,Mathf.Infinity, layerFail);
                if (hitFail.collider != null)
                {
                    isDragging = true;
                    itemParent = hitFail.collider.gameObject;
                    itemChild = itemParent.transform.GetChild(0).gameObject;
                    MouseDownFail(1.1f);
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                if (itemParent != null)
                {
                    MouseUp();
                    TagGameObject tag  = itemParent.GetComponent<TagGameObject>();
                    //if (itemParent.tag == "Item")
                    if (tag!=null && tag.tagValue =="Item")
                    {
                        RaycastHit2D[] listHitItem = Physics2D.RaycastAll(mousePosition, Vector3.forward, Mathf.Infinity, layerFail);

                        if (listHitItem.Length > 1)
                        {
                            Debug.Log("1111");
                            GameObject items = listHitItem[0].collider.gameObject;
                            itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, items.transform.position.z - 0.001f);
                        }
                        else
                        {
                            itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, 0);
                        }
                        Vector3 truePos = itemParent.GetComponent<Fail_Glass>().truePos.transform.position;
                        if (Vector3.Distance(itemParent.transform.position, truePos) < 0.5f)
                        {
                            itemParent.transform.DOMove(new Vector3(truePos.x,truePos.y,0.1f), 0.15f);
                            GameManager_StainedGlass.instance.RemoveFail(itemParent);
                            itemParent.GetComponent<Collider2D>().enabled = false;
                        }
                    }
                    else
                    {
                        itemParent.transform.DOMove(lastPosTool, 0.15f);
                    }
                    isDragging = false;
                    itemParent = null;
                    GameManager_StainedGlass.instance.CheckWave_6();
                    GameManager_StainedGlass.instance.EndGame();
                }
            }
            if (isDragging && itemParent != null)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition);
                itemParent.transform.position = new Vector3(newPosition.x, newPosition.y);
                if (itemParent.transform.childCount > 1)
                {
                    currentPosTool = itemParent.transform.GetChild(1).transform.position;
                }
                
            }
        }
        private void MouseDown(float scale)
        {
            itemParent.transform.DORotate( new Vector3(0,0,45), 0.15f);
            itemParent.transform.DOScale(scale, 0.15f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
            TagGameObject tag = itemParent.GetComponent<TagGameObject>();
            //if (itemParent.tag == "Tool_1")
            if (tag != null && tag.tagValue == "Tool_1") 
            {
                spriteRe.sprite = itemParent.GetComponent<Soldering>().spOn;
                itemParent.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        private void MouseDownFail(float scale)
        {
            itemParent.transform.DOScale(scale, 0.15f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
        }
        private void MouseUp()
        {
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 1;
            TagGameObject tag = itemParent.GetComponent<TagGameObject>();
            
            //if (itemParent.tag == "Tool_1")
            if (tag!=null && tag.tagValue =="Tool_1")
            {
                spriteRe.sprite = itemParent.GetComponent<Soldering>().spOff;
                itemParent.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        public Vector3 getPosTool()
        {
            return currentPosTool;
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
            GameManager_StainedGlass.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager_StainedGlass.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            UIController_StainedGlass.instance.AddButton();
        }
    }
}
