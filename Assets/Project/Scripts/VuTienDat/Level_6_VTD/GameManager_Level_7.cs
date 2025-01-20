using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VuTienDat
{
    public class GameManager_Level_7 : MonoBehaviour
    {

        [SerializeField] private float time;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip musicClip;
        private bool isGamePause = false;

        public static GameManager_Level_7 instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            PopupManager.Open(PopupPath.POPUPUI_Level_7, LayerPopup.Main);
            UIController_Level_7.instance.InitTime();
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
