using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class RotationByMouse : MonoBehaviour
    {
        private Camera cam;
        private void Start()
        {
            cam = Camera.main;
        }
        private void Update()
        {
            Rotation();
        }
        private void Rotation()
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 aimDir = (mousePos - transform.position).normalized;
            float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
            if (angle > 80)
            {
                angle = 80;
                Debug.Log(" > 80");
            }
            else if (angle < 0)
            {
                angle = 0;
                Debug.Log(" < 0");

            }
            transform.eulerAngles = new Vector3(0, 0, angle);
        }    
    }
}
