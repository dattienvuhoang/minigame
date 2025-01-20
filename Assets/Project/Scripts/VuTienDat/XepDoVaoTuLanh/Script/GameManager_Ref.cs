using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuTienDat_Game2;

namespace VuTienDat
{
    public class GameManager_Ref : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip musicClip;
        private bool isGamePause = false;

        public static GameManager_Ref instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            PopupManager.Open(PopupPath.POPUPUI_TuLanh, LayerPopup.Main);
            UIController_TuLanh.instance.InitTime();
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
