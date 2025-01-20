using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class UpButtonPot : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Debug.Log("up");
            LevelHotPotController.instance.UpButtonClick();
        }
    }
}
