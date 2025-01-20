using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class WashTheHairWave : Wave
    {
        [Header("Start Wave")]
        [SerializeField] SpriteRenderer hair;
        [SerializeField] GameObject hairs;

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            hair.DOFade(1f, duration).OnComplete(() =>
            {
                hairs.SetActive(true);
            });
        }

        public override void EndWave(float duration)
        {
        }
    }
}
