using DG.Tweening;
using UnityEngine;
using VuTienDat;

namespace VuTienDat_Game3_LamBanh
{
    public class Fish : MonoBehaviour
    {
        [SerializeField] private GameObject fish;
        [SerializeField] private SpriteMask mask;
        [SerializeField] private BoxCollider2D box; 
        private bool isTrigger = false;
        private float scaleInit = 0;
        private float scaleLast = 0;
        private Tween scaleTween;
        private void Start()
        {
            
        }
        private void Update()
        {
            if (isTrigger)
            {
                scaleInit += Time.deltaTime;
                if (scaleInit <= 1f)
                {
                    scaleTween = mask.transform.DOScale(scaleInit, 0.5f);
                }
                else
                {
                    box.enabled = false;
                    fish.transform.DOScale(1.1f, 0.05f).OnComplete(() => { 
                        fish.transform.DOScale(1f, 0.05f); 
                        DragController_Baking.instance.SetIndexCake();
                    });
                }
            }
            else
            {
                scaleTween.Kill();
                scaleLast = scaleInit;
            }
           
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            /*if(collision!=null && collision.gameObject.CompareTag("Finish"))*/
            TagGameObject tag = collision.GetComponent<TagGameObject>();
            if (tag != null && tag.tagValue == "Finish")
            {
                isTrigger = true;
                scaleInit = scaleLast;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            isTrigger  = false;
        }
    }
}
