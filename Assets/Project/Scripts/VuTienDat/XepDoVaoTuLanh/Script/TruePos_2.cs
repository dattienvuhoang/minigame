using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class TruePos_2 : MonoBehaviour
    {
        public Sprite sp, spFold;
        public ListPos listPos;
        public float distance;
        public Collider2D box;
        public bool isMove = true;
        public int sortingLayer;
        public float scale;
        Vector3 move = Vector3.zero;
        float dis = 100;
        bool isMoveToPos = false;   
        private void Start()
        {
            //box = this.GetComponent<Collider2D>();
            dis = 100;
        }
        public void Move()
        {
            if (isMoveToPos)
            {
                isMoveToPos = false;
                isMove = false;
                transform.DOMove(move, 0.15f);
                box.enabled = false;
                if (transform.childCount != 0)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = sortingLayer;
                    transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spFold;
                }
                transform.DORotate(Vector3.zero, 0.15f);
                transform.DOScale(scale, 0.15f);
            }
            else
            {
                isMove = true;
            }
           
        }
        public bool Check()
        {
            int index = -100;
            for (int i = 0; i < listPos.listPos.Count; i++)
            {
                if (Vector3.Distance(listPos.listPos[i].transform.position, transform.position) < distance)
                {
                    Debug.Log("1111111111");
                    if (Vector3.Distance(listPos.listPos[i].transform.position, transform.position) < dis)
                    {
                        Debug.Log("22222222222");
                        dis = Vector3.Distance(listPos.listPos[i].transform.position, transform.position);
                        move = listPos.listPos[i].transform.position;
                        isMoveToPos = true;
                        index = i;
                        break;
                    }
                }
            }
            Debug.Log("Dis : " + dis);
            Debug.Log("Vector: " + move);
            if (index != -100)
            {
                listPos.listPos.Remove(listPos.listPos[index]);
            }

            return isMoveToPos;
        }
    }
}
