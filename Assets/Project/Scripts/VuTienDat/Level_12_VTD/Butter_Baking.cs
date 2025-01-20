using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Butter_Baking : MonoBehaviour
    {
        public GameObject imgButter;
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
            if (collision != null && collision.GetComponent<TagGameObject>().tagValue == "Finish" && id == 3)
            {
                transform.DOLocalMove(new Vector3(-0.83f, 0.66f, 0), 0.15f).OnComplete(() =>
                {
                    anim.enabled = true;
                    imgButter.transform.DOScale(1, 0.4f).SetDelay(0.2f);
                    imgButter.transform.DOMoveY(0, 0.4f).SetDelay(0.2f).OnComplete(() =>
                    {
                        StartCoroutine(DisableAnim());
                        DragController_Baking.instance.indexMisson++;

                    });
                });
            }
        }
        IEnumerator DisableAnim()
        {
            yield return new WaitForSeconds(0.5f);
            anim.enabled = false;
            transform.DOScale(1, 0.15f);
            transform.DOMove(pos, 0.15f).OnComplete(() =>
            {
                box.enabled = true;
            });
        }
    }
}
