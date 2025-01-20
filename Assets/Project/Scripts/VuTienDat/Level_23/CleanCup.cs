using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuTienDat;

namespace VuTienDat
{
    public class CleanCup : MonoBehaviour
    {
        public SpriteRenderer spDirt, spVisual_1, spVisual_2;
        public int indexTrigger = 3;
        public BoxCollider2D box;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagGameObject tag = collision.GetComponent<TagGameObject>();
            if (tag != null && tag.tagValue == "Rag")
            {
                if (indexTrigger == 3)
                {
                    spDirt.DOFade(0.9f, 0.1f);
                    indexTrigger--;
                }
                else if (indexTrigger == 2)
                {
                    spDirt.DOFade(0.6f, 0.1f);
                    indexTrigger--;
                }
                else
                {
                    spDirt.DOFade(0, 0.5f);
                    spVisual_1.DOFade(0, 0.5f);
                    spVisual_2.DOFade(1, 0.5f);
                    box.enabled = false;
                    //DragController_Level23.instance.listItem.Remove(this.gameObject);
                    int index = DragController_Level23.instance.listItem.IndexOf(this.gameObject);
                    DragController_Level23.instance.listItem.Remove(this.gameObject);
                    DragController_Level23.instance.listItenPos.Remove(DragController_Level23.instance.listItenPos[index]);
                }
            }
        }
    }
}
