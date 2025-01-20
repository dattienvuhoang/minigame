
using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class MixDoughWave : Wave
    {
        [Header("End Wave")]
        [SerializeField] Transform missionsOfWave;
        [SerializeField] Vector3 offScreenPos;
        [SerializeField] float time;
        [Space(20)]
        [SerializeField] Transform flour;
        [SerializeField] Vector3 scaleVector;
 
        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            if (missions.Count > 2)
            {
                missions[0].SetCanMissionComplete(true);
                missions[1].SetCanMissionComplete(true);
            }
        }

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            missionsOfWave.DOMove(offScreenPos, time).OnComplete(() =>
            {
                missionsOfWave.gameObject.SetActive(false);
            });

            flour.SetParent(null);
            flour.GetComponent<SpriteRenderer>().DOFade(0.3f, time / 2);
            flour.DOScale(scaleVector, time / 2).OnComplete(() =>
            {
                flour.gameObject.SetActive(false);
            });
        }
    }
}
