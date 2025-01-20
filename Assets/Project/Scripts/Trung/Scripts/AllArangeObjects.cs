using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class AllArangeObjects : MonoBehaviour
    {
        public static AllArangeObjects instance;
        [SerializeField] List<ArrangeObject> aggangeObjects;
        private bool isShown;
        public bool isWon { get; private set; }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }
        private void Start()
        {
            isWon = false;
            isShown = false;
        }
        public bool CheckWinCondition()
        {
            if (!isWon)
            {
                foreach (var obj in aggangeObjects)
                {
                    if (!obj.isOnTruePos)
                    {
                        return false;
                    }
                }
                isWon = true;

                return true;
            }
            return false;
        }
        public void ShowWinToast()
        {
           if(!isShown)
            {
                Debug.Log("Win");
                PopupManager.ShowToast("WIN");
                isShown = true;
            }
        }
    }
}
