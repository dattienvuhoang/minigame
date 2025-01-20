using SR4BlackDev.UISystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace VuTienDat
{
    public class UIController_Level_37 : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtTime;
        [SerializeField] private Button btnAds, btnHint, btnNext;
        [SerializeField] private Button btnReplay;
        private float time;
        private bool isPause = false;

        public static UIController_Level_37 instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {
            UpdateTimerDisplay();
            GameManager_Level_37.instance.setIsGamePause(false);

            btnReplay.onClick.AddListener(Replay);
            btnNext.onClick.AddListener(NextLevel);
        }
        private void Update()
        {
            isPause = GameManager_Level_37.instance.IsGamePause();
            if (time > 0 && !isPause)
            {
                time -= Time.deltaTime;

                time = Mathf.Max(time, 0);

                UpdateTimerDisplay();
            }
            else
            {
                GameManager_Level_37.instance.setIsGamePause(true);
            }
        }
        private void UpdateTimerDisplay()
        {
            int minutesRemaining = Mathf.FloorToInt(time / 60);
            int secondsRemaining = Mathf.FloorToInt(time % 60);

            txtTime.text = string.Format("{0:00}:{1:00}", minutesRemaining, secondsRemaining);
        }
        public void NextLevel()
        {
            //GameManager_Baking.instance.CloseMusic();
            PopupManager.Close(LayerPopup.Main);
            SceneManager.LoadSceneAsync("SapXepDoVaoTuLanh");
        }
        private void Replay()
        {
            PopupManager.Close(LayerPopup.Main);
            SceneManager.LoadSceneAsync("Minigame_Vali");
        }
        public void InitTime()
        {
            time = GameManager_Level_37.instance.getTime();
        }
    }
}
