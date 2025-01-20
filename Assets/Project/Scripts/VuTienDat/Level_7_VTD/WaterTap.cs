using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class WaterTap : MonoBehaviour
    {
        [SerializeField] private GameObject spOn, water;
        [SerializeField] private bool isOn = true;
        [SerializeField] private D2dDestructibleSprite d2d;
        public static WaterTap instance;
        private void Awake()
        {
            instance = this;
        }
        public void Tap()
        {
            if (isOn)
            {
                isOn = false;
                d2d.enabled = true;
                spOn.SetActive(false);
                water.SetActive(false);
            }
            else
            {
                isOn = true;
                d2d.enabled = false;
                spOn.SetActive(true);
                water.SetActive(true);
            }
        }
    }
}
