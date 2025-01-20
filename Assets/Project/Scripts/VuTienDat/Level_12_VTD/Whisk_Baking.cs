using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Whisk_Baking : MonoBehaviour
    {
        public SpriteRenderer imgMix;
        public List<SpriteRenderer> listItem;
        public Animator anim;
        public BoxCollider2D box;
        public Vector3 lastPos;
        int id;
        private void Start()
        {
            lastPos = transform.position;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            /*if (collision != null && collision.CompareTag("Finish"))*/
            id = DragController_Baking.instance.indexMisson;
            TagGameObject GameTag = collision.GetComponent<TagGameObject>();


            if (GameTag!=null && GameTag.tagValue == "Finish" && id == 5)
            {
                anim.enabled = true;
                box.enabled = false;
                for (int i = 0; i < listItem.Count; i++)
                {
                    listItem[i].DOFade(0, 5f);
                }
                imgMix.DOFade(1, 4).OnComplete(() =>
                {
                    anim.enabled = false;
                    transform.DOMove(lastPos, 0.3f).OnComplete(() =>
                    {
                        DragController_Baking.instance.indexMisson++;
                    });
                });
            }
        }


    }
}
