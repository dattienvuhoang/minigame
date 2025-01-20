using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Item_Baking : MonoBehaviour
    {
        public GameObject truePos;
        public Vector3 lastPos;
        public BoxCollider2D box;
        private bool isDrag = false;
        public float distance = 0;
        private void Start()
        {
            lastPos = transform.position;
        }
        private void Update()
        {
            if (isDrag && truePos != null)
            {
                if (DragController_Baking.instance.isDragging == false)
                {
                    if (Vector3.Distance(transform.position, truePos.transform.position) < distance)
                    {
                        Debug.Log("Check");
                        transform.GetChild(0).transform.DOScale(0.5f, 0.15f);
                        transform.DOScale(1, 0.15f);
                        TagGameObject tag = gameObject.GetComponent<TagGameObject>();

                        /*if (gameObject.tag == "Lock")*/
                        if (tag.tagValue == "Lock")
                        {
                            transform.DOMove(truePos.transform.position, 0.2f).OnComplete(() =>
                            {
                                DragController_Baking.instance.ChangeSpriteCake();
                            });
                        }
                        else
                        {
                            transform.DOMove(truePos.transform.position, 0.2f);
                            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 3;
                            /*if (gameObject.tag == "Misson_1")*/
                            if (tag.tagValue == "Cake")
                            {
                                DragController_Baking.instance.RemoveCake(gameObject);
                            }
                            Debug.Log("DOMove To TruePos");
                        }
                        /*if (transform.tag == "Item")*/
                        if (tag.tagValue == "Item")
                        {
                            DragController_Baking.instance.RemoveItem(this.gameObject);

                        }
                        box.enabled = false;
                        isDrag = false;
                    }
                    else
                    {
                        Debug.Log("Move lastPos");
                        transform.DOMove(lastPos, 0.2f);
                        isDrag = false;
                    }
                }
                
            }
        }
        public void Dragging()
        {
            isDrag = true;
        }

    }
}
