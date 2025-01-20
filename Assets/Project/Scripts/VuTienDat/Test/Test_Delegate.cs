using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Test_Delegate : MonoBehaviour
    {
        delegate void Move();
        Move mov;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (mov == null)
                {
                    Debug.Log("null");
                }
                else
                {
                    mov();
                }
            }
            if ( Input.GetKeyUp(KeyCode.A))
            {
                mov = Print_a;
            }
            if ( Input.GetKeyUp(KeyCode.B))
            {
                mov = Print_b;
            }
        }

        private void Print_a()
        {
            Debug.Log("A");
        }
        private void Print_b()
        {
            Debug.Log("b");
        }
    }
}
