using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Flour_Baking : MonoBehaviour
    {
        public GameObject imgFlour;
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
            if (collision != null && collision.GetComponent<TagGameObject>().tagValue == "Finish" && id == 2)
            {
                box.enabled = false;
                DragController_Baking.instance.setNullItem();
                transform.DOLocalMove(new Vector3(-2.15f, 0.57f, 0), 0.15f).OnComplete(() =>
                {
                    anim.enabled = true;
                    imgFlour.transform.DOScale(1, 0.7f).SetDelay(0.3f);
                    imgFlour.transform.DOMoveY(0, 0.7f).SetDelay(0.3f).OnComplete(() =>
                    {
                        StartCoroutine(DisableAnim());

                    });
                });
            }
        }
        IEnumerator DisableAnim()
        {
            yield return new WaitForSeconds(0.2f);
            anim.enabled = false;
            transform.DOScale(1, 0.15f);
            transform.DOMove(pos, 0.15f).OnComplete(() =>
            {
                box.enabled = true;
                DragController_Baking.instance.indexMisson++;
            });
        }
    }
}
