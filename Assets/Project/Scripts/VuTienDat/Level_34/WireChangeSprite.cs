using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class WireChangeSprite : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D boxWire, boxBootle, boxTray;
        [SerializeField] private GameObject wireOn, wireOff, lightOn;
       
        public void ChangeOff()
        {
            wireOn.SetActive(false);
            wireOff.SetActive(true);
            lightOn.SetActive(false);
            boxWire.enabled = false;
            boxBootle.enabled = true;
            boxTray.enabled = true;
        }
        public void ChangeOn()
        {
            lightOn.SetActive(true);
            boxWire.enabled = false;
        }
    }
}
