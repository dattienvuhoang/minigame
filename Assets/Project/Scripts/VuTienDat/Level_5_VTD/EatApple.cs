using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class EatApple : MonoBehaviour
    {
        public Animator anim;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagGameObject tag = collision.GetComponent<TagGameObject>();
            if (tag != null)
            {
                if (tag.tagValue == "Cappy")
                {
                    anim.enabled = true;
                    this.gameObject.SetActive(false);
                    DragController_7_2.instance.RemoveItem(this.gameObject);
                    Destroy(this.gameObject, 1f);

                }
            }
        }
    }
}
