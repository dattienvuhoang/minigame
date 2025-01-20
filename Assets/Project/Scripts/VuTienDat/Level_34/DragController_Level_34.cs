using Destructible2D;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class DragController_Level_34 : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private bool isDragging = false;
        public int indexWave = 0;

        [Header("Wave 1")]
        [SerializeField] private GameObject wave_1;
        [SerializeField] private List<SpriteRenderer> listSpriteWave_1;
        [SerializeField] private List<GameObject> listItemWave_1;

        [Header("Wave 2")]
        [SerializeField] private GameObject wave_2;
        [SerializeField] private BoxCollider2D lid;
        [SerializeField] private List<SpriteRenderer> listSpriteWave_2;
        [SerializeField] private List<GameObject> listD2D_Wave2;
        [SerializeField] private Vector3 lidPosTrue;
        [SerializeField] private List<SpriteRenderer> listSpriteToolWave2;
        private bool isRebuilSucces = false;

        [Header("Wave 3")]
        [SerializeField] private GameObject test;
        [SerializeField] private BoxCollider2D wire;
        [SerializeField] private List<SpriteRenderer> listSpriteWave_3;
        [SerializeField] private List<BoxCollider2D> listBoxWave_3;
        [SerializeField] private List<GameObject> listD2DWave_3;
        [SerializeField] private Vector3 posOn;
        [SerializeField] private List<GameObject> listMisson;
        [SerializeField] private int indexMisson = 0;   

        private GameObject itemParent, itemChild;
        private Vector3 lastPos; 
        private Camera cam;
        public static DragController_Level_34 instance;

        private void Awake()
        {
            instance = this;
        }
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
                    itemChild = itemParent.transform.GetChild(0).gameObject;
                    lastPos = itemParent.transform.position;
                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    if (tag!=null && tag.tagValue =="On")
                    {
                        itemParent.transform.GetChild(0).gameObject.SetActive(true);
                        itemParent.transform.GetChild(1).gameObject.SetActive(false);
                    }
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                if (itemParent != null)
                {
                   /* if (itemParent.tag == "Lock")
                    {
                        if (Vector3.Distance(lastPos, itemParent.transform.position)> 0.5f)
                        {
                            WireChangeSprite.ins.ChangeSprite();
                        }
                    }
                    else if (itemParent.tag == "Target 1")
                    {
                        if (Vector3.Distance(itemParent.transform.position, lidPosTrue) < 0.5f)
                        {
                            itemParent.transform.DOMove(lidPosTrue, 0.15f).OnComplete(() =>
                            {
                                lidPosTrue = lastPos;
                                for (int i = 0; i< listSpriteToolWave2.Count; i++)
                                {
                                    listSpriteToolWave2[i].DOFade(1, 0.3f);
                                }
                            });
                        }
                        else
                        {
                            itemParent.transform.DOMove(lastPos, 0.15f);
                        }
                    }
                    else if (itemParent.tag == "Tool_1")
                    {
                        itemParent.transform.DOMove(lastPos, 0.15f);
                    }
                    else 
                    {
                        if (Vector3.Distance(lastPos,itemParent.transform.position)>1f)
                        {
                            itemParent.transform.DOMoveX(10, 0.3f);
                            listItemWave_1.Remove(itemParent);
                        }
                        else
                        {
                            itemParent.transform.DOMove(lastPos, 0.3f);
                        }
                    }*/

                    TagGameObject tag = itemParent.GetComponent<TagGameObject>();
                    if (tag != null)
                    {
                        if (tag.tagValue == "Off")
                        {
                            if (Vector3.Distance(lastPos, itemParent.transform.position) > 0.5f)
                            {
                                itemParent.GetComponent<WireChangeSprite>().ChangeOff();
                                //WireChangeSprite.ins.ChangeOff();
                            }
                        }
                        else if (tag.tagValue == "On" )
                        {
                            if (Vector3.Distance(itemParent.transform.position, posOn) < 0.5f)
                            {
                                itemParent.transform.DOMove(posOn, 0.15f);
                                //WireChangeSprite.ins.ChangOn();
                                itemParent.GetComponent<WireChangeSprite>().ChangeOn();
                            }
                            else
                            {
                                itemParent.transform.DOMove(lastPos, 0.15f);
                                itemParent.transform.GetChild(0).gameObject.SetActive(false);
                                itemParent.transform.GetChild(1).gameObject.SetActive(true);
                            }
                        }
                        else if (tag.tagValue == "Move")
                        {
                            if (Vector3.Distance(lastPos, itemParent.transform.position) > 1f)
                            {
                                itemParent.transform.DOMoveX(10, 0.3f);
                                listItemWave_1.Remove(itemParent);
                            }
                        }
                        else if (tag.tagValue == "Tool")
                        {
                            //itemParent.transform.DOMove(lastPos, 0.15f);
                            TruePos truePos = itemParent.GetComponent<TruePos>();
                            if (truePos != null)
                            {
                                if (Vector3.Distance(truePos.truePos, itemParent.transform.position)<truePos.distance)
                                {
                                    truePos.Move();
                                }
                                else
                                {
                                    itemParent.transform.DOMove(lastPos, 0.15f);
                                }
                            }
                            else
                            {
                                itemParent.transform.DOMove(lastPos, 0.15f);
                            }

                        }
                        else if (tag.tagValue =="Lid")
                        {
                            if (Vector3.Distance(itemParent.transform.position, lidPosTrue) < 0.5f)
                            {
                                itemParent.GetComponent<BoxCollider2D>().enabled = false;
                                itemParent.transform.DOMove(lidPosTrue, 0.15f).OnComplete(() =>
                                {
                                    lidPosTrue = lastPos;
                                    for (int i = 0; i < listSpriteToolWave2.Count; i++)
                                    {
                                        listSpriteToolWave2[i].DOFade(1, 0.3f);
                                    }
                                });
                            }
                            else
                            {
                                itemParent.transform.DOMove(lastPos, 0.15f);
                            }
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
                TagGameObject tag = itemParent.GetComponent<TagGameObject>();

                if (tag != null)
                {
                    if (/*tag.tagValue == "On" ||*/ tag.tagValue == "Off")
                    {
                        if (newPosition.y > 0.9f)
                            newPosition.y = 0.9f;
                        if (newPosition.y < -0.9f)
                            newPosition.y = -0.9f;
                        newPosition.x = itemParent.transform.position.x;
                    }
                    if (tag.tagValue == "On")
                    {
                        /*if (newPosition.y > 0.9f)
                            newPosition.y = 0.9f;
                        if (newPosition.y < -0.9f)
                            newPosition.y = -0.9f;
                        newPosition.x = itemParent.transform.position.x;*/
                    }
                }

                itemParent.transform.position = new Vector3(newPosition.x, newPosition.y);
            }
            if (listItemWave_1.Count == 0 && indexWave == 1)
            {
                indexWave++;
                Invoke("Wave_2_Enable", 0.5f);
            }
            if (indexWave == 2  && isRebuilSucces)
            {
                for (int i = 0; i < listD2D_Wave2.Count; i++)
                {
                    if (listD2D_Wave2[i].GetComponent<D2dDestructibleSprite>().AlphaCount == 0)
                    {
                        listD2D_Wave2.Remove(listD2D_Wave2[i]);
                    }
                }
                if (listD2D_Wave2.Count == 0)
                {
                    lid.enabled = true;
                }
            }
            /*if (indexWave == 3 && isRebuilSucces)
            {
                for (int i = 0; i <listD2DWave_3.Count; i++)
                {
                    if (listD2DWave_3[i].GetComponent<D2dDestructibleSprite>().AlphaCount == 0)
                    {
                        listD2DWave_3.Remove(listD2DWave_3[i]);
                    }
                }
                if (listD2DWave_3.Count == 0)
                {
                    wire.enabled = true;
                    isRebuilSucces = false;
                }
            }*/
            if (Wave_2_RebuildD2D.Instance.isNextWave && indexWave == 2)
            {
                isRebuilSucces = false;
                indexWave++;
                Debug.Log("Next Waveeeeeeeeeeeeeeeeeeee");
                Invoke("Wave_3_Enable", 0.5f);

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
            //itemParent.transform.DORotate(itemParent.GetComponent<Item_Level_10>().rotation, 0.15f);
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 5;
        }
        public void Wave_2_Enable()
        {
            for (int i = 0; i< listSpriteWave_1.Count;i++)
            {
                listSpriteWave_1[i].DOFade(0, 1f);
            }
            for (int i = 0; i<listSpriteWave_2.Count;i++)
            {
                listSpriteWave_2[i].DOFade(1, 1f);
            }
            for (int i = 0; i < listD2D_Wave2.Count; i++)
            {
                listD2D_Wave2[i].SetActive(true);
            }
            Wave_2_RebuildD2D.Instance.RebuildD2D();
            wave_1.SetActive(false);
            isRebuilSucces = true;
        }
        public void Wave_3_Enable()
        {
            for (int i = 0; i < listSpriteWave_2.Count; i++)
            {
                listSpriteWave_2[i].DOFade(0, 1f);
            }
            for (int i = 0; i < listSpriteToolWave2.Count; i++)
            {
                listSpriteToolWave2[i].DOFade(0, 1f);
            }
            for (int i = 0; i < listSpriteWave_3.Count; i++)
            {
                listSpriteWave_3[i].DOFade(1, 1f);

            }
            for (int i = 0;i<listD2DWave_3.Count;i++)
            {
                listD2DWave_3[i].SetActive(true);
            }
            for (int i = 0; i < listBoxWave_3.Count; i++)
            {
                listBoxWave_3[i].enabled = true;
            }
            wave_2.SetActive(false);
            isRebuilSucces |= true;
        }
    }
}
