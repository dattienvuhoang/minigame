using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class LotusTruePos : MonoBehaviour
    {
        public bool haveLotus { get; private set; }
        public Vector3 pos { get; private set; }

        private void Start()
        {
            haveLotus = false;
            pos = gameObject.transform.position;
        }

        public void SetLotus(bool status)
        {
            haveLotus = status;
        }
    }
}
