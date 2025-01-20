using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class SkinPicker : MonoBehaviour
     
    {
        public Animator anim;
        public GameObject picker, tool;
        public BoxCollider2D boxParent, boxChild;
        private Vector3 lastPos;
        public RotationController rot;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject o = collision.gameObject;
            TagGameObject tag = o.GetComponent<TagGameObject>();
            if (tag != null && tag.tagValue == "Box")
            {
                picker.transform.SetParent(o.transform);
                //picker.transform.localPosition = Vector3.zero;
                picker.transform.SetAsLastSibling();
                
                boxParent.enabled = false;
                boxChild.enabled = false;
                o.GetComponent<BoxCollider2D>().enabled = false;
                DragController_Level_1_2.ins.itemParent = null;
                /*picker.transform.DOLocalMove(o.transform.GetChild(0).transform.position, 0.15f).OnComplete(() =>
                {
                    anim.Play("Skin_picker");
                    anim.Play("Default");
                    picker.transform.DOLocalMove(o.transform.GetChild(1).transform.position, 0.15f).SetDelay(1).OnComplete(() =>
                    {
                        anim.Play("Skin_picker");
                        anim.Play("Default");
                        picker.transform.DOLocalMove(o.transform.GetChild(2).transform.position, 0.15f).SetDelay(1).OnComplete(() =>
                        {
                            anim.Play("Skin_picker");
                            anim.Play("Default");
                            picker.transform.DOMove(DragController_Level_1_2.ins.lastPos, 0.3f).SetDelay(0.3f);
                        });
                    });
                });*/
                Sequence sequence = DOTween.Sequence();
                sequence.Append(picker.transform.DOLocalMove(o.transform.GetChild(0).transform.localPosition, 0.05f).OnComplete(() =>
                    anim.Play("Skin_picker")
                ));
                sequence.AppendInterval(0.5f);
                anim.Play("Default");

                sequence.Append(picker.transform.DOLocalMove(o.transform.GetChild(1).transform.localPosition, 0.05f).OnStart(() =>
                {
                    anim.Play("Default");

                }).OnComplete(() =>
                {
                    anim.Play("Skin_picker");
                }));
                anim.Play("Default");

                sequence.AppendInterval(0.5f);
                sequence.Append(picker.transform.DOLocalMove(o.transform.GetChild(2).transform.localPosition, 0.05f).OnStart(() => anim.Play("Default")).OnComplete(() =>
                {
                    Debug.Log(o.transform.GetChild(2).transform.localPosition);

                    anim.Play("Skin_picker");
                }));
                sequence.AppendInterval(0.5f);
                sequence.Append(o.transform.DOMoveY(o.transform.position.y - 1f, 0.2f).OnComplete(() =>
                {
                    o.GetComponent<SpriteRenderer>().DOFade(0, 0.15f);
                    picker.transform.DOMove(DragController_Level_1_2.ins.lastPos, 0.25f);
                    picker.transform.DORotate(rot.rotation, 0.25f);
                    picker.transform.SetParent(tool.transform);
                    picker.transform.SetSiblingIndex(2);
                    boxParent.enabled = true;
                    boxChild.enabled = true;
                    DragController_Level_1_2.ins.RemoveHandSkin(o);
                    DragController_Level_1_2.ins.Move_Clipper();
                }));
            }
        }
    }
}
