using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace VuTienDat
{
    public class UIController_Level_4_2 : MonoBehaviour
    {
        
        [SerializeField] private TextMeshProUGUI txtTime;
        [SerializeField] private Button btnNext, btnBack, btnHint, btnAds;
        [SerializeField] private Button btnReplay;
        private float time;
        private bool isPause = false;

        public static UIController_Level_4_2 instance;
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            GlobalData.indexLevel = 3;

            GameManager_Level_4_2.instance.setIsGamePause(false);
            UpdateTimerDisplay();
/*
            btnBack.onClick.AddListener(BackLevel);
            btnNext.onClick.AddListener(NextLevel);
            btnReplay.onClick.AddListener(Reset);
            btnHint.onClick.AddListener(DragController_Level_4_2.instance.OpenHint);*/
        }
        private void Update()
        {
            isPause = GameManager_Level_4_2.instance.IsGamePause();
            if (time > 0 && !isPause)
            {
                time -= Time.deltaTime;

                time = Mathf.Max(time, 0);

                UpdateTimerDisplay();
            }
            else
            {
                GameManager_Level_4_2.instance.setIsGamePause(true);
            }
        }
        public void BackLevel()
        {
            PopupManager.Close(LayerPopup.Main);
            GlobalData.indexLevel--;
            if (GlobalData.indexLevel == 0)
            {
                GlobalData.indexLevel = 1;
            }
            SceneManager.LoadSceneAsync($"Level_{GlobalData.indexLevel}_VTD");

        }
        public void NextLevel()
        {
            PopupManager.Close(LayerPopup.Main);
            GlobalData.indexLevel++;
            if (GlobalData.indexLevel == 11)
            {
                GlobalData.indexLevel = 1;
            }
            SceneManager.LoadSceneAsync($"Level_{GlobalData.indexLevel}_VTD");

        }
        public void Reset()
        {
            DOTween.KillAll();
            PopupManager.Close(LayerPopup.Main);
            SceneManager.LoadSceneAsync($"Level_{GlobalData.indexLevel}_VTD");

        }
        private void UpdateTimerDisplay()
        {
            int minutesRemaining = Mathf.FloorToInt(time / 60);
            int secondsRemaining = Mathf.FloorToInt(time % 60);

            txtTime.text = string.Format("{0:00}:{1:00}", minutesRemaining, secondsRemaining);
        }
        public void InitTime()
        {
            time = GameManager_Level_4_2.instance.getTime();
        }
        public void AddButton()
        {
            btnBack.onClick.RemoveAllListeners();
            btnNext.onClick.RemoveAllListeners();
            btnReplay.onClick.RemoveAllListeners();
            btnHint.onClick.RemoveAllListeners();

            btnBack.onClick.AddListener(BackLevel);
            btnNext.onClick.AddListener(NextLevel);
            btnReplay.onClick.AddListener(Reset);
            btnHint.onClick.AddListener(DragController_Level_4_2.instance.OpenHint);
        }
    }
}
