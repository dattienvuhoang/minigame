using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class GameManager_Level_37 : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip musicClip;
        private bool isGamePause = false;

        public static GameManager_Level_37 instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            PopupManager.Open(PopupPath.POPUPUI_Level_37, LayerPopup.Main);
            UIController_Level_37.instance.InitTime();
        }
        public float getTime()
        {
            return time;
        }
        public bool IsGamePause()
        {
            return isGamePause;
        }
        public void setIsGamePause(bool isPause)
        {
            this.isGamePause = isPause;
        }
    }
}
