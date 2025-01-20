using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class RotationController : MonoBehaviour
    {
        public Vector3 rotation;
        private void Start()
        {
            transform.eulerAngles = rotation;
        }
    }
}
