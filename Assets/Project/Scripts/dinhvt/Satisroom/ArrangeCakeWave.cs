using SR4BlackDev;
using System.Collections;
using UnityEngine;

namespace dinhvt
{
    public class ArrangeCakeWave : ArrangeItemWave
    {
        [Header("Start Wave")]
        [SerializeField] GameObject wave;

        [Header("End Wave")]
        [SerializeField] Sprite tableSprite;

        public override void StartWave(float duration)
        {
            wave.SetActive(true);

            base.StartWave(duration);

            foreach (Mission mission in missions)
            {
                mission.SetCanMissionComplete(true);
            }
        }

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            StartCoroutine(TransitionWave(duration));
        }

        IEnumerator TransitionWave(float duration)
        {
            this.PostEvent(EventID.TransitionWave);

            yield return new WaitForSeconds(duration);

            wave.SetActive(false);
            if (tableSprite != null) this.PostEvent(EventID.UpdateBackground, tableSprite);
        }
    }
}
