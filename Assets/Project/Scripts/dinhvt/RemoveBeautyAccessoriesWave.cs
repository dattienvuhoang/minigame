using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class RemoveBeautyAccessoriesWave : Wave
    {
        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            this.PostEvent(EventID.OnMissionResult, true);
        }
    }
}
