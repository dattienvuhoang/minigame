using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class RottenGlass : MonoBehaviour
    {
        //[SerializeField] private GameObject stain;
        [SerializeField] private GameObject spOn, spOff;
        public BoxCollider2D box;
        private bool isOn = true;

        public static RottenGlass instance;
        private void Awake()
        {
            instance = this;
        }

        public void OnOff()
        {
            if (isOn)
            {
                box.enabled = false;
                isOn = false;
                spOn.SetActive(false);
                spOff.SetActive(true);
                //stain.SetActive(false);
            }
            else
            {
                isOn = true;
                spOff.SetActive(false);
                spOn.SetActive(true);
                //stain.SetActive(true);
            }
        }
    }
}
