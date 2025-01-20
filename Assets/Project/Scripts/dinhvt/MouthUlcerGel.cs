using DG.Tweening;
using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class MouthUlcerGel : DestructionTool
    {
        [Header("Mouth Ulcer Gel")]         
        [SerializeField] GameObject mouthUlcer;
        [SerializeField] float fadeTime;

        public override void CheckComplete()
        {
            if (canComplete && isComplete)
            {
                destroyedObjects[_useIndex].gameObject.SetActive(false);
                mouthUlcer.GetComponent<SpriteRenderer>().DOFade(0f, fadeTime).OnComplete(() =>
                {
                    mouthUlcer.SetActive(false);

                    CompleteMission();
                    this.PostEvent(EventID.OnMissionResult, true);
                }); 
            }
        }
    }
}
