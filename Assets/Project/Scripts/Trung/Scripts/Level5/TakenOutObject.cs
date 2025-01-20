using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace Trung
{
    public class TakenOutObject : ClickableObject
    {
        private Vector3 oriPos;
        private bool isOnTruePos;
        public bool isOut { get; private set; }
        private void Start()
        {
            oriPos = transform.position;
            isOnTruePos = true;
            isOut = false;
        }

        private void Update()
        {
            if(!isClicked)
            {
                if (isOnTruePos)
                {
                    MoveToTruePos();
                }
                else
                {
                    MoveDown();
                }
            }

        }

        private void MoveToTruePos()
        {
            transform.position = Vector3.MoveTowards(transform.position, oriPos, 10f*Time.deltaTime);
        }
        private void MoveDown()
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0,-10,0), 20f * Time.deltaTime);

        }

        public override void OnMouseDown()
        {
            base.OnMouseDown();
            MouseController.instance.GetMousePos(transform);
            transform.position = new Vector3(MouseController.instance.GetMouseWorldPos().x, MouseController.instance.GetMouseWorldPos().y, transform.position.z);
        }

        private void OnMouseDrag()
        {
            transform.position = new Vector3(MouseController.instance.GetMouseWorldPos().x, MouseController.instance.GetMouseWorldPos().y, transform.position.z);
        }

        public override void OnMouseUp()
        {
            base.OnMouseUp();
            MouseController.instance.MouseUp(transform);
            if(Vector3.Distance(transform.position, oriPos) < 1f)
            {
                isOnTruePos = true;
            }
            else
            {
                isOnTruePos= false;
            }
        }
        public void SetCollider(bool status)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = status;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagController tag = collision.gameObject.GetComponent<TagController>();
            if(tag != null )
            {
                if (tag.tag == "destroy")
                {
                    isOut = true;
                    if (Level5Controller.instance != null)
                    {
                        Level5Controller.instance.CheckStatus0();
                    }
                    gameObject.SetActive(false);
                }
            }

        }
    }
}
