using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class TurnOnOffFan : MonoBehaviour
    {
        [SerializeField] private GameObject fanOn, fanOff;
        [SerializeField] public bool isOn;
        [SerializeField] private BoxCollider2D box;

        public static TurnOnOffFan Instance;
        private void Awake()
        {
           
            Instance = this;
        }
        /*private void Start()
        {
            isOn = true;
        }*/
        public void OnOff()
        {
            if (isOn)
            {
                Debug.Log("On");
                isOn = false;
                fanOn.SetActive(false);
                fanOff.SetActive(true);
                box.enabled = false;
            }
            else
            {
                Debug.Log("Off");

                isOn = true;
                fanOn.SetActive(true);
                fanOff.SetActive(false);
                box.enabled = false;
            }
        }
    }
}
