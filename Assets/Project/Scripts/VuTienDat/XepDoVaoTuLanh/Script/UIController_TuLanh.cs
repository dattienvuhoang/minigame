using SR4BlackDev.UISystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace VuTienDat
{
    public class UIController_TuLanh : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI txtTime;
        [SerializeField] private Button btnNext, btnBack, btnHint, btnAds;
        [SerializeField] private Button btnReplay;
        private float time;
        private bool isPause = false;

        public static UIController_TuLanh instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {

            GameManager_Ref.instance.setIsGamePause(false);
            UpdateTimerDisplay();
/*
            btnBack.onClick.AddListener(BackLevel);
            btnNext.onClick.AddListener(NextLevel);
            btnReplay.onClick.AddListener(Reset);*/
        }
        private void Update()
        {
            isPause = GameManager_Ref.instance.IsGamePause();
            if (time > 0 && !isPause)
            {
                time -= Time.deltaTime;

                time = Mathf.Max(time, 0);

                UpdateTimerDisplay();
            }
            else
            {
                GameManager_Ref.instance.setIsGamePause(true);
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
            if (GlobalData.indexLevel == 19)
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
        public void AddButton()
        {
            btnBack.onClick.RemoveAllListeners();
            btnNext.onClick.RemoveAllListeners();
            btnReplay.onClick.RemoveAllListeners();
            btnHint.onClick.RemoveAllListeners();

            btnBack.onClick.AddListener(BackLevel);
            btnNext.onClick.AddListener(NextLevel);
            btnReplay.onClick.AddListener(Reset);
            btnHint.onClick.AddListener(DragController_Refrigerator.instance.OpenHint);
        }
        public void InitTime()
        {
            time = GameManager_Ref.instance.getTime();
        }
    }
}
