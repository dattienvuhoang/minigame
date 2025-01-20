using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Screws : MonoBehaviour
    {
        private bool canMove;
        public bool isOut { get; private set; }

        private void Start()
        {
            canMove = false;
            isOut = false;
        }
        private void Update()
        {
            Move();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagController tag = collision.gameObject.GetComponent<TagController>();
            if(tag != null)
            {
                if (tag.tag == "screwdriver")
                {
                    canMove = true;
                }
            }
        }
        private void Move()
        {
            if(canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-10, 0, 0), 15f * Time.deltaTime);
                if(Vector3.Distance(transform.position, new Vector3(-10, 0, 0)) < 0.02f){
                    canMove = false;
                    isOut = true;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
