using UnityEngine;

namespace dinhvt
{
    public class FaceCleansingWave : Wave
    {
        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            if (missions.Count > 0) missions[0].SetCanMissionComplete(true);
        }

        public override void EndWave(float duration)
        {
        }
    }
}
