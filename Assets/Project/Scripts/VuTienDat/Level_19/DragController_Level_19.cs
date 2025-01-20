using Destructible2D;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class DragController_Level_19 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerPos;
        [SerializeField] private List<GameObject> listItem, listPos;
        [SerializeField] private bool isDragging = false;
        private Vector3 lastPos;
        private int idTool;

        [Header("Wave")]
        [SerializeField] private int indexMisson = 1; 
        [SerializeField] private GameObject currentTool;
        [SerializeField] private List<GameObject> listTools, wave_1,wave_2, listMisson;

        [Header("Nisson_1")]
        [SerializeField] private D2dDestructibleSprite miss_1;

        [Header("Misson_2")]
        [SerializeField] private List<GameObject> miss_2;
        [SerializeField] private Animator anim_needle;
        [SerializeField] private BoxCollider2D R_wrinkle;
        [SerializeField] private BoxCollider2D L_wrinkle;

        [Header("Misson_3")]
        [SerializeField] private List<GameObject> miss_3;
        [SerializeField] private GameObject faceOld;
        [SerializeField] private BoxCollider2D faceYoung;


        private GameObject itemParent, itemChild;
        private Camera cam;
        public static DragController_Level_19 instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            cam = Camera.main;
            currentTool = listTools[0];
        }

        private void Update()
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    isDragging = true;
                    itemParent = hit.collider.gameObject;
                    itemChild = itemParent.transform.GetChild(0).gameObject;
                    lastPos = itemParent.transform.position;
                    MouseDown(1);
                    // Kich hoat Anim cua Tool 
                    Tools tools = itemParent.GetComponent<Tools>();
                    idTool = tools.id;
                    if (idTool == 1)
                    {
                        itemParent.transform.GetChild(1).gameObject.SetActive(true);
                    }
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                if (itemParent != null)
                {
                    MouseUp();
                    if (idTool == 1)
                    {
                        itemParent.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    itemParent.transform.DOMove(lastPos, 0.1f).OnComplete(() =>
                    {
                        itemParent = null;
                    });
                    isDragging = false;
                    switch (indexMisson)
                    {
                        case 1:
                            {
                                Misson_1();
                                break;
                            }
                        case 2:
                            {
                                Misson_2();
                                break;
                            }
                        case 3:
                            {
                                Misson_3();
                                break;
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
           
        }
        private void MouseDown(float scale)
        {
            itemParent.transform.DORotate(itemParent.GetComponent<Tools>().rotation, 0.2f);
            itemParent.transform.DOScale(scale, 0.2f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
        }
        private void MouseUp()
        {
            itemParent.transform.DORotate(Vector3.zero, 0.2f);
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 5;
        }
        public bool CheckTool(int id)
        {
            if (listTools[id-1] == currentTool)
                return true;
            return false;
        }
        public void Emoji()
        {
            Debug.Log("Current Tool :  "+ currentTool + " -1 Live");
        }
        private void Misson_1()
        {
            if (miss_1.AlphaCount < 40)
            {
                listMisson.Remove(listMisson[0]);
                currentTool = listTools[1];
                R_wrinkle.enabled = true;
                L_wrinkle.enabled = true;
                indexMisson++;
                Debug.Log("Done Misson 1");
            }
        }

        public void Fade_Wrinkle(GameObject gameObject)
        {
            anim_needle.Play("Needle_Fade");
            gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(() =>
            {
                miss_2.Remove(gameObject);
            });
        }
        private void Misson_2()
        {
            if (miss_2.Count == 0)
            {
                listMisson.Remove(listMisson[0]);
                currentTool = listTools[2];
                indexMisson++;
                Debug.Log("Done Misson 2");
            }
        }

        public void Fade_Face()
        {
            for (int i = 0; i < miss_3.Count; i++)
            {
                miss_3[i].GetComponent<SpriteRenderer>().DOFade(0, 3f).OnComplete(() =>
                {
                    miss_3.Remove(miss_3[0]);
                });
            }
        }
        private void Misson_3()
        {
            if (miss_3.Count == 0)
            {
                faceOld.SetActive(false);
                faceYoung.enabled = true;
                for (int i = 0; i < wave_1.Count; i++)
                {
                    wave_1[i].SetActive(false);
                }
                Debug.Log("Done Misson 3");
            }
        }
    }
}
