using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class OrganizingKitchenUtensilsWave : ArrangeItemWave
    {
        [Header("Start Wave")]
        [SerializeField] Transform box;
        [SerializeField] Vector3 screenOnPosition;
        [SerializeField] float moveTime;

        [Header("End Wave")]
        [SerializeField] List<Collider2D> slotsCol;

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            box.gameObject.SetActive(true);
            box.DOMove(screenOnPosition, moveTime);
            foreach (Mission mission in missions)
            {
                mission.transform.DOMove(screenOnPosition, moveTime);
                mission.SetCanMissionComplete(true);
            }

            foreach (Collider2D col in slotsCol)
            {
                col.enabled = true;
            }
        }

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            foreach (Collider2D col in  slotsCol)
            {
                col.enabled = false;
            }
        }

        public override void InitMissionSpriteRend() { }
    }
}
