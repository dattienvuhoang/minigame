using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class AttachTrayToTheBoxWave : ArrangeItemWave
    {
        [Header("Start Wave")]
        [SerializeField] List<MovableObject> movableObjs = new List<MovableObject>();

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            foreach (MovableObject obj in movableObjs)
            {
                obj.transform.gameObject.SetActive(true);
                obj.transform.DOMove(obj.targetPosition, duration);
            }
        }

        public override void EndWave(float duration)
        {
        }

        public override void NextMission()
        {
            if (_missionIndex == 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    missions[_missionIndex + i].SetCanMissionComplete(true);
                    ToggleCollider(missions[_missionIndex + i], true);
                }
            }
            else
            {
                missions[_missionIndex].SetCanMissionComplete(true);
                ToggleCollider(missions[_missionIndex], true);
            }
        }
    }
}
