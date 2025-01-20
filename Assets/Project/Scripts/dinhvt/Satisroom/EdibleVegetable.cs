using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class EdibleVegetable : ItemController
    {
        [Space(20)]
        [SerializeField] Sprite compactSprite;

        private void OnEnable()
        {
            _spriteRenderer.sortingOrder += ((ArrangeItemWave)wave).missionSpriteRends.Count;
            wave.AddMissionSpriteRend(_spriteRenderer);
        }

        public override void AssignToSlot(Slot slot, bool enableCollider = true)
        {
            _collider.enabled = enableCollider;
            if (!enableCollider) wave.RemoveMissionSpriteRend(_spriteRenderer);
            _slot = slot;
            SetSortingOrder(_slot.GetSortingOrder());
            transform.DOMove(_slot.GetPosition(), moveInSlotTime).OnComplete(() =>
            {
                _isInSlot = true;
                if (_slotManager.IsHaveSlot(idInSlot))
                {
                    _slotManager.SetIsFilledSlot(_slot, true);
                    if (compactSprite) _spriteRenderer.sprite = compactSprite;

                    isComplete = true;
                    CompleteMission();
                }
            });
        }
    }
}
