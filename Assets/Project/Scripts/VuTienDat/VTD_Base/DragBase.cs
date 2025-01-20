using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuTienDat;

namespace VuTienDat_Base
{
    public class DragBase : MonoBehaviour
    {
        [Header("Drag")]
        [SerializeField] private LayerMask layerItem;
        [SerializeField] private LayerMask layerPos;
        [SerializeField] private List<GameObject> listItem, listPos;
        [SerializeField] private bool isDragging = false;
        private GameObject itemParent,itemChild;
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
                if (hit.collider!=null)
                {
                    isDragging = true;
                    itemParent = hit.collider.gameObject;
                    itemChild = itemParent.transform.GetChild(0).gameObject;
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                if (itemParent!= null)
                {
                    isDragging = false;
                    itemParent = null;
                }
            }
            if (isDragging && itemParent !=null)
            {
                Vector3 newMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Vector3 newPosition = cam.ScreenToWorldPoint(newMousePosition);
                itemParent.transform.position = new Vector3(newPosition.x, newPosition.y);
            }
        }
        private void MouseDown(float scale)
        {
            itemParent.transform.DORotate(Vector3.zero, 0.15f);
            itemParent.transform.DOScale(scale, 0.15f);
            SpriteRenderer spriteRe = itemChild.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 10;
            //spriteRe.sprite = itemParent.GetComponent<Item_Level_10>().spItemFold;
        }
        private void MouseUp()
        {
            //itemParent.transform.DORotate(itemParent.GetComponent<Item_Level_10>().rotation, 0.15f);
            itemParent.transform.DOScale(1, 0.15f);
            SpriteRenderer spriteRe = itemChild.transform.GetComponent<SpriteRenderer>();
            spriteRe.sortingOrder = 5;
           // spriteRe.sprite = itemParent.GetComponent<Item_Level_10>().spItem;
        }
    }
}
