using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class KeyBoard : MonoBehaviour
    {
        private void OnMouseDown()
        {
            LevelCleanComputerController.instance.SetMove1();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
