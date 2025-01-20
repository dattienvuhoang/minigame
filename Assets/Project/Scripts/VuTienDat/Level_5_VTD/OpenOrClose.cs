using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class OpenOrClose : MonoBehaviour
    {
        [SerializeField] private GameObject open, close;
        [SerializeField] public bool isOn = true;
        [SerializeField] private BoxCollider2D box;

        public void OnOff()
        {
            if (isOn)
            {
                isOn = false;
                open.SetActive(false);
                close.SetActive(true);
                box.enabled = false;
            }
            else
            {
                isOn = true;
                open.SetActive(true);
                close.SetActive(false);
                box.enabled = false;
            }
        }
    }
}
