using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Egg_Baking : MonoBehaviour
    {
        public SpriteRenderer spEgg;
        public GameObject imgEgg;
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
            /*if (collision!=null && collision.CompareTag("Finish"))*/
            id = DragController_Baking.instance.indexMisson;
            if (collision != null && collision.GetComponent<TagGameObject>().tagValue == "Finish" && id == 1)
            {
                box.enabled = false;
                DragController_Baking.instance.setNullItem();
                transform.DOLocalMove(new Vector3(-1.62f, -1.62f, 0), 0.15f).OnComplete(() =>
                {
                    anim.enabled = true;
                    spEgg.DOFade(0, 0.3f).SetDelay(1.2f);
                    imgEgg.transform.DOMoveY(0, 0.3f).SetDelay(1.2f);
                    imgEgg.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack).From(0).SetDelay(1.2f);
                    DragController_Baking.instance.indexMisson++;
                    //DragController_Baking.instance.isPull = false;
                });
               
            }
        }
    }
}
