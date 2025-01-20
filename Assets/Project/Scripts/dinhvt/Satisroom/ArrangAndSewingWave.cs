using UnityEngine;

namespace dinhvt
{
    public class ArrangAndSewingWave : ArrangeItemWave
    {
        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            foreach (Mission mission in missions)
            {
                mission.SetCanMissionComplete(true);
            }
        }

        public override void InitMissionSpriteRend() { }
    }
}
