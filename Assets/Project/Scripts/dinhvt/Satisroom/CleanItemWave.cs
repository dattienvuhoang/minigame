using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class CleanItemWave : Wave
    {
        [Header("Arrange Item Wave")]
        public List<SpriteRenderer> missionSpriteRends = new List<SpriteRenderer>();
        public int _maxSortingOrder;
        public int _startSortingOrder;

        public override void UpdateMissionSortingOrder(SpriteRenderer missionSR)
        {
            base.UpdateMissionSortingOrder(missionSR);

            if (missionSR.sortingOrder == _maxSortingOrder) return;
            int index = missionSpriteRends.IndexOf(missionSR);

            for (int i = missionSpriteRends.Count - 1; i > index; i--)
            {
                if (missionSpriteRends[i].sortingOrder > _startSortingOrder)
                    //missionSpriteRends[i].sortingOrder = (_startSortingOrder + i) - 1;
                    SetHierarchySortingOrder(missionSpriteRends[i], (_startSortingOrder + i) - 1);
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
