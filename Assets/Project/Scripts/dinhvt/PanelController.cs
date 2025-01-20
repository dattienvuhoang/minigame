using DG.Tweening;
using SR4BlackDev;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace dinhvt
{
    public class PanelController : MonoBehaviour
    {
        [SerializeField] Image blackImg;
        [Space(20)]
        [SerializeField] Image whiteImg;
        [SerializeField] GameObject phoneImg;

        private void Awake()
        {
            this.RegisterListener(EventID.TransitionWave, Fade);
            this.RegisterListener(EventID.CameraFlash, Flash);
        }

        private void OnDestroy()
        {
            this.RemoveListener(EventID.TransitionWave, Fade);
            this.RemoveListener(EventID.CameraFlash, Flash);
        }

        public void Fade(object sender, object param)
        {
            blackImg.gameObject.SetActive(true);
            blackImg.DOFade(1f, 1f).OnComplete(() =>
            {
                blackImg.DOFade(0f, 1f).OnComplete(()=>
                {
                    blackImg.gameObject.SetActive(false);
                });
            });
        }

        public void Flash(object sender, object param)
        {
            whiteImg.gameObject.SetActive(true);
            whiteImg.DOFade(1f, 0.3f).OnComplete(() =>
            {
                phoneImg.SetActive(true);
                whiteImg.DOFade(0f, 0.3f).OnComplete(() =>
                {
                    whiteImg.gameObject.SetActive(false);
                });
            });
        }
    }
}
