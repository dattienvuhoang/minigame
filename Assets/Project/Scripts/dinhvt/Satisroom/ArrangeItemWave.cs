using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class ArrangeItemWave : Wave
    {
        [Header("Arrange Item Wave")]
        public List<SpriteRenderer> missionSpriteRends = new List<SpriteRenderer>();
        protected int _maxSortingOrder;

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            InitMissionSpriteRend();
        }

        public virtual void InitMissionSpriteRend()
        {   
            foreach (Mission mission in missions)
            {
                if (mission is ItemController itemController) 
                    if (itemController.GetSpriteRenderer() != null)
                        missionSpriteRends.Add(itemController.GetSpriteRenderer());
            }

            if (missionSpriteRends.Count > 0)
            {
                _maxSortingOrder = missionSpriteRends[missionSpriteRends.Count - 1].sortingOrder;
            }
        }

        public override void AddMissionSpriteRend(SpriteRenderer missionSP)
        {
            missionSpriteRends.Add(missionSP);
            _maxSortingOrder = missionSP.sortingOrder;
        }

        public override void RemoveMissionSpriteRend(SpriteRenderer missionSP)
        {
            missionSpriteRends.Remove(missionSP);
        }

        public override void UpdateMissionSortingOrder(SpriteRenderer missionSR)
        {   
            base.UpdateMissionSortingOrder(missionSR);

            if (missionSR.sortingOrder == _maxSortingOrder) return;
            int index = missionSpriteRends.IndexOf(missionSR);

            for (int i = missionSpriteRends.Count - 1; i > index; i--)
            {   
                //missionSpriteRends[i].sortingOrder = (_startSortingOrder + i) - 1;
                SetHierarchySortingOrder(missionSpriteRends[i], _maxSortingOrder - (missionSpriteRends.Count - i));
            }

            //missionSR.sortingOrder = _maxSortingOrder;
            //foreach (Transform child in missionSR.transform)
            //{
            //    child.GetComponent<SpriteRenderer>().sortingOrder = _maxSortingOrder;
            //}
            SetHierarchySortingOrder(missionSR, _maxSortingOrder);

            MoveMissionSpriteRendToEnd(missionSR);
        }

        public virtual void MoveMissionSpriteRendToEnd(SpriteRenderer missionSR)
        {
            missionSpriteRends.Remove(missionSR);
            missionSpriteRends.Add(missionSR);
        }

        public virtual void SetHierarchySortingOrder(SpriteRenderer parent, int sortingOrder)
        {
            parent.sortingOrder = sortingOrder;
            SpriteRenderer spriteRenderer;
            foreach (Transform child in parent.transform)
            {   
                if (child.TryGetComponent<SpriteRenderer>(out spriteRenderer))
                {
                    spriteRenderer.sortingOrder = sortingOrder;
                }
            }
        }
    }
}
