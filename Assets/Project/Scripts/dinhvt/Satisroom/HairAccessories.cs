using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class HairAccessories : ItemController
    {
        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            ((TakeOffHairClipsWave) wave).UnlockedMission();
        }

        public override void EndMission(float duration)
        {
            base.EndMission(duration);

            _spriteRenderer.DOFade(0f, duration).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }

        public bool HasPositionChanged()
        {
            return transform.position != _initialPosition;
        }
    }
}
