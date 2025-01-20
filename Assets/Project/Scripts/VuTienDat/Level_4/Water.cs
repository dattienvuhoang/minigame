using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Water : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D box;
        private bool isTrigger = false;
        private float scaleInit = 0;
        private float scaleLast = 0;
        private Tween scaleTween;
       
        private void Update()
        {
            if (isTrigger)
            {
                scaleInit += Time.deltaTime;
                if (scaleInit <= 1f)
                {
                    //scaleTween = transform.DOScale(scaleInit,10f);
                    transform.localScale = new Vector3(scaleInit, scaleInit);
                }
                else
                {
                    box.enabled = false;
                }
               
            }
            else
            {
                //scaleTween.Kill();
                scaleLast = scaleInit;
            }

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if (collision != null && collision.gameObject.CompareTag("Tool_4"))
            TagGameObject tag = collision.GetComponent<TagGameObject>();
            if (tag != null && tag.tagValue == "Tool_4") 
            {
                isTrigger = true;
                scaleInit = scaleLast;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            isTrigger = false;
        }
    }
}
