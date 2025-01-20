using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class TakeOffHairClipsWave : ArrangeItemWave
    {
        [Header("End Wave")]
        [SerializeField] SpriteRenderer box;

        [Header("Unlock Mission")]
        [SerializeField] List<HairAccessories> prerequisiteMissions = new List<HairAccessories>();
        [SerializeField] Collider2D unlockedMissionCol;

        private bool _prerequisiteMissionChangedPosition;

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            box.DOFade(0f, duration).OnComplete(() =>
            {
                box.gameObject.SetActive(false);
            });
        }

        public void UnlockedMission()
        {
            _prerequisiteMissionChangedPosition = true;
            foreach (HairAccessories misssion in prerequisiteMissions)
            {
                if (!misssion.HasPositionChanged())
                {
                    _prerequisiteMissionChangedPosition = false;
                    break;
                }
            }

            if (_prerequisiteMissionChangedPosition) unlockedMissionCol.enabled = true;
        }
    }
}
