using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Trung
{
    public class LastSurf : UseableObjects
    {

        [SerializeField] private D2dDestructibleSprite soapLayer;
        private bool canMove = false;
        private void Update()
        {
            CheckAlpha();
            MoveOut();
        }
        private void CheckAlpha()
        {
            if (soapLayer.AlphaRatio < 0.05 && !isClicked)
            {
                soapLayer.gameObject.SetActive(false);
                canMove = true;
            }
        }
        private void MoveOut()
        {
            if(canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(10,0,0), 15f*Time.deltaTime);
                if(transform.position.x > 8)
                {
                    Level5Controller.instance.GoNextStatus();
                    Level5Controller.instance.SetStatus6();
                    canMove = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
