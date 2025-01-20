using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class seed : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("soil2"))
            {
                
            }
        }
    }
}
