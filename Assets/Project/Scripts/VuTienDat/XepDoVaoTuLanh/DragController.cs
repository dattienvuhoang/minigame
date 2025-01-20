using Destructible2D;
using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VuTienDat;


namespace VuTienDat_Game2
{
    public class DragController : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMaskItem, layerMaskPos, wave_1;
        public TMP_Text txtWin;
        [SerializeField] private GameObject itemChild, itemParent, itemRef,posRef , doorOpen, doorClose;
        [SerializeField] private List<GameObject> listItem;
        [SerializeField] private List<GameObject> listItemPos;
        [SerializeField] private List<GameObject> listDraw; 
        [SerializeField] private GameObject stains;
        [Header("Hint")]
        [SerializeField] private bool isCheck = false;
        [SerializeField] private List<SpriteRenderer> listSpriteBlink;
        private Vector3 lastPosItem;
        private Vector3 posLastTowel;
        private bool isDragging = false;
        private bool isPause = false;
        private bool isBlink;
        private Camera cam;

        public static DragController instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(instance);
                instance = this;
            }
        }

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            isPause = GameManager.instance.IsGamePause();
            if (Input.GetMouseButtonDown(0) && Input.touchCount == 1 && !isPause)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                if (listItem.Count==1)
                {
                   
                    if (listDraw.Count==0)
                    {
                        RaycastHit2D hitRef = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, wave_1);
                        if (hitRef.collider != null)
                        {
                            doorOpen.SetActive(false);
                            itemRef.SetActive(false);
                            posRef.SetActive(false);
                            doorClose.SetActive(true);
                        }
                    }
                }

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerMaskItem);
                if (hit.collider != null )
                {
                    TagGameObject tag = hit.collider.GetComponent<TagGameObject>();

                    //if (!hit.collider.gameObject.CompareTag("Drawer"))
                    if (tag == null || tag.tagValue != "Drawer")
                    {
                        itemChild = hit.collider.gameObject;
                        itemParent = itemChild.transform.parent.gameObject;
                        isDragging = true;
                        MouseDown(1.1f);
                    }
                    else
                    {
                        GameObject drawer = hit.collider.gameObject;
                        drawer.GetComponent<DrawerController>().CheckOpen();
                    }

                }
               
                
                if (itemParent != null)
                {
                    lastPosItem = itemParent.transform.position;
                    //TagGameObject tag = itemParent.GetComponent<TagGameObject>();

                    if (itemParent.gameObject.name == "Towel")
                    //if (tag!=null && tag.tagValue =="Towel")
                    {
                        posLastTowel = itemParent.transform.position;
                    }
                    if (itemParent.GetComponent<Item>().position != null)
                    {
                        ItemPosition itemPos = itemParent.GetComponent<Item>().position;

                        for (int i = 0; i < itemPos.listItem.Count; i++)
                        {
                            if (itemParent == itemPos.listItem[i])
                            {
                                itemParent.GetComponent<Item>().position.listItem[i] = null;
                                itemParent.GetComponent<Item>().position = null;
                            }
                        }
                    }
                    if (!listItem.Contains(itemParent) && itemParent.gameObject.name != "Towel")
                    {
                        listItem.Add(itemParent);
                    }
                    int id = itemParent.GetComponent<Item>().getID();
                    for (int i = 0; i<listItemPos.Count;i++)
                    {
                        int idPos = listItemPos[i].GetComponent<ItemPosition>().id;
                        if (id == idPos)
                        {
                            listItemPos[i].GetComponent<BoxCollider2D>().enabled = true;
                            if(isCheck)
                            {
                                isBlink = true;
                                for (int j = 0; j < listItemPos[i].transform.childCount;j++)
                                {
                                    SpriteRenderer spriteRenderer  = listItemPos[i].transform.GetChild(j).GetComponent<SpriteRenderer>();
                                    if (!listSpriteBlink.Contains(spriteRenderer))  
                                     {
                                        listSpriteBlink.Add(spriteRenderer);    
                                    }
                                }
                            }
                        }
                    }
                }

            }
            if (Input.GetMouseButtonUp(0))
            {

                if (itemParent != null)
                {
                    isDragging = false;
                    isBlink = false;
                    
                    Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

                    RaycastHit2D[] listHitItem = Physics2D.RaycastAll(mousePosition, Vector3.forward, Mathf.Infinity, layerMaskItem);


                    if (listHitItem.Length > 1)
                    {
                        GameObject items = listHitItem[1].collider.gameObject;
                        TagGameObject tagItems = items.GetComponent<TagGameObject>();

                        //if (items.gameObject.CompareTag("Drawer"))
                        if (tagItems != null && tagItems.tagValue == "Drawer")
                        {
                            itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, 0);
                        }
                        else
                        {
                            itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, items.transform.position.z - 0.001f);

                        }

                    }
                   

                    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerMaskPos);

                    Item item = itemParent.GetComponent<Item>();
                    int id = item.getID();
                    float scaleLast = item.scale;
                    MouseUp(scaleLast);

                    if (hit.collider != null )
                    {
                        GameObject gameObject = hit.collider.gameObject;
                        ItemPosition itemPosition = gameObject.GetComponent<ItemPosition>();
                        if (itemPosition != null)
                        {
                            int idPos = itemPosition.id;
                            int countListItem = itemPosition.listItem.Count;

                            if (idPos == id)
                            {
                                itemParent.GetComponent<Item>().position = itemPosition;
                                for (int i = 0; i < countListItem; i++)
                                {

                                    if (itemPosition.listItem[i] == null)
                                    {
                                        itemParent.transform.DOMove(itemPosition.listPos[i].transform.position, 0.25f);/*.OnComplete(() =>
                                        {
                                            itemPosition.listPos[i].GetComponent<BoxCollider2D>().enabled = false;
                                        });*/
                                        itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, 0.0001f);
                                        float scale = itemPosition.listPos[i].transform.localScale.x;
                                        MouseUp(scale);
                                        SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
                                        spriteRe.sprite = itemParent.GetComponent<Item>().itemFold;
                                        spriteRe.sortingOrder = itemPosition.listPos[i].GetComponent<SpriteRenderer>().sortingOrder;
                                       /* itemPosition.listPos[i].transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;*/
                                        itemParent.transform.DORotate(Vector3.zero, 0.15f);

                                        hit.collider.gameObject.GetComponent<ItemPosition>().listItem[i] = itemParent;
                                        if (listItem.Contains(itemParent))
                                        {
                                            listItem.Remove(itemParent);
                                        }

                                        break;
                                    }

                                }

                            }
                        }
                    }
                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    
                    {
                        if (itemParent.gameObject.name == "Towel")
                        //if (tag.tagValue =="Towel")
                        {
                            itemParent.transform.position = posLastTowel;
                        }
                        //else if (!itemParent.gameObject.CompareTag("Rotten"))
                        else if (tag != null && tag.tagValue !="Rotten")
                        {
                            int idItem = itemParent.GetComponent<Item>().getID();
                            listItemPos[idItem - 1].GetComponent<BoxCollider2D>().enabled = false;
                        }
                    }
                    
                    Vector3 mousePos = Input.mousePosition;

                    if (mousePos.x <= 0 || mousePos.x >= Screen.width ||
                        mousePos.y <= 0 || mousePos.y >= Screen.height)
                    {
                        itemParent.transform.position = lastPosItem;
                    }
                }

                itemParent = null;
            }
            if (isDragging && itemParent != null)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition);
                itemParent.transform.position = new Vector3(newPosition.x, newPosition.y);
            }

            D2dDestructibleSprite checkStains = stains.GetComponent<D2dDestructibleSprite>();
            if (checkStains.AlphaCount == 0 && listItem.Contains(stains))
            {
                listItem.Remove(stains);
            }

            if (listDraw.Count==0)
            {
                doorOpen.GetComponent<BoxCollider2D>().enabled = true;
            }
            if (listItem.Count == 0)
            {
                txtWin.text = "WIN";
                PopupManager.ShowToast("Win");
                GameManager.instance.setIsGamePause(true);
                //PopupManager.Open(PopupPath.POPUP_WinGame, LayerPopup.Main);
            }
            if (isBlink && listSpriteBlink.Count>0)
            {
                for (int i = 0;i<listSpriteBlink.Count; i++)
                {
                    listSpriteBlink[i].enabled = true;
                }
            }
            else if (!isBlink)
            {
                for (int i = 0; i<listSpriteBlink.Count; i++)
                {
                    listSpriteBlink[i].enabled = false;

                    listSpriteBlink.Remove(listSpriteBlink[i]);
                }
            }
        }
        public void CheckItem(GameObject itemCheck)
        {
            Debug.Log("11111111111");
            if (listItem.Contains(itemCheck))
            {
                Debug.Log("2222222222");
                listItem.Remove(itemCheck);
            }
        }
        private void MouseDown(float scale)
        {
            Item item = itemParent.GetComponent<Item>();
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            itemParent.transform.DOScale(scale, 0.15f);
            itemChild.transform.DOScale(item.scale, 0.15f);
            SpriteRenderer spriteRe =itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sprite = item.itemFold;
            spriteRe.sortingOrder = 5;
        }
        private void MouseUp( float scale)
        {
            itemParent.transform.DORotate(itemParent.GetComponent<Item>().rotation, 0.15f);
            itemParent.transform.DOScale(1, 0.15f);
            itemChild.transform.DOScale(scale, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sprite = itemParent.GetComponent<Item>().item;
            spriteRe.sortingOrder = 4;
        }
        public void RemoveDrawer(GameObject game)
        {
            listDraw.Remove(game);   
        }
        public void AddDrawer(GameObject game)
        {
            if (!listDraw.Contains(game))
            {
                listDraw.Add(game); 
            }
       
        }
        public int getListItem()
        {
            return listItem.Count;
        }
        private IEnumerator Blink(SpriteRenderer spriteRenderer)
        {
            if (isBlink)
            {
                float blinkDuration = 0.1f;
                float totalDuration = 15f;
                float timer = 0f;

                while (timer < totalDuration)
                {
                    spriteRenderer.color = new Color(0, 0, 0, spriteRenderer.color.a == 0.5f ? 0 : 0.5f);
                    yield return new WaitForSeconds(blinkDuration);
                    timer += blinkDuration;
                }
            }
        }
    }
}
