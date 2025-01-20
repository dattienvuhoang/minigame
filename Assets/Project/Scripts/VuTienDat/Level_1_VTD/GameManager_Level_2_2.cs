using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class GameManager_Level_2_2 : MonoBehaviour
    {

        public float time;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip musicClip;
        private bool isGamePause = false;

        public static GameManager_Level_2_2 instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            PopupManager.Open(PopupPath.POPUPUI_Level_2_2, LayerPopup.Main);
            UIController_Level_2_2.instance.InitTime();
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
