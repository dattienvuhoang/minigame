using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Glue : MonoBehaviour
    {
        private SpriteRenderer spVisual;
        private void Start()
        {
            spVisual = GetComponent<SpriteRenderer>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagGameObject tag  = collision.GetComponent<TagGameObject>();
            if ( tag != null && tag.tagValue == "GlueBoth")
            {
                spVisual.DOFade(1, 0.25f).OnComplete(() =>
                {
                    DragController_Level_1_2.ins.RemoveGlue(this.gameObject);
                });
            }
        }
    }
}
