using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace dinhvt
{
    public class ArrangeFoodWave : ArrangeItemWave
    {
        [Header("End Wave")]
        [SerializeField] GameObject[] fruits;

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            for (int i = 0; i < missions.Count; i++)
            {
                missions[i].SetCanMissionComplete(true);
            }
        }

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            foreach (GameObject fruit in fruits)
            {
                fruit.SetActive(false);
            }
        }
    }
}
