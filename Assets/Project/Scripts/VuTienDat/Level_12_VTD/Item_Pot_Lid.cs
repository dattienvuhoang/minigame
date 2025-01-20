using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Item_Pot_Lid : MonoBehaviour
    {
        public GameObject truePos;
        public BoxCollider2D box;
        private Vector3 lastPos;
        private void Start()
        {
            lastPos = transform.position;
        }
        private void Update()
        {
            
        }
    }
}
