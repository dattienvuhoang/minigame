using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat_Test
{
    public class Test_Drag : MonoBehaviour
    {
        public GameObject targetGameObject;
        private void Update()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                /* Collider2D target = Physics2D.OverlapPoint(mousePosition);
                 if (target)
                 {
                     targetGameObject = target.transform.gameObject;
                 }*/
                Collider2D[] target = Physics2D.OverlapPointAll(mousePosition);
                Collider2D highestCollier = GetHighestObject(target);
                targetGameObject = highestCollier.transform.gameObject;
            }
            if (Input.GetMouseButtonUp(0))
            {
                targetGameObject = null;
            }
            if (targetGameObject)
            {
                targetGameObject.transform.position = new Vector3(mousePosition.x , mousePosition.y , 0);
            }
        }
        Collider2D GetHighestObject(Collider2D[] results)
        {
            int highestValue = 0;
            Collider2D highestObject = results[0];

            foreach (Collider2D col in results)
            {
                Renderer ren = col.gameObject.GetComponent<Renderer>();
                if (ren && ren.sortingOrder > highestValue)
                {
                    highestValue = ren.sortingOrder;
                    highestObject = col;
                }
            }

            return highestObject;
        }

    }
}
