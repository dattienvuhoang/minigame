using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuTienDat_Game2;

namespace VuTienDat
{
    public class GameManager_level_1_2 : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip musicClip;
        private bool isGamePause = false;

        public static GameManager_level_1_2 instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            PopupManager.Open(PopupPath.POPUPUI_Level_1_2, LayerPopup.Main);
            UIController_level_1_2.instance.InitTime();
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
