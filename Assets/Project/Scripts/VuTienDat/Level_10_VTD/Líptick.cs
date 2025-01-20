using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Líptick : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagGameObject tag = collision.gameObject.GetComponent<TagGameObject>();
            if (tag != null)
            {
                if (tag.tagValue == "Clamp")
                {
                    this.gameObject.transform.SetParent(collision.transform, true);
                    this.gameObject.transform.DOLocalMove(Vector3.zero, 0.15f);
                }
                if (tag.tagValue == "Box")
                {
                    this.gameObject.transform.SetParent(collision.transform,true);
                    ListTruePos listPos = collision.transform.GetComponent<ListTruePos>();
                    if (listPos != null)
                    {
                        for (int i = 0; i < listPos.listItem.Count; i++)
                        {
                            if (listPos.listItem[i]==null)
                            {
                                listPos.listItem[i] = this.gameObject;
                                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                                this.gameObject.transform.DOMove(listPos.listPos[i].transform.position, 0.3f);
                                this.gameObject.transform.DOScale(Vector3.one, 0.15f);
                                this.gameObject.transform.DORotate(Vector3.zero, 0.15f);
                                DragController_Level_40.ins.EnableBox();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
