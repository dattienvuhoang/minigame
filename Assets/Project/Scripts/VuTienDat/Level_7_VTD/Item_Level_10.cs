using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Item_Level_10 : MonoBehaviour
    {
        public int idBox;
        public int idType;
        public int idItem;
        public Position positionItem;
        public Sprite spItem, spItemFold;
        public Vector3 rotation;
        private void Start()
        {
            transform.eulerAngles = rotation;
        }
    }
}
