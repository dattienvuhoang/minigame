using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat_Game3_LamBanh
{
    public class DragController : MonoBehaviour
    {
        [Header("Game Object")]
        [SerializeField] private GameObject itemParent, itemChild;

        [Header("List Game Object")]
        [SerializeField] private GameObject item;
        [SerializeField] private GameObject listItem, listPos;
        [SerializeField] private GameObject listFish;
        [SerializeField] private GameObject listSpriteMaskFish;

        [Header("Layer")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerPos;



        private bool isDragging = false;
        private Camera cam;
        private void Start()
        {
            cam = Camera.main;
        }
        private void Update()
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity, layerItem);
                if (hit.collider != null)
                {
                    itemChild = hit.collider.gameObject;
                    itemParent = itemChild.transform.parent.gameObject;
                    if (itemParent != null)
                    {
                        isDragging = true;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (itemParent != null)
                {
                    isDragging = false;
                    itemParent = null; 
                    itemChild = null;
                }
            }
            if (isDragging)
            {
                itemParent.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            }
        }
    }
}
