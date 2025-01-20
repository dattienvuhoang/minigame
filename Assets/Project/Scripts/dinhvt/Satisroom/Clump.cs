using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class Clump : Trash
    {
        public int id;
        public int inSlotId;

        public override void Dragged(Transform dragger)
        {
            transform.SetParent(dragger);
        }

        public override void Dropped(Transform slot)
        {
            col.enabled = false;
            transform.DOMove(slot.position, moveTime).OnComplete(() =>
            {
                transform.SetParent(slot);
            });
            sp.sortingOrder = inSlotSortingOrder;
        }
    }
}
