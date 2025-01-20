using UnityEngine;

namespace dinhvt
{
    public class FruitController : ItemController
    {
        [Header("Fruit Settings")]
        [SerializeField] Sprite slicedFruitSprite;
        

        public override void HandleSlotAssignment()
        {
            base.HandleSlotAssignment();

            _spriteRenderer.sprite = slicedFruitSprite;
        }
    }
}
