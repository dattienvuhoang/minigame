using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class TruePos : MonoBehaviour
    {
        public Vector3 truePos;
        public float distance;
        public Collider2D box;
        public bool isMove = true;
        public int sortingLayer;

        private void Start()
        {
            box = this.GetComponent<Collider2D>();
        }
        public void Move()
        {
            if (Vector3.Distance(truePos, transform.position) < distance)
            {
                isMove = false;
                transform.DOMove(truePos, 0.15f);
                box.enabled = false;
                if (transform.childCount != 0)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = sortingLayer;
                }
            }
            else
            {
                isMove = true;
            }

        }
    }
}
