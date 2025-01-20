using SR4BlackDev;
using UnityEngine;  

namespace dinhvt
{
    public class ArrangeAndPhotographWave : ArrangeItemWave
    {
        [Header("Start Wave")]
        [SerializeField] GameObject wave;

        public override void StartWave(float duration)
        {
            wave.SetActive(true);

            base.StartWave(duration);
        }

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            this.PostEvent(EventID.CameraFlash);
            missions[missions.Count - 1].gameObject.SetActive(false);
        }
    }
}
