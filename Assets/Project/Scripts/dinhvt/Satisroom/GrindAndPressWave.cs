using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class GrindAndPressWave : Wave
    {
        [Header("Start Wave")]
        [SerializeField] List<MovableObject> movableObjects = new List<MovableObject>();
        [SerializeField] GameObject missionChecker;

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            StartCoroutine(InitMission(duration));
        }

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            missionChecker.SetActive(false);
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

        IEnumerator InitMission(float duration)
        {
            foreach (MovableObject obj in movableObjects)
            {
                obj.transform.DOMove(obj.targetPosition, duration);
            }

            yield return new WaitForSeconds(duration);
            missionChecker.SetActive(true);
        }
    }
}
