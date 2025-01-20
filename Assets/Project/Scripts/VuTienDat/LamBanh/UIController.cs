using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VuTienDat_Game3_LamBanh
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtTime;
        [SerializeField] private Button btnAds, btnHint, btnNext, btnBack;
        [SerializeField] private Button btnReplay;
        private float time;
        private bool isPause = false;
        private void Start()
        {
            //time = GameManager.instance.getTime();
            time = 90;
            UpdateTimerDisplay();
            btnReplay.onClick.AddListener(Replay);
            btnNext.onClick.AddListener(NextLevel);
            btnBack.onClick.AddListener(BackLevel);
        }
        private void Update()
        {
            //isPause = GameManager.instance.IsGamePause();
            if (time > 0 && !isPause)
            {
                time -= Time.deltaTime;
                time = Mathf.Max(time, 0);
                UpdateTimerDisplay();
            }
            else
            {
                //GameManager.instance.setIsGamePause(true);
            }
        }
        private void UpdateTimerDisplay()
        {
            int minutesRemaining = Mathf.FloorToInt(time / 60);
            int secondsRemaining = Mathf.FloorToInt(time % 60);

            txtTime.text = string.Format("{0:00}:{1:00}", minutesRemaining, secondsRemaining);
        }
        public void BackLevel()
        {
            PopupManager.Close(LayerPopup.Main);
            SceneManager.LoadSceneAsync("SapXepDoVaoTuLanh");
        }
        public void NextLevel()
        {
           //GameManager.instance.CloseMusic();
            PopupManager.Close(LayerPopup.Main);
            SceneManager.LoadSceneAsync("SapXepDoVaoTuLanh");
        }
        private void Replay()
        {
            SceneManager.LoadSceneAsync("Minigame_LamBanh");
        }
    }
}
