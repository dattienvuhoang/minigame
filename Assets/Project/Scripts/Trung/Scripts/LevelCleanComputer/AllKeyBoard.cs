using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class AllKeyBoard : MonoBehaviour
    {
        [SerializeField] private List<KeyCaps> keyCaps;
        [SerializeField] private GameObject glass;

        private void Start()
        {
            glass.SetActive(false);
        }


        public bool CheckKeyCap()
        {
            foreach(var keyCap in keyCaps)
            {
                if (!keyCap.isOnTruePos)
                {
                    return false;
                }
            }
            glass.SetActive(true);
            LevelCleanComputerController.instance.GoNextStatus();
            return true;
        }
    }
}
