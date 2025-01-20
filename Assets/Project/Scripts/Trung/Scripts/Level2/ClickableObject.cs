using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class ClickableObject : MonoBehaviour
    {
        public bool isClicked { get; private set; }

        private void Start()
        {
            isClicked = false;
        }
        public virtual void OnMouseDown()
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder += 5;
            isClicked = true;
            MouseController.instance.GetMousePos(transform);
            transform.position = new Vector3(MouseController.instance.GetMouseWorldPos().x, MouseController.instance.GetMouseWorldPos().y, transform.position.z);
        }
        public virtual void OnMouseUp()
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder -= 5;
            isClicked = false;
            MouseController.instance.MouseUp(transform);
        }
        public void cancelClicked()
        {
            isClicked = false;

        }
    }
}
