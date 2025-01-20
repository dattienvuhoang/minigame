using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class RottenController : MonoBehaviour
    {
        bool isOpen;
        private void Update()
        {
            isOpen = DragController_Level_7.ins.isOpen;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger");
            //if (collision != null && collision.gameObject.CompareTag("Trashcan"))
            TagGameObject tag = collision.GetComponent<TagGameObject>();
            if (tag != null && tag.tagValue == "Trashcan" && isOpen) 
            {
                //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
                DragController_Level_7.ins.RemoveRotten(this.gameObject);
            }
        }
    }
}
