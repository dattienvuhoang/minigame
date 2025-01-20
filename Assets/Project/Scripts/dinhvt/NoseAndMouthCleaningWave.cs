using UnityEngine;

namespace dinhvt
{
    public class NoseAndMouthCleaningWave : Wave
    {
        [Header("Start Wave")]
        [SerializeField] Collider2D faceCollider;

        public override void InitWave()
        {
            base.InitWave();

            if (missions.Count > 0) missions[0].SetCanMissionComplete(true);
        }

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            faceCollider.enabled = true;
        }
    }
}
