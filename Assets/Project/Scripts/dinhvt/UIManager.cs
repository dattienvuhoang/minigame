using SR4BlackDev;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace dinhvt
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] LogicDrag logicDrag;
        [SerializeField] HeathBarController healthbar;

        [Header("Button")]
        public Button nextBtn;
        public Button backBtn;
        public Button reloadBtn;
        public List<Button> levelBtns = new List<Button>();
        public List<Text> levelTexts = new List<Text>();

        public Text logText;

        private void OnEnable()
        {
            this.RegisterListener(EventID.Log, UpdateText);
        }

        private void OnDisable()
        {
            this.RemoveListener(EventID.Log, UpdateText);
        }

        public void UpdateText(object sender, object result)
        {
            logText.text = result.ToString();
        }

        private void Awake()
        {
            nextBtn.onClick.AddListener(NextLevel);
            backBtn.onClick.AddListener(BackLevel);
            reloadBtn.onClick.AddListener(Reload);

            foreach (Button b in levelBtns)
            {
                b.onClick.AddListener(() =>
                {
                    LoadLevel(b);
                });
            }

            int sceneIdx = SceneManager.GetActiveScene().buildIndex;
            UpdateLevelIdx(sceneIdx);
        }

        void Reload()
        {
            int sceneIdx = SceneManager.GetActiveScene().buildIndex;
            healthbar.ResetHealth();
            logicDrag.canDrag = true;
            SceneManager.LoadScene(sceneIdx);

            logText.text = "";
        }

        void LoadLevel(Button btn)
        {
            int levelIdx = levelBtns.IndexOf(btn);
            healthbar.ResetHealth();
            logicDrag.canDrag = true;
            SceneManager.LoadScene(levelIdx);
            UpdateLevelIdx(levelIdx);

            logText.text = "";
        }

        void NextLevel()
        {
            healthbar.ResetHealth();
            logicDrag.canDrag = true;

            int sceneIdx = SceneManager.GetActiveScene().buildIndex;
            int levelIdx = (sceneIdx + 1) % levelBtns.Count;
            SceneManager.LoadScene(levelIdx);
            UpdateLevelIdx(levelIdx);

            logText.text = "";
        }

        void BackLevel()
        {
            healthbar.ResetHealth();
            logicDrag.canDrag = true;

            int sceneIdx = SceneManager.GetActiveScene().buildIndex;
            int levelIdx = (sceneIdx + levelBtns.Count - 1) % levelBtns.Count;
            SceneManager.LoadScene(levelIdx);
            UpdateLevelIdx(levelIdx);

            logText.text = "";
        }

        void UpdateLevelIdx(int levelIdx)
        {
            for (int i = 0; i < levelTexts.Count; i++)
            {
                if (i == levelIdx) levelTexts[i].fontSize = 60;
                else levelTexts[i].fontSize = 40;
            }
        }

        void OnDestroy()
        {
            reloadBtn.onClick.RemoveListener(Reload);
        }
    }
}
