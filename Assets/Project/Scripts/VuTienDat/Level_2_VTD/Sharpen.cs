using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Sharpen : MonoBehaviour
    {
        public Animator anim;
        public BoxCollider2D boxParent, boxChild;
        public GameObject sharpen;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagGameObject tag = collision.GetComponent<TagGameObject>();    
            if (tag != null && tag.tagValue == "Nail")
            {
                GameObject o = collision.gameObject;
                o.GetComponent<Collider2D>().enabled = false;
                SpriteRenderer spNail = o.GetComponent<SpriteRenderer>();
                DragController_Level_1_2.ins.itemParent = null;
                boxParent.enabled = false;
                boxChild.enabled = false;
                anim.Play("SharpenAnim");
                spNail.DOFade(0, 0.4f);
                sharpen.transform.DOMove(DragController_Level_1_2.ins.lastPos, 0.15f).SetDelay(0.5f).OnStart(() => anim.Play("Default")).OnComplete(() =>
                {
                    boxParent.enabled = true;
                    boxChild.enabled = true;
                    sharpen.transform.DORotate(Vector3.zero, 0.15f);
                    DragController_Level_1_2.ins.RemoveNail(o);
                });
            }
        }
    }
}
