using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Tools : MonoBehaviour
    {
        public int id;
        public Vector3 rotation;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null && collision.CompareTag("Face"))
            {
                if (!DragController_Level_19.instance.CheckTool(id))
                {
                    DragController_Level_19.instance.Emoji();
                }
                if (gameObject.name == "Roller")
                {
                    DragController_Level_19.instance.Fade_Face();

                }
            }
            if (collision != null && collision.CompareTag("wrinkle")&& gameObject.name == "Needle")
            {
                DragController_Level_19.instance.Fade_Wrinkle(collision.gameObject);
            }
            
        }
    }
}
