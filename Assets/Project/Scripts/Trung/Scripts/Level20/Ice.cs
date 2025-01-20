using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Ice : MonoBehaviour
    {
        private bool onPos;
        private Vector3 pos;
        private void Start()
        {
            pos = transform.position + new Vector3(0,-1f,0);
            onPos = false;
        }
        private void Update()
        {
            Drop();
        }
        private void Drop()
        {
            if (!onPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, 4f * Time.deltaTime);
                if(Vector3.Distance(transform.position, pos) < 0.02)
                {
                    onPos = true;
                }
            }
        }
    }
}
