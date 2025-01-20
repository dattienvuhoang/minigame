using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Item_Level_7 : MonoBehaviour
    {
        public int id;
        public Vector3 rotation;
        //public ItemPosition position;

        private void Start()
        {
            transform.eulerAngles = rotation;
        }
    }
}
