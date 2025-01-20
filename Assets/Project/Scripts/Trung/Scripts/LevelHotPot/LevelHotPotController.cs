using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Trung
{
    public class LevelHotPotController : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D upButton;
        [SerializeField] private BoxCollider2D downButton;
        [SerializeField] private Text levelText;
        [SerializeField] private GameObject smoke;
        private int electricLevel;

        public static LevelHotPotController instance;

        private void Awake()
        {
            if(instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }
        private void Start()
        {
            electricLevel = 1;
            levelText.text = $"P{electricLevel}";
            smoke.SetActive(false);
            upButton.enabled = false;
            downButton.enabled = false;

            Application.targetFrameRate = 60;
            PopupManager.Open(PopupPath.MainPopUpTrung, LayerPopup.Main);
        }

        private void Update()
        {
            if (AllArangeObjects.instance.isWon && !upButton.enabled)
            {
                upButton.enabled = true;
                downButton.enabled = true;
            }
            if(electricLevel == 6)
            {
                FeelingTool.instance.FadeInImplement(smoke);
                AllArangeObjects.instance.ShowWinToast();
            }
        }


        public void UpButtonClick()
        {
            if(electricLevel < 6)
            {
                electricLevel++;
                levelText.text = $"P{electricLevel}";
            }
        }
        public void DownButtonClick()
        {
            if (electricLevel > 1)
            {
                electricLevel--;
                levelText.text = $"P{electricLevel}";
            }
        }
    }
}
