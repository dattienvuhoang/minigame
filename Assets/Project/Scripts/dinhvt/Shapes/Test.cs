using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class Test : MonoBehaviour
    {
        public int _count;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _count++;
            Debug.Log(collision.name);
        }
    }
}
