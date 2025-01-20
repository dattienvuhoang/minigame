using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class EnableBoxBin : MonoBehaviour
    {
        public Collider2D box;
        public static EnableBoxBin ins;
        private void Awake()
        {
            ins = this;
        }
        private void Start()
        {
            
        }
        public void EnableBox ()
        {
            box.enabled = true;
        }
    }
}
