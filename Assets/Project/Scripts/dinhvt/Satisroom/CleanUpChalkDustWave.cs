using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class CleanUpChalkDustWave : Wave
    {
        [Header("End Wave")]
        [SerializeField] List<MovableObject> offScreenObjects = new List<MovableObject>();

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            foreach (var obj in offScreenObjects)
            {
                obj.transform.DOLocalMove(obj.targetPosition, endWaveTime).OnComplete(() =>
                {
                    obj.transform.gameObject.SetActive(false);
                });
            }
        }
    }
}
