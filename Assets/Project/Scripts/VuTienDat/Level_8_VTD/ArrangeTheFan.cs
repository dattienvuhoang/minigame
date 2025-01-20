using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class ArrangeTheFan : MonoBehaviour
    {
        public List<GameObject> listTruePos;
        public int indexItem;
        public bool isDone = false;

        public static ArrangeTheFan instance;
        private void Awake()
        {
            instance = this;
        }
        public void CheckOject(GameObject gameObject)
        {
            if (listTruePos.IndexOf(gameObject) == indexItem)
            {

            }
        }
    }
}
