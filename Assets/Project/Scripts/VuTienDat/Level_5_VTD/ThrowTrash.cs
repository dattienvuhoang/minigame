using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class ThrowTrash : MonoBehaviour
    {
        private Collider2D box;
        private void Start()
        {
            box = GetComponent<Collider2D>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagGameObject tag = collision.GetComponent<TagGameObject>();
            if (tag != null && tag.tagValue == "Bin")
            {
                DragController_7_2.instance.RemoveTrash(this.gameObject);
                this.gameObject.SetActive(false);
                box.enabled = false;
                Destroy(this.gameObject,1f);
            }
        }
    }
}
