using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class OpenDoor : MonoBehaviour
    {
        public float moveX; 
        public bool isOpen = false;
        public static OpenDoor ins;
        private void Awake()
        {
            ins = this;
        }
        private void Start()
        {
            
        }
        public void OpenOrClose()
        {
            if (isOpen)
            {
                isOpen = false;
                this.gameObject.transform.DOLocalMoveX(0, 0.3f);
            }
            else
            {
                isOpen = true;
                this.gameObject.transform.DOLocalMoveX(moveX, 0.3f);
            }
        }
    }
}
