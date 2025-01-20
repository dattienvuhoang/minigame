using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trungggg
{
    public class TruePos : MonoBehaviour
    {
        public bool isHavingObject { get; private set; }
        public Vector3 pos;
        private void Start()
        {
            pos = transform.position;   
            isHavingObject = false;
        }

        public void SetObject(bool status)
        {
            isHavingObject = status;  
        }
    }
}
