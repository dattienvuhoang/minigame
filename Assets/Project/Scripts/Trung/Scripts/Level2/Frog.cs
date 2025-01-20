using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Frog : MonoBehaviour
    {
        [SerializeField] private GameObject water;
        [SerializeField] private SpriteRenderer cleanWater;
        private bool showWater = false;
        public float fadeDuration = 2f;
        private float currentTime = 0f;

        private Level2Controller level2Controller;
        private Level2Controller Level2Controller
        {
            get
            {
                if (level2Controller == null)
                {
                    level2Controller = GameObject.Find("GameController").GetComponent<Level2Controller>();
                }
                return level2Controller;
            }
            set
            {
                level2Controller = value;
            }
        }
        private void OnMouseDown()
        {
            if(Level2Controller.status == 5)
            {
                Debug.Log("chay nuoc");
                water.SetActive(true);
                showWater = true;
            }
        }
        private void Update()
        {
            if (showWater)
            {
                ShowWater(); 
            }
        }

        private void ShowWater()
        {
            if (currentTime < fadeDuration)
            {
                Debug.Log("Show");
                currentTime += Time.deltaTime;

                float lerpRatio = currentTime / fadeDuration;

                Color color = cleanWater.color;
                color.a = Mathf.Lerp(0f, 1f, lerpRatio);
                cleanWater.color = color;
                if (lerpRatio >= 1f)
                {
                    Debug.Log("done");
                    showWater = false;
                    water.SetActive(false);
                    PopupManager.ShowToast("WIN");
                }
            }
        }
    }
}
