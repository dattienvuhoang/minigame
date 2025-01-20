using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class DirtyObject : AnimalController
    {
        [Header("Dirty Object Settings")]
        [SerializeField] SpriteRenderer dirtySprite;

        public override void UpdateSortingOrder(int sortingOrder)
        {   
            base.UpdateSortingOrder(sortingOrder);
            dirtySprite.sortingOrder = sortingOrder;
        }
    }
}
