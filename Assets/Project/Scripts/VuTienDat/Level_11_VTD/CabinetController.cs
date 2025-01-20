using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class CabinetController : MonoBehaviour
    {
        [SerializeField] private GameObject spClose, spOpen;
        [SerializeField] private bool isOpen = false;
        [SerializeField] private GameObject pos;
        [SerializeField] private BoxCollider2D box;
        [SerializeField] private int index = 0;
        private bool isClose = false;   
        private void Update()
        {
            if (index == pos.GetComponent<Position>().listGameObject.Count && !isClose)
            {
                isClose = true; 
                box.enabled = false;
                Open();
            }
        }
        public void Open()
        {
            if (isOpen)
            {
                isOpen = false;
                pos.SetActive(false);
                spClose.SetActive(true);
                spOpen.SetActive(false);
            }
            else
            {
                isOpen = true;
                pos.SetActive(true);
                spClose.SetActive(false);
                spOpen.SetActive(true);
            }
        }
        public void upIndex()
        {
            index++;
        }

    }
}
