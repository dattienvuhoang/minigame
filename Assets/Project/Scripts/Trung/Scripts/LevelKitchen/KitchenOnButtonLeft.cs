using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Trung
{
    public class KitchenOnButtonLeft : MonoBehaviour
    {
        private void OnMouseDown()
        {
            KitchenController.instance.LeftOnButton();
        }
    }
}
