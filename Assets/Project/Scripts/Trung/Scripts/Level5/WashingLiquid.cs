using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace Trung
{
    public class WashingLiquid : UseableObjects
    {
        [SerializeField] private GameObject spray;
        [SerializeField] private D2dDestructibleSprite dirtyLayer;
        [SerializeField] private D2dDestructibleSprite soapLayer;
        [SerializeField] private GameObject cleaner;

        private bool canMove = false;
        private void Start()
        {
            spray.SetActive(false);
        }

        private void Update()
        {
            base.OnUpdate();
            CheckAlpha();
            MoveOut();
        }

        private void CheckAlpha()
        {
            if(dirtyLayer.AlphaRatio < 0.05 && !isClicked)
            {
                dirtyLayer.gameObject.SetActive(false);
                canMove = true;
            }
        }
        public override void OnMouseDown()
        {
            dirtyLayer.enabled = true;
            soapLayer.enabled = false;
            base.OnMouseDown();
            spray.SetActive(true);
        }

        public override void OnMouseUp()
        {
            base.OnMouseUp();
            spray.SetActive(false);
        }

        private void MoveOut()
        {
            if (canMove)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(10, 0, 0) + gameObject.transform.position, 20f * Time.deltaTime);
                if (gameObject.transform.position.x > 8)
                {
                    cleaner.SetActive(true);
                    soapLayer.enabled = true;
                    gameObject.SetActive(false);
                }
            }
        }
        
        //private void PanMove()
        //{
        //    task3.transform.position = Vector3.MoveTowards(task3.transform.position, task3.transform.position + new Vector3(0, 10, 0), 20f * Time.deltaTime);
        //    if(task3.transform.position.y > 10)
        //    {
        //        Level5Controller.instance.GoNextStatus();
        //        task3.SetActive(false);
        //    }
        //}

    }
}
