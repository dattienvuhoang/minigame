using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Cup : MonoBehaviour
    {
        [SerializeField] private Transform cupPostion;
        private bool canMove;
        private void Start()
        {
            canMove = false;
        }

        private void OnMouseDown()
        {
            canMove = true;
            GetComponent<BoxCollider2D>().enabled = false;
            Level20Controller.instance.GoNextStatus();
        }
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if(canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, cupPostion.position, 10f * Time.deltaTime);
                if(Vector3.Distance(transform.position, cupPostion.position) < 0.02)
                {
                    canMove = false;                 
                }
            }
        }
    }
}
