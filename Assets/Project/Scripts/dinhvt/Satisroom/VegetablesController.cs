using Destructible2D;
using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class VegetablesController : ItemController
    {
        [Space(20)]
        public D2dDestructibleSprite peel;
        [SerializeField] PeelerController peeler;
        [Space(20)]
        [SerializeField] GameObject halves;


        public override void AssignToSlot(Slot slot, bool enableCollider = true)
        {
            _collider.enabled = enableCollider;
            _slot = slot;
            transform.DOMove(_slot.GetPosition(), moveInSlotTime).SetId(transform.GetInstanceID() + "MoveInSlot").OnComplete(() =>
            {
                _isInSlot = true;
                if (_slotManager.IsHaveSlot(idInSlot))
                {
                    _slotManager.SetIsFilledSlot(_slot, true);

                    if (peel != null) peel.enabled = true;
                    peeler.UpdateVegetablePeel(this, peel);
                }   
            });
        }

        public override void ClearSlotAssignment()
        {
            base.ClearSlotAssignment();

            if (peel != null) peel.enabled = false;
            peeler.UpdateVegetablePeel(null, null);
        }

        public void OnCut()
        {
            CompleteMission();

            halves.SetActive(true);
            _slotManager.SetIsFilledSlot(_slot, false);
            gameObject.SetActive(false);
        }

        public void DisableCollider()
        {
            _collider.enabled = false;
        }
    }
}
