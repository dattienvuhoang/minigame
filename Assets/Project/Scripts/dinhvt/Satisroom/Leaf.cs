using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class Leaf : Trash
    {
        public override void Dragged(Transform dragger)
        {
            transform.SetParent(dragger);
        }

        public override void Dropped(Transform slot)
        {
            col.enabled = false;
            transform.DOMove(slot.position, moveTime).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
            sp.sortingOrder = inSlotSortingOrder;
        }
    }
}
