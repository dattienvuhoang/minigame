using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat_Level1
{
    public class Item : MonoBehaviour
    {
        public int id;
        public Vector3 rotation;
        public ItemPosition position;

        private void Start()
        {
            transform.eulerAngles = rotation;
        }
    }
}
