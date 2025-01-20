using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Leaf : MonoBehaviour
    {
        public Animator anim;
        private void Start()
        {
            
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagGameObject tag = collision.GetComponent<TagGameObject>();
            if (tag != null)
            {
                if (tag.tagValue == "Cut")
                {
                    anim.Play("ScissorsAnim");
                    transform.DOMoveY(-3.2f, 0.5f).OnStart(()=> anim.Play("Default")).SetDelay(0.5f);
                    gameObject.layer = 6;
                }
            }
        }
    }
}
