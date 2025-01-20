
using UnityEngine;

namespace dinhvt
{
    public interface IArrangableItem
    {
        public void SetSortingOrder(int targetSortingOrder);
        public SpriteRenderer GetSpriteRenderer();
    }
}
