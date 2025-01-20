using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VuTienDat
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField] private LayerMask layerMaskItem, layerMaskPos;
        [SerializeField] private List<GameObject> listItem;
        [SerializeField] private List<GameObject> listLock;
        [SerializeField] private Vector3 lastPosItem;
        [SerializeField] private GameObject posLock;
        [SerializeField] private List<SpriteRenderer> listSpriteHint;
        public GameObject child, item,  pos, hover;
        public Image imgCheckMark;
        public Animator animCheckMark;
        [SerializeField] private bool isCheck = false;
        private int idItem, idPos;
        private bool isDragging = false;
        [Header("Hint")]
        private bool isPause;
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;
        Camera cam;
        public static CameraController instance;
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
            isPause = GameManager.instance.IsGamePause();
            if ( Input.GetMouseButtonDown(0) && Input.touchCount == 1 && !isPause)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerMaskItem);

                if (hit.collider != null)
                {
                    Debug.Log("Check "+hit.collider.name);
                    TagGameObject tag = hit.collider.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        //if (hit.collider != null && hit.collider.gameObject.CompareTag("Item"))
                        if (tag.tagValue == "Item")
                        {

                            child = hit.collider.gameObject;
                            if (child.transform.parent != null)
                            {
                                isDragging = true;
                                item = child.transform.parent.gameObject;
                                lastPosItem = item.transform.position;
                                MouseDown(item, child, 1.2f);
                            }

                        }
                        //if (hit.collider != null && hit.collider.gameObject.CompareTag("Lock"))
                        if (tag.tagValue == "Lock")
                        {
                            if (listItem.Count == 0)
                            {
                                child = hit.collider.gameObject;
                                isDragging = true;
                                item = child.transform.parent.gameObject;
                                lastPosItem = item.transform.position;
                                MouseDown(item, child, 0.8f);
                            }
                        }
                    }
                }
                
            }

            if (Input.GetMouseButtonUp(0))
            {
                // Da fix
                if (item != null)
                {
                    MouseUp(item, child);
                    Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D[] listHitItem = Physics2D.RaycastAll(mousePosition, Vector3.forward, Mathf.Infinity, layerMaskItem);

                    //Debug.Log("Count: "+listHitItem.Length);

                    if (listHitItem.Length > 1)
                    {
                        //Debug.Log("1111");
                        GameObject items = listHitItem[0].collider.gameObject;
                        item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, items.transform.position.z - 0.001f);

                    }
                    else
                    {
                        item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 0);
                    }
                    //-----------------------//
                    TagGameObject tag = item.GetComponent<TagGameObject>();
                    TagGameObject tagChild = child.GetComponent<TagGameObject>();
                    RaycastHit2D[] listHitPos = Physics2D.RaycastAll(mousePosition, Vector3.forward, Mathf.Infinity, layerMaskPos);
                    {
                        //if (listHitPos[i].collider != null && listHitPos[i].collider.gameObject.CompareTag("ItemPos"))
                        if (listHitPos.Length!=0)
                        {
                            
                            for (int i = 0; i < listHitPos.Length; i++)
                            {
                                if (listHitPos[i].collider.gameObject.CompareTag("ItemPos"))
                                {
                                    idItem = item.GetComponent<Item>().getID();
                                    pos = listHitPos[i].collider.gameObject;

                                    

                                    GameObject posParent = pos.transform.parent.gameObject;
                                    idPos = posParent.transform.GetComponent<ItemPos>().id;
                                    

                                    if (idItem == idPos)
                                    {
                                        GameManager.instance.PlayCheckMark();
                                        
                                        
                                        int sortLayer = pos.GetComponent<SpriteRenderer>().sortingOrder;
                                        SpriteRenderer spriteChild = child.GetComponent<SpriteRenderer>();
                                        spriteChild.sortingOrder = sortLayer;
                                        spriteChild.sprite = item.GetComponent<Item>().itemFold;
                                        if (tag != null && tag.tagValue == "Lock")
                                        //if (item.gameObject.CompareTag("Lock"))
                                        {
                                            item.transform.position = posParent.transform.position;
                                            item.transform.DORotate(Vector3.zero, 0.2f);
                                        }
                                        else
                                        {
                                            item.transform.position = new Vector3(posParent.transform.position.x, posParent.transform.position.y, 0.01f);
                                            item.transform.DOScale(new Vector3(0.8f, 0.8f, 0f), 0.2f);
                                            item.transform.DORotate(Vector3.zero, 0.2f);
                                        }
                                        
                                        
                                        Destroy(child.GetComponent<BoxCollider2D>());
                                        child.AddComponent<BoxCollider2D>();

                                        imgCheckMark.transform.position = item.transform.position;
                                        imgCheckMark.enabled = true;

                                        animCheckMark.Play("AnimCheckMark");
                                        Invoke("DiasableCheckMark", 0.5f);


                                        //if (child.gameObject.CompareTag("Lock"))
                                        if (tagChild != null && tagChild.tagValue == "Lock")
                                        {
                                            if(listLock.Contains(item))
                                            {
                                                listLock.Remove(item);
                                            }
                                        }
                                        else if (listItem.Contains(item))
                                        {
                                            listItem.Remove(item);
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        //if (!listItem.Contains(item) && !item.gameObject.CompareTag("Lock"))
                                        if (!listItem.Contains(item) && tag.tagValue != "Lock")
                                        {
                                            listItem.Add(item);
                                        }
                                        //else if(!listLock.Contains(item) && item.gameObject.CompareTag("Lock"))
                                        else if (!listLock.Contains(item) && tag.tagValue == "Lock")
                                        {
                                            listLock.Add(item);
                                        }
                                    }
                                }
                                
                            }

                        }
                        else
                        {
                            
                            //if (!listItem.Contains(item) && !item.gameObject.CompareTag("Lock"))
                            if (!listItem.Contains(item) && tag.tagValue != "Lock")
                            {
                                //Debug.Log("Lock");
                                listItem.Add(item);
                                Destroy(child.GetComponent<BoxCollider2D>());
                                child.AddComponent<BoxCollider2D>();
                            }
                            //else if (!listLock.Contains(item) && item.gameObject.CompareTag("Lock"))
                            else if (!listLock.Contains(item) && tag.tagValue == "Lock")
                            {
                                listLock.Add(item);
                            }
                        }

                    }

                    Vector3 mousePos = Input.mousePosition;

                    if (mousePos.x <= 0 || mousePos.x >= Screen.width ||
                        mousePos.y <= 0 || mousePos.y >= Screen.height)
                    {
                        item.transform.position = lastPosItem;
                    }
                   
                }
                isDragging = false;
                item = null;
            }

            if (isDragging)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition) ;
                TagGameObject tag = item.GetComponent<TagGameObject>();
                
                //if (item.gameObject.CompareTag("Lock"))
                if (tag != null && tag.tagValue == "Lock")
                {
                    int id = item.GetComponent<Item>().getID();
                    if (id==25)
                    {
                        item.transform.position = new Vector2(item.transform.position.x, newPosition.y);
                    }
                    else 
                    item.transform.position = new Vector2(newPosition.x, item.transform.position.y);
                }
                else
                {
                    item.transform.position = new Vector2(newPosition.x, newPosition.y);
                }
                if (isCheck)
                {
                    Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hitPos = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerMaskPos);
                    //if (hitPos.collider != null && hitPos.collider.gameObject.CompareTag("ItemPos"))
                    if (hitPos.collider != null && hitPos.collider.GetComponent<TagGameObject>().tagValue == "ItemPos")
                    {
                        GameObject check = hitPos.collider.gameObject;
                        if (hover != null && check != hover)
                        {
                            check.GetComponent<SpriteRenderer>().enabled = true;
                            hover.GetComponent<SpriteRenderer>().enabled = false;
                            hover = check;
                        }
                        else
                        {
                            hover = check;
                            check.GetComponent<SpriteRenderer>().enabled = true;
                        }
                    }
                    else
                    {
                        if (hover != null)
                        {
                            hover.GetComponent<SpriteRenderer>().enabled = false;
                        }
                    }
                }
                
            }
            if (!isPause)
            {
                if (listItem.Count == 0)
                {
                    if (!posLock.active)
                    {
                        posLock.SetActive(true);
                        ShowDone();
                    }
                }
                if (listLock.Count == 0 && !isPause)
                {
                    ShowDone();
                    Debug.Log("Win");
                    PopupManager.ShowToast("Win");
                    GameManager.instance.setIsGamePause(true);
                    Invoke("EndGame", 1);
                }
            }
            
        }
        private void MouseDown(GameObject gameObject, GameObject child, float scale)
        {
            //if (!gameObject.gameObject.CompareTag("Lock"))
            TagGameObject tag = gameObject.GetComponent<TagGameObject>();
            if (tag == null || tag.tagValue != "Lock") 
            {
                gameObject.transform.DOScale(new Vector3(scale, scale, 0), 0.2f)/*.SetEase(Ease.InBack)*/;
                gameObject.transform.DORotate(Vector3.zero, 0.2f);
            }    
           
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = gameObject.GetComponent<Item>().itemFold;
            spriteRenderer.sortingOrder = 6;
        }
        private void MouseUp(GameObject gameObject, GameObject child)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 5;
            spriteRenderer.sprite = gameObject.GetComponent<Item>().item;
            //if (!gameObject.gameObject.CompareTag("Lock"))
            TagGameObject tag = gameObject.GetComponent<TagGameObject>();
            if (tag == null || tag.tagValue != "Lock")
                gameObject.transform.DOScale(Vector3.one, 0.2f);
            Vector3 rotation = gameObject.GetComponent<Item>().rotation;
            gameObject.transform.DORotate(rotation, 0.2f);
        }

        private void DiasableCheckMark()
        {
            imgCheckMark.fillAmount = 0;
            imgCheckMark.enabled = false;
            animCheckMark.Play("AnimDisable");
        }

        private void EndGame()
        {
            GameManager.instance.EndGame();
        }
        public void Hint()
        {
            if (listItem.Count > 0)
            {
                int index = Random.Range(0, listItem.Count - 1);
                GameObject item = listItem[index];
                int id = item.GetComponent<Item>().getID();
                listSpriteHint[id - 1].DOFade(1, 0.7f).SetDelay(0.5f).OnComplete(() =>
                {
                    listSpriteHint[id - 1].DOFade(0, 0.7f).SetLoops(9,LoopType.Yoyo);
                });
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
            GameManager.instance.setIsGamePause(true);
            cvHint.DOFade(1, 0.3f).From(0).SetEase(Ease.OutBack);
            hint.transform.DOScale(1, 0.3f).From(0).SetEase(Ease.OutBack);
        }
        public void CloseHint()
        {
            cvHint.DOFade(0, 0.3f).From(1).SetEase(Ease.InBack).OnComplete(() =>
            {
                hint.SetActive(false);
                GameManager.instance.setIsGamePause(false);

            });
            hint.transform.DOScale(0, 0.3f).From(1).SetEase(Ease.InBack);

        }
        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            PanelHome.instance.AddButton();
        }
    }
}