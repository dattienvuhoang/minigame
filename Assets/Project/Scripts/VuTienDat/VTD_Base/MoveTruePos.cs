using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class MoveTruePos : MonoBehaviour
    {
        public Sprite sp, spFold;
        public ListPos listPos;
        public float distance;
        public Collider2D box;
        public bool isMove = true;
        public int sortingLayer;
        Vector3 rotation;
        float scale;
        Vector3 move = Vector3.zero;
        float dis = 100;
        bool isMoveToPos = false;
        private void Start()
        {
            dis = 100;
            box = GetComponent<Collider2D>();
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
                    transform.GetChild(0).transform.DOScale(scale, 0.15f);
                }
                transform.DORotate(rotation, 0.15f);
                Debug.Log("True");
            }
            else
            {
                Debug.Log("False");

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
                    if (Vector3.Distance(listPos.listPos[i].transform.position, transform.position) < dis)
                    {
                        dis = Vector3.Distance(listPos.listPos[i].transform.position, transform.position);
                        move = listPos.listPos[i].transform.position;
                        scale = listPos.scaleList[i];
                        rotation = listPos.rotationList[i];
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
                listPos.scaleList.Remove(listPos.scaleList[index]);
                listPos.rotationList.Remove(listPos.rotationList[index]);
            }

            return isMoveToPos;
        }

    }
}
