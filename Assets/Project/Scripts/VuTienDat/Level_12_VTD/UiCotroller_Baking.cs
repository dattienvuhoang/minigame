using SR4BlackDev.UISystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace VuTienDat
{
    public class UiCotroller_Baking : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI txtTime;
        [SerializeField] private Button btnAds, btnHint, btnNext, btnBack;
        [SerializeField] private Button btnReplay;
        private float time;
        private bool isPause = false;

        public static UiCotroller_Baking instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {
            GlobalData.indexLevel = 12;
            UpdateTimerDisplay();
            GameManager_Baking.instance.setIsGamePause(false);

        }
        private void Update()
        {
            isPause = GameManager_Baking.instance.IsGamePause();
            if (time > 0 && !isPause)
            {
                time -= Time.deltaTime;

                time = Mathf.Max(time, 0);

                UpdateTimerDisplay();
            }
            else
            {
                GameManager_Baking.instance.setIsGamePause(true);
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
            if (GlobalData.indexLevel == 18)
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
        public void InitTime()
        {
            time = GameManager_Baking.instance.getTime();
        }
        public void AddButton()
        {
            btnBack.onClick.RemoveAllListeners();
            btnNext.onClick.RemoveAllListeners();
            btnReplay.onClick.RemoveAllListeners();
            btnHint.onClick.RemoveAllListeners();

            btnReplay.onClick.AddListener(Reset);
            btnNext.onClick.AddListener(NextLevel);
            btnBack.onClick.AddListener(BackLevel);
            btnHint.onClick.AddListener(DragController_Baking.instance.OpenHint);
        }
    }
    }
