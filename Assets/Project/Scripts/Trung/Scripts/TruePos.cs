using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class TruePos : MonoBehaviour
    {
        public bool isHavingObject { get; private set; }
        public Vector3 pos;
        private void Start()
        {
            UpdatePosition();
            isHavingObject = false;
        }

        public virtual void SetObject(bool status)
        {
            isHavingObject = status;  
        }

        public void UpdatePosition() 
        {
            pos = transform.position;
        }
    }
}
