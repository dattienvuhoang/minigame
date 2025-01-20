using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuTienDat;

namespace VuTienDat_Game2
{
    public class RottenController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //if (collision != null && collision.gameObject.CompareTag("Trashcan"))
            TagGameObject tag = collision.GetComponent<TagGameObject>();
            if (tag != null && tag.tagValue == "Trashcan")
            {
                GameObject parent = this.gameObject.transform.parent.gameObject;
                //Destroy(parent);
                //Debug.Log(parent);
                DragController_Refrigerator.instance.CheckItem(parent);
                parent.SetActive(false);
            }
        }
    }
}
