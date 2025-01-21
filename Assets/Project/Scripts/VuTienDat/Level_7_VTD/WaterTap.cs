using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class WaterTap : MonoBehaviour
    {
        public BoxCollider2D box;
        [SerializeField] private GameObject spOn,spOff, water;
        [SerializeField] private bool isOn = true;
        [SerializeField] private List<D2dDestructibleSprite> d2d;
        public static WaterTap instance;
        private void Awake()
        {
            instance = this;
        }
        public void Tap()
        {
            if (isOn)
            {
                box.enabled = false;
                isOn = false;
                spOff.SetActive(true);
                for (int i = 0; i < d2d.Count; i++)
                {
                    d2d[i].enabled = true;
                }
                spOn.SetActive(false);
                water.SetActive(false);
            }
            else
            {
                isOn = true;
                for (int i = 0; i < d2d.Count; i++)
                {
                    d2d[i].enabled = false;
                }
                spOn.SetActive(true);
                water.SetActive(true);
            }
        }
    }
}
