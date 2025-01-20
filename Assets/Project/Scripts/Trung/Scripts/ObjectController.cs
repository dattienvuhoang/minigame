using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class ObjectController : MonoBehaviour
    {
        private bool canMove;


        private void OnMouseDown()
        {
            if(canMove) 
            {
                MouseController.instance.GetMousePos(transform);
            }

        }
        private void OnMouseDrag()
        {
            if (canMove)
            {
                transform.position = MouseController.instance.MouseDragging();
            }
        }

        private void OnMouseUp()
        {
            if (canMove)
            {
                MouseController.instance.MouseUp(transform);
            }
        }
        private void Start()
        {
            canMove = true;
        }
    }
}





