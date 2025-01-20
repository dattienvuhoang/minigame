using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HeartsHealthVisual;

namespace Trung
{
    public class LifeController : MonoBehaviour
    {
        private int heart;
        [SerializeField] private List<GameObject> heartImages;
        [SerializeField] private GameObject loseHeartAnim;
        [SerializeField] private GameObject angryEmoji;

        public static LifeController instance;
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
            angryEmoji.SetActive(false);
            heart = 5;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K)) 
            {
                LoseHeart();
            }
        }
        public void LoseHeart()
        {
            if(heart > 0)
            {
                heart--;
                heartImages[heartImages.Count - 1].SetActive(false);
                heartImages.RemoveAt(heartImages.Count - 1);
                loseHeartAnim.SetActive(true);
                angryEmoji.SetActive(true);
                if (heart == 0)
                {
                    Debug.Log("LOSE");
                }
            }           
        }
    }
}
