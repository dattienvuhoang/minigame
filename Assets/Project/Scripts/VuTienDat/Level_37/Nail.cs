using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Nail : MonoBehaviour
    {
        public SpriteRenderer sprite;
        
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null && collision.name == "Kim")
            {
                sprite.DOFade(0, 1f).OnComplete(() =>
                {
                    DragController_Level_37.instance.RemoveNail(this.gameObject);
                });

            }
        }
    }
}
