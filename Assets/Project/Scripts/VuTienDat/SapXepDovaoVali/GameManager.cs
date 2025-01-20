using DG.Tweening;
using SR4BlackDev;
using SR4BlackDev.UISystem;
using UnityEngine;
namespace VuTienDat
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private GameObject vali;
        [SerializeField] private GameObject suitscase;
        [SerializeField] private AudioClip musicClip, checkMarkClip;
        private bool isGamePause = false;
        private AudioSource audioSource;

        public static GameManager instance;

        private void Awake()
        {
            //if (instance == null)
            {
                instance = this;
            }

        }
        private void Start()
        {
            PopupManager.Open(PopupPath.POPUPUI_Vali, LayerPopup.Main);
            PanelHome.instance.InitTime();
            //Debug.Log("Tine: " + time);
            Application.targetFrameRate = 60;
            //audioSource =  AudioManager.PlayMusic(musicClip);
        }
        private void Update()
        {


        }
        public float getTime()
        {
            return time;
        }
        public bool IsGamePause() 
        {
            return isGamePause;
        }
        public void setIsGamePause(bool isPause)
        {
            this.isGamePause = isPause;
        }
        public void EndGame()
        {
            CloseMusic();
            vali.transform.DOMoveX(-10, 1f).SetEase(Ease.InBack).OnComplete(() =>
            {
                suitscase.transform.DOMoveX(0, 1f).SetEase(Ease.OutBack);
            });
            
        }
       public void CloseMusic()
        {
            AudioManager.StopSound(audioSource);
        }
        public void PlayCheckMark()
        {
            //AudioManager.PlayEffect(checkMarkClip);
        }
        public void ShowWin()
        {

        }
        public void ShowLose()
        {

        }
      
    }

}