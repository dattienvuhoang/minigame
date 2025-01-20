using Destructible2D;
using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VuTienDat_Game2;

namespace VuTienDat
{
    public class DragController_Level_37 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerPos;
        [SerializeField] private List<GameObject> listItem, listPos;
        [SerializeField] private bool isDragging = false;
        [SerializeField] private Vector3 lastPos;

        [Header("Wave 1")]
        [SerializeField] private List<D2dDestructibleSprite> listD2D;
        [SerializeField] private List<GameObject> listTool;
        [SerializeField] private List<GameObject> listMoveUp;
        [SerializeField] private List<GameObject> listMoveDown;
        [SerializeField] private List<GameObject> listNail;
        [SerializeField] private SpriteRenderer spShoe; 
        public int indexTool = 0;
        public int indexMisson = 1;

        [Header("Wave 2")]
        [SerializeField] private List<GameObject> listMove;
        [SerializeField] private List<GameObject> listNailWave2;
        [SerializeField] private Vector3 posTool6;
        [SerializeField] private BoxCollider2D boxMongChan;
        private bool isFold = true;


        private GameObject itemParent, itemChild;
        private Camera cam;

        private bool isPause;
        public GameObject emojiLike;
        private bool isShowDone = false;
        public GameObject hint;
        public CanvasGroup cvHint;
        public Button btnContinue;

        public static DragController_Level_37 instance;

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            cam = Camera.main;
            StartCoroutine(DisableD2D());
        }

        private void Update()
        {
            isPause = GameManager_Level_37.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0) && Input.touchCount == 1 && !isPause)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    isDragging = true;
                    itemParent = hit.collider.gameObject;
                    itemChild = itemParent.transform.GetChild(0).gameObject;
                    lastPos = itemParent.transform.position;
                    if (listTool.IndexOf(itemParent) == indexTool && indexMisson >= 3)
                    {
                        itemParent.GetComponent<Cleanner>().enabled = true;
                    }

                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        //if (itemParent.tag == "Lock")
                        if (tag.tagValue == "Lock")
                        {
                            itemParent.GetComponent<RotationByMouse>().enabled = true;
                            Debug.Log("Click Lock");
                        }
                        
                    }
                    if (itemParent.name == "Tool_6")
                    {
                        itemParent.transform.DOScale(2, 0.15f);
                    }
                    isFold = true;
                    MouseDown(1);
                }
                

            }
            if (Input.GetMouseButtonUp(0))
            {
                if (itemParent != null)
                {

                    if (itemParent.GetComponent<Cleanner>() != null)
                    {
                        itemParent.GetComponent<Cleanner>().enabled = false;
                    }
                    TagGameObject tag = itemParent.GetComponent <TagGameObject>();
                    if (tag != null)
                    {
                        //if (itemParent.tag == "Tool_1")
                        if (tag.tagValue == "Tool_1")
                        {
                            if (itemParent.GetComponent<TruePos>() != null)
                            {
                                TruePos truePos = itemParent.GetComponent<TruePos>();
                                if (Vector3.Distance(truePos.truePos, itemParent.transform.position) < truePos.distance)
                                {
                                    truePos.enabled = true;
                                    indexMisson++;
                                    isFold = false;
                                }
                                else
                                    itemParent.transform.DOMove(lastPos, 0.25f);

                            }
                            else
                                itemParent.transform.DOMove(lastPos, 0.25f);
                        }
                        //if (itemParent.tag == "Lock")
                        if (tag.tagValue == "Lock")
                        {
                            itemParent.GetComponent<RotationByMouse>().enabled = false;
                            if (itemParent.transform.eulerAngles.z == 0)
                            {
                                itemParent.GetComponent<Collider2D>().enabled = false;
                                indexMisson++;
                                Debug.Log("Lock");
                            }
                            else
                            {
                                Debug.Log("No Lock");
                            }
                        }
                    }
                    
                    if (isFold)
                    {
                        MouseUp();
                    }
                    isDragging = false;
                    itemParent = null;
                }
            }
            if (isDragging && itemParent != null)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition);
                TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                //if (itemParent.tag!="Lock")
                if (tag == null || tag.tagValue !="Lock")
                {
                    itemParent.transform.position = new Vector3(newPosition.x, newPosition.y);
                }
            }
            if (indexMisson == 3 && indexTool == 0)
            {
                listD2D[indexTool].enabled = true;
                if (listD2D[indexTool].AlphaRatio < 0.05f)
                {
                    for (int i = 0; i < listNail.Count; i++)
                    {
                        listNail[i].GetComponent<BoxCollider2D>().enabled = true;
                    }
                    indexTool++;
                }
            }
            if (listNail.Count == 0 && indexMisson == 3)
            {
                indexMisson++;
                spShoe.DOFade(0, 1f).OnComplete(() =>
                {
                    listD2D[indexTool].enabled = true;
                });
            }
            if (listD2D[indexTool].AlphaRatio == 0 && indexMisson == 4)
            {
                indexMisson++;
                listD2D[indexTool].gameObject.SetActive(false);
                listTool[indexTool].GetComponent<Cleanner>().enabled = false;
                indexTool++;
                listD2D[indexTool].enabled = true;
            }
            if (!isDragging)
            {
                if (listD2D[indexTool].AlphaRatio == 0f && indexMisson == 5)
                {
                    indexMisson++;
                    for (int i = 0; i < listMoveUp.Count; i++)
                    {
                        listMoveUp[i].transform.DOMoveY(10, 1f);
                    }
                    for (int i = 0; i < listMoveDown.Count; i++)
                    {
                        listMoveDown[i].transform.DOMoveY(-10, 1f);
                    }
                    listMove[0].transform.DOMoveY(2.86f, 1f);
                    listMove[1].transform.DOMoveY(2.86f, 1f);
                    listMove[2].transform.DOMoveY(-2.86f, 1f);
                }
            }
            if (indexMisson == 15)
            {
                indexMisson++;
                boxMongChan.enabled = true;
            }
        }
        private void MouseDown(float scale)
        {
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            if (itemParent.GetComponent<ChangeSprite>() != null)
            {
                spriteRe.sprite = itemParent.GetComponent<ChangeSprite>().spriteFold;
                itemParent.transform.DORotate(itemParent.GetComponent<ChangeSprite>().rotation, 0.15f);
            }
        }
        private void MouseUp()
        {
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            if (itemParent.GetComponent<ChangeSprite>() != null)
            {
                spriteRe.sprite = itemParent.GetComponent<ChangeSprite>().sprite;
            }
        }
        IEnumerator DisableD2D()
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < listD2D.Count; i++)
            {
                listD2D[i].enabled = false;
            }
        }
        public void RemoveNail(GameObject nail)
        {
            listNail.Remove(nail);
        }
        public void MoveToLast()
        {
            boxMongChan.enabled = false;
            listMove[1].transform.DOMove(new Vector3(2.05f, 2.86f, 0), 0.25f).OnComplete(() =>
            {
                PopupManager.ShowToast("Win");
                GameManager_Level_37.instance.setIsGamePause(true);
            });
        }
    }
}
