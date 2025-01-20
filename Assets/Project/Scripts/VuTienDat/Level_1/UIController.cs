using SR4BlackDev.UISystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace VuTienDat_Level1
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtTime;
        [SerializeField] private Button btnNext, btnBack, btnHint, btnAds;
        [SerializeField] private Button btnReplay;
        private float time;
        private bool isPause = false;

        public static UIController instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {

            GameManager.instance.setIsGamePause(false);
            UpdateTimerDisplay();

            btnBack.onClick.AddListener(BackLevel);
            btnNext.onClick.AddListener(NextLevel);
            btnReplay.onClick.AddListener(Reset);
        }
        private void Update()
        {
            isPause = GameManager.instance.IsGamePause();
            if (time > 0 && !isPause)
            {
                time -= Time.deltaTime;

                time = Mathf.Max(time, 0);

                UpdateTimerDisplay();
            }
            else
            {
                GameManager.instance.setIsGamePause(true);
            }
        }
        public void BackLevel()
        {
            PopupManager.Close(LayerPopup.Main);
            SceneManager.LoadSceneAsync("Minigame_Vali");

        }
        public void NextLevel()
        {
            PopupManager.Close(LayerPopup.Main);
            SceneManager.LoadSceneAsync("Minigame_LamBanh");
        }
        public void Reset()
        {
            PopupManager.Close(LayerPopup.Main);
            SceneManager.LoadSceneAsync("SapXepDoVaoTuLanh");
        }
        private void UpdateTimerDisplay()
        {
            int minutesRemaining = Mathf.FloorToInt(time / 60);
            int secondsRemaining = Mathf.FloorToInt(time % 60);

            txtTime.text = string.Format("{0:00}:{1:00}", minutesRemaining, secondsRemaining);
        }
        public void InitTime()
        {
            time = GameManager.instance.getTime();
        }
    }
}
