using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using VuTienDat;
using SR4BlackDev.UISystem;

namespace VuTienDat_Level1
{
    public class DragController : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerPos;
        [SerializeField] private List<GameObject> listItem, listPos;
        [SerializeField] private List<Sprite> listSprite;
        [SerializeField] private bool isDragging = false;
        [SerializeField] private GameObject item_5;
        [SerializeField] private GameObject XoaWave_2; 
        public TMP_Text txtWin;

        private GameObject itemParent, itemChild;
        private Camera cam;
        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    isDragging = true;
                    itemParent = hit.collider.gameObject;
                    itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, 0);
                    itemChild = itemParent.transform.GetChild(0).gameObject;

                    MouseDown(1.2f);
                }

                if (itemParent!=null)
                {
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
                    if (!listItem.Contains(itemParent))
                    {
                        listItem.Add(itemParent);
                    }
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                if (itemParent != null)
                {
                    Item itemHit = itemParent.GetComponent<Item>();
                    int id = itemHit.id;

                    #region Check thoi son 
                    RaycastHit2D[] hitPos = Physics2D.RaycastAll(mousePosition, Vector3.forward, Mathf.Infinity, layerItem); 
                    if ( hitPos.Length > 1 )
                    {
                        GameObject gameObject = hitPos[1].collider.gameObject;
                        TagGameObject tag = gameObject.GetComponent<TagGameObject>();
                        //if (gameObject.CompareTag("Lipstick"))
                        if ( tag != null && tag.tagValue == "Lipstick")
                        {
                            int idLip = gameObject.GetComponent<Item>().id;
                            if (idLip == id)
                            {
                                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listSprite[id - 7];
                                if (listItem.Contains(itemParent))
                                {
                                    listItem.Remove(itemParent);
                                }
                                Destroy(itemParent);
                                itemParent = null;
                            }
                        }
                        int idPos = gameObject.GetComponent<Item>().id;
                        if (idPos == id && id == 3)
                        {
                            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listSprite[5];
                            itemParent.GetComponent<BoxCollider2D>().enabled = false;   
                        }
                        if (itemParent != null)
                            itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, gameObject.transform.position.z - 0.001f);

                    }
                    #endregion
                    if (itemParent != null)
                    {
                        MouseUp();
                    }
                    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerPos);
                    if (hit.collider != null)
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
                                        //itemParent.transform.position = itemPosition.listPos[i].transform.position;
                                        itemParent.transform.DOMove(itemPosition.listPos[i].transform.position, 0.15f).OnComplete(() =>
                                        {

                                            itemParent.transform.position = new Vector3(itemPosition.listPos[i].transform.position.x, itemPosition.listPos[i].transform.position.y, 0.0001f);
                                            if (id==5)
                                            {
                                                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                                                
                                            }
                                            if (id == 12)
                                            {
                                                itemParent.transform.SetParent(item_5.transform);
                                                itemParent.GetComponent<BoxCollider2D>().enabled = false;
                                            }
                                            if (id == 3)
                                            {
                                               gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = listSprite[4];
                                               itemParent.GetComponent<BoxCollider2D>().enabled=false;
                                            }
                                            itemParent = null;

                                        });
                                        SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
                                        //spriteRe.sprite = itemParent.GetComponent<Item>().itemFold;
                                        spriteRe.sortingOrder = itemPosition.listPos[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
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

                    isDragging = false;
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
                txtWin.text = "WIN";

                PopupManager.ShowToast("Win");

                GameManager.instance.setIsGamePause(true);
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
            itemParent.transform.DORotate(itemParent.GetComponent<Item>().rotation, 0.15f);
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 5;
        }
    }
}
