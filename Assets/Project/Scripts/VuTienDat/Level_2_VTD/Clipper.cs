using Destructible2D;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace VuTienDat
{
    public class Clipper : MonoBehaviour
    {
        public Animator anim;
        public bool isHurt = false;
        public int index;
        public GameObject d2d;
        public BoxCollider2D boxClipper;
        private void Start()
        {
        }
        private void Update()
        {
            if (index == 2 && DragController_Level_1_2.ins.listNailHead.Count == 0 && !DragController_Level_1_2.ins.isDragging)
            {
                boxClipper.enabled = false;
                index++;
                if (isHurt)
                {
                    DragController_Level_1_2.ins.indexMisson += 1;
                    DragController_Level_1_2.ins.MoveTool(3, 4);
                }
                else
                {
                    DragController_Level_1_2.ins.indexMisson += 2;
                    DragController_Level_1_2.ins.MoveTool(3, 5);
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagGameObject tag = collision.GetComponent<TagGameObject>();
            if (tag != null )
            {
                if (tag.tagValue == "Nail_Head")
                {
                    index = DragController_Level_1_2.ins.indexMisson;
                    GameObject o = collision.gameObject;
                    o.GetComponent<BoxCollider2D>().enabled = false;
                    anim.Play("ClipperAnim");

                    o.transform.DOMoveY(o.transform.position.y - 0.5f, 0.25f).SetDelay(0.15f).OnStart(() => anim.Play("Default")).OnComplete(() =>
                    {
                        o.GetComponent<SpriteRenderer>().DOFade(0, 0.15f);
                        DragController_Level_1_2.ins.RemoveNailHead(o);
                       
                    }
                    );
                }
                if (tag.tagValue == "Hurt")
                {
                    isHurt = true;
                    GameObject o = collision.gameObject;
                    o.GetComponent<SpriteRenderer>().DOFade(1, 0.15f).OnComplete(() =>
                    {
                        o.AddComponent<D2dDestructibleSprite>();
                        o.GetComponent<SpriteRenderer>().material = d2d.GetComponent<SpriteRenderer>().material;
                        o.GetComponent<D2dDestructibleSprite>().Rebuild();
                        DragController_Level_1_2.ins.AddD2DHurt(o.GetComponent<D2dDestructibleSprite>());
                    });
                }
            }
            
        }
    }
}
