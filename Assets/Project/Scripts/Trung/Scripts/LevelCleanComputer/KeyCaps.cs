using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class KeyCaps : MonoBehaviour
    {
        private float pushTime;
        private bool canMove;
        private float maxPushTime;
        public bool isOnTruePos { get; private set; }
        [SerializeField] private Transform truePos;
        [SerializeField] private Transform keyBox;
        private void Start()
        {
            isOnTruePos = false;
            canMove = false;
            pushTime = 0f;
            maxPushTime = 0.1f;
        }
        private void Update()
        {
            if(pushTime >= maxPushTime)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                canMove = true;
                pushTime = 0;
            }
            MoveToBox();
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            TagController tag = collision.gameObject.GetComponent<TagController>();
            if(tag != null)
            {
                if (tag.tag == "tool")
                {
                    pushTime += Time.deltaTime;
                }
            }

        }

        private void MoveToBox()
        {
            if (canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, truePos.position, 10f * Time.deltaTime);
                if(Vector3.Distance(transform.position, truePos.position) < 0.02f)
                {
                    
                    isOnTruePos = true;
                    canMove = false;
                    transform.SetParent(keyBox);
                }
            }
        }
    }
}
