using Destructible2D;
using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VuTienDat_Game2;

namespace VuTienDat
{
    public class DragController_Level23 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        public List<GameObject> listItem;
        public List<GameObject> listItenPos;
        [SerializeField] private List<D2dDestructibleSprite> listD2D;
        [SerializeField] private GameObject rag, cup, tulip, can;
        [SerializeField] private bool isDragging = false;
        [SerializeField] private Vector3 lastPos;
        public RuntimeAnimatorController anim;
        public int indexHint = 0;
        private GameObject itemParent, itemChild;
        private Camera cam;
        public static DragController_Level23 instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            for (int i = 0; i < listD2D.Count; i++) 
            {
                listD2D[i].Rebuild();
            }
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
                    itemChild = itemParent.transform.GetChild(0).gameObject;
                    lastPos = itemParent.transform.position; 
                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    if (tag != null && tag.tagValue =="Rag")
                    {
                        itemParent.GetComponent<Cleanner>().enabled = true;    
                    }
                    MouseDown(1.2f);
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                if (itemParent != null)
                {
                    MouseUp();
                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    if (tag != null && tag.tagValue == "Rag")
                    {
                        itemParent.GetComponent<Cleanner>().enabled = false;
                    }

                    RaycastHit2D[] hit = Physics2D.RaycastAll(mousePosition,Vector3.forward, Mathf.Infinity, layerItem);
                    if (hit != null && hit.Length > 1)
                    {
                        Debug.Log(hit[1].collider.gameObject.name);
                        itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, hit[1].collider.gameObject.transform.position.z - 0.001f);
                    }
                    else
                    {
                        itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, 0);
                    }
                    Vector3 mousePos = Input.mousePosition;

                    if (mousePos.x <= 0 || mousePos.x >= Screen.width ||
                        mousePos.y <= 0 || mousePos.y >= Screen.height)
                    {
                        itemParent.transform.DOMove(lastPos, 0.15f);
                    }
                    isDragging = false;
                    itemParent = null;
                    lastPos = Vector3.zero;
                }
            }
            if (isDragging && itemParent != null)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition);
                itemParent.transform.position = new Vector3(newPosition.x, newPosition.y);
                
            }
            if (listD2D.Count != 0)
            {
                for (int i = 0; i < listD2D.Count; i++)
                {
                    if (listD2D[i].AlphaRatio < 0.001f)
                    {
                        listD2D.Remove(listD2D[i]);
                        listD2D[i].gameObject.SetActive(false);
                    }
                }
            }
            else if (itemParent == null && !listItem.Contains(cup))
            {
                rag.transform.DOMoveX(10, 0.5f);
            }
            if (listD2D.Count == 0 && listItem.Count == 0)
            {
                PopupManager.ShowToast("Win");
            }
        }
        private void MouseDown(float scale)
        {
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            itemParent.transform.DOScale(scale, 0.15f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 20;
        }
        private void MouseUp()
        {
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 1;
            TruePos truePos = itemParent.GetComponent<TruePos>();
            if (truePos != null)
            {
                TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                if (listItem.Count != 1 )
                {
                    if (tag != null && tag.tagValue != "Can")
                        truePos.Move();
                }
                else if (listD2D.Count == 0)
                {
                    truePos.Move();
                }

                if (truePos.isMove)
                {
                    itemParent.transform.DORotate(itemParent.GetComponent<RotationController>().rotation, 0.15f);
                }
                else
                {
                    if (listItem.Count == 1 && listD2D.Count == 0)
                    {
                        
                        if (tag != null && tag.tagValue == "Can")
                        {
                            itemParent.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().DOFade(1, 3f).OnComplete(() =>
                            {
                                tulip.GetComponent<Tulip>().FadeTulip();
                                can.transform.DOMoveX(-10, 1f).OnComplete(() =>
                                {
                                    listItem.Remove(can);
                                });

                            });
                        }
                    }
                    else
                    {
                        //listItem.Remove(itemParent);
                        int index =  listItem.IndexOf(itemParent);
                        Debug.Log("Index: " + index);
                        listItem.Remove(itemParent);
                        listItenPos.Remove(listItenPos[index]);
                    }
                }
            }
            
        }
        public void Hint()
        {
            if (listItem.Count != 0)
            {
                int index = Random.Range(0, listItenPos.Count);
                indexHint = index;
                listItenPos[index].transform.GetChild(0).AddComponent<Animator>();
                listItenPos[index].transform.GetChild(0).gameObject.GetComponent<Animator>().runtimeAnimatorController = anim;
                StartCoroutine(DelayFlicker());
            }
            
        }    
        IEnumerator DelayFlicker()
        {
            yield return new WaitForSeconds(2.5f);
            Destroy(listItenPos[indexHint].transform.GetChild(0).gameObject.GetComponent<Animator>());
        }
    }
}
