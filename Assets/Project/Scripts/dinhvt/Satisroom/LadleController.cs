using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class LadleController : ItemController
    {

        public override void AssignToSlot(Slot slot, bool enableCollider = true)
        {
            _collider.enabled = enableCollider;
            _slot = slot;

            Vector3 targetPosition = _slot.GetPosition();
            targetPosition.y -= _spriteRenderer.size.y / 2;

            SetSortingOrder(_slot.GetSortingOrder());
            transform.DOMove(targetPosition, moveInSlotTime).OnComplete(() =>
            {
                _isInSlot = true;
                if (_slotManager.IsHaveSlot(idInSlot))
                {
                    _slotManager.SetIsFilledSlot(_slot, true);
                    isComplete = true;
                    CompleteMission();
                }
            });
        }
    }
}
