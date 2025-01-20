using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuTienDat;

namespace VuTienDat_Game2
{
    public class DrawerController : MonoBehaviour
    {
        [SerializeField] int id;
        [SerializeField] private Vector3 posInit, posLast;
        public bool isOpen = false;
        public CheckOpen check;
        private int leng;

        private void Start()
        {
            posInit = transform.localPosition;
        }
        private void Update()
        {
            /*leng = DragController.instance.getListItem();
            if (leng == 1)
            {
                if (!isOpen)
                {
                    DragController.instance.RemoveDrawer(this.gameObject);
                }
                else
                {
                    DragController.instance.AddDrawer(this.gameObject);
                }
            }*/
        }
        public void CheckOpen()
        {
            if (isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
        public void Open()
        {
            this.transform.DOLocalMoveX(posLast.x, 0.3f).SetEase(Ease.Linear);
            isOpen = true;
            check.isOpen = true;
            DragController_Refrigerator.instance.AddDoor(this.gameObject);
        }
        public void Close()
        {
            transform.DOLocalMoveX(posInit.x, 0.3f).SetEase(Ease.Linear);
            isOpen = false;
            check.isOpen = false;
            DragController_Refrigerator.instance.RemoveDoor(this.gameObject);

        }
    }
}
