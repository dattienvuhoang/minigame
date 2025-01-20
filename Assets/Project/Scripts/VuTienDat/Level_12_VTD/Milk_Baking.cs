using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Milk_Baking : MonoBehaviour
    {
        public Animator anim;
        public Vector3 pos;
        public BoxCollider2D box;
        private int id;
        private void Start()
        {
            pos = transform.position;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            id = DragController_Baking.instance.indexMisson;
            /*if (collision !=null && collision.CompareTag("Finish"))*/
            if (collision != null && collision.GetComponent<TagGameObject>().tagValue == "Finish" && id == 0)
            {
                box.enabled = false;
                transform.DOMove(new Vector3(-1.25f, 0, 0), 0.1f).OnComplete(() =>
                {
                    anim.enabled = true;
                    DragController_Baking.instance.FillMilk();
                    DragController_Baking.instance.setNullItem();
                    transform.DOMove(pos, 0.15f).SetDelay(2f);
                    transform.DOScale(1, 0.15f).SetDelay(2f).OnComplete(() =>
                    {
                        box.enabled = true;
                        DragController_Baking.instance.indexMisson++;
                    });
                    
                });
            }
            else
            {
                Debug.Log("No");
            }
        }
    }
}
