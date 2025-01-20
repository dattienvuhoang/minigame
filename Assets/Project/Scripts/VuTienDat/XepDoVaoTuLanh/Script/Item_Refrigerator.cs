using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuTienDat_Game2;

namespace VuTienDat
{
    public class Item_Refrigerator : MonoBehaviour
    {
        public int id;
        public string nameItem;
        public Sprite item, itemFold;
        public Vector3 rotation;
        public ItemPosition position;
        public float scale;

        private void Start()
        {
            transform.eulerAngles = rotation;
            scale = transform.GetChild(0).transform.localScale.x;
        }

    }
}
