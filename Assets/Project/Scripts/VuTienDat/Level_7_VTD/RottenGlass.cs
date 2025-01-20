using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class RottenGlass : MonoBehaviour
    {
        [SerializeField] private GameObject stain;
        [SerializeField] private GameObject spOn, spOff;
        private bool isOn = false;

        public static RottenGlass instance;
        private void Awake()
        {
            instance = this;
        }

        public void OnOff()
        {
            if (isOn)
            {
                isOn = false;
                spOn.SetActive(false);
                spOff.SetActive(true);
                stain.SetActive(false);
            }
            else
            {
                isOn = true;
                spOff.SetActive(false);
                spOn.SetActive(true);
                stain.SetActive(true);
            }
        }
    }
}
