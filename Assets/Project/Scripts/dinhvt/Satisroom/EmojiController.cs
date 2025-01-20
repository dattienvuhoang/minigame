using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class EmojiController : MonoBehaviour
    {
        private const string playTrigger = "Play";

        [SerializeField] Animator trueAnimator;   
        [SerializeField] Animator falseAnimator;

        private void OnEnable()
        {
            this.RegisterListener(EventID.OnMissionResult, PlayEmojiAnimator);
        }

        private void OnDisable()
        {
            this.RemoveListener(EventID.OnMissionResult, PlayEmojiAnimator);
        }

        public void PlayEmojiAnimator(object sender, object result)
        {
            if ((bool) result)
            {
                trueAnimator.SetTrigger(playTrigger);
            }
            else
            {
                falseAnimator.SetTrigger(playTrigger);
            }
        }
    }
}
