using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class NoseCleaningWave : Wave
    {
        [Header("Start Wave")]   
        [SerializeField] GameObject nostril;

        [Header("End Wave")]
        [SerializeField] Wave chooseWave;

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            FadeHierarchy(nostril, 1f, duration);
        }

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            FadeHierarchy(nostril, 0f, duration);
        }

        public override void CompleteAllMission()
        {
            CompleteWave();
            waveManager.NextWave(this, chooseWave);
        }
    }
}
