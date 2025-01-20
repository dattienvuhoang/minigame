using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class ItemChristmas : MonoBehaviour
    {
        public Collider2D box;
        public List<GameObject> listPos;
        public float distance;
        public Vector3 rotation;
        public bool isMove = false;
        private void Start()
        {
            transform.eulerAngles = rotation;
        }
        private void Update()
        {
            
        }
        public void MoveToPos()
        {
            int count  = listPos.Count;
                for (int i = 0; i < count; i++)
                {
                if (listPos[i] != null) 
                    if (Vector3.Distance(transform.position, listPos[i].transform.position) < distance)
                    {
                        box.enabled = false;
                        isMove = true;
                        Vector3 pos = listPos[i].transform.position;
                        Destroy(listPos[i]);
                        transform.DORotate(Vector3.zero, 0.15f);
                        transform.DOScale(1, 0.15f);
                        transform.DOMove(pos, 0.15f);
                    }
                }
        }
    }
}
