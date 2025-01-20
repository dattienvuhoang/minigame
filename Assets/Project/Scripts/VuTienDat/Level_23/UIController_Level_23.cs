using SR4BlackDev.UISystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VuTienDat;
using System.Collections;

namespace VuTienDat
{
    public class UIController_Level_23 : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtTime;
        [SerializeField] private Button btnNext, btnBack, btnHint, btnAds;
        [SerializeField] private Button btnReplay;
        private float time;
        private bool isPause = false;

        public static UIController_Level_23 instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {

            GameManager_Level_23.instance.setIsGamePause(false);
            UpdateTimerDisplay();

            btnBack.onClick.AddListener(BackLevel);
            btnNext.onClick.AddListener(NextLevel);
            btnReplay.onClick.AddListener(Reset);
            btnHint.onClick.AddListener(Hint);
        }
        private void Update()
        {
            isPause = GameManager_Level_23.instance.IsGamePause();
            if (time > 0 && !isPause)
            {
                time -= Time.deltaTime;

                time = Mathf.Max(time, 0);

                UpdateTimerDisplay();
            }
            else
            {
                GameManager_Level_23.instance.setIsGamePause(true);
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
        public void Hint()
        {
            DragController_Level23.instance.Hint();
            btnHint.enabled = false;
            StartCoroutine(EnableButoon());
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
            time = GameManager_Level_23.instance.getTime();
        }
        IEnumerator EnableButoon()
        {
            yield return new WaitForSeconds(3.5f);
            btnHint.enabled = true;
        }
    }
}
