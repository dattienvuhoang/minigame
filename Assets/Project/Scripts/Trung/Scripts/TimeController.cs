using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Trung
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private Text timeText;
        [SerializeField] private Animator timeOutAnim;
        private float time;
        private float minute;
        private float second;

        private void Start()
        {
            time = 120;
            timeOutAnim.enabled = false;
        }
        private void Update()
        {
            TimeCounter();
            ShowTimeText();
        }

        private void ShowTimeText()
        {
            minute = Mathf.Round((int)time / 60);
            second = Mathf.Round(time - minute * 60);
            timeText.text = second >= 10 ? minute + ":" + second : minute + ":0" + second;
        }
        private void TimeCounter()
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
            }
            else if (time < 0)
            {
                timeOutAnim.enabled = false;
                Debug.Log("LOSE");
                time = 0;
            }

            if(time < 5 && time > 0)
            {
                timeOutAnim.enabled = true;
            }
        }
    }
}
