using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Baking_Baking : MonoBehaviour
    {
        public SpriteRenderer imgBaking;
        public Vector3 pos;
        public Animator anim;
        public BoxCollider2D box;
        int id;
        private void Start()
        {
            pos = transform.position;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            /*if (collision!=null && collision.CompareTag("Finish"))*/
            id = DragController_Baking.instance.indexMisson;
            if (collision!=null && collision.GetComponent<TagGameObject>().tagValue == "Finish" && id == 4)
            {
                box.enabled = false;
                transform.DOLocalMove(new Vector3(-1.91f, 0.24f, 0), 0.2f).OnComplete(() =>
                {
                    anim.enabled = true;
                    imgBaking.DOFade(1, 0.5f).SetDelay(0.5f).OnComplete(() =>
                    {
                        anim.enabled = false;
                        transform.DOMove(pos, 0.2f).OnComplete(() =>
                        {
                            box.enabled = true;
                            DragController_Baking.instance.indexMisson++;
                        });
                    });

                });
            }
        }
    }
}
