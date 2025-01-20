using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace dinhvt
{
    public class WashingTheDishWave : ArrangeItemWave
    {
        [Header("Start Wave")]
        [SerializeField] Transform dishRack;
        [SerializeField] Vector3 screenOnPosition;
        [SerializeField] float moveTime;

        [Header("End Wave")]
        [SerializeField] List<Collider2D> slotsCol;

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            dishRack.gameObject.SetActive(true);
            dishRack.DOMove(screenOnPosition, moveTime);

            foreach (Mission mission in missions)
            {
                mission.SetCanMissionComplete(true);
            }

            foreach (Collider2D col in slotsCol)
            {
                col.enabled = true;
            }
        }

        public override void InitMissionSpriteRend()
        {
            foreach (Mission mission in missions)
            {   
                if (mission is ItemController)
                {
                    missionSpriteRends.Add(mission.transform.GetComponent<SpriteRenderer>());
                }
            }

            if (missionSpriteRends.Count > 0)
            {
                _maxSortingOrder = missionSpriteRends[missionSpriteRends.Count - 1].sortingOrder;
            }
        }
    }
}
