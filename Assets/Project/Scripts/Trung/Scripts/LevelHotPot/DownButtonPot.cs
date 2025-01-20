using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class DownButtonPot : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Debug.Log("down");
            LevelHotPotController.instance.DownButtonClick();
        }
    }
}
