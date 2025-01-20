using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Trung
{
    public class Surf : UseableObjects
    {
        [SerializeField] private D2dDestructibleSprite soapLayer;
        [SerializeField] private GameObject task2;
        private bool canMove = false;

        private void Update()
        {
            base.OnUpdate();
            CheckAlpha();
            MoveOut();
        }
        private void CheckAlpha()
        {
            if(soapLayer.AlphaRatio < 0.05 && !isClicked)
            {
                soapLayer.gameObject.SetActive(false);
                canMove = true;
            }
        }
        private void MoveOut()
        {
            if (canMove)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(10, 0, 0) + gameObject.transform.position, 20f * Time.deltaTime);
                if (gameObject.transform.position.x > 8)
                {
                   PanMove();
                }
            }
        }
        private void PanMove()
        {
            task2.transform.position = Vector3.MoveTowards(task2.transform.position, task2.transform.position + new Vector3(0, 10, 0), 20f * Time.deltaTime);
            if (task2.transform.position.y > 10)
            {
                Level5Controller.instance.GoNextStatus();
                Level5Controller.instance.SetStatus3();
                task2.SetActive(false);
            }
        }
    }
}
