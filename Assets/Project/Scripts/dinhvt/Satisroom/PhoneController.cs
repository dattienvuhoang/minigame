
using DG.Tweening;
using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class PhoneController : ItemController
    {
        public override void AssignToSlot(Slot slot, bool enableCollider = true)
        {
            _collider.enabled = enableCollider;
            _slot = slot;
            SetSortingOrder(_slot.GetSortingOrder());
            transform.DOMove(_slot.GetPosition(), moveInSlotTime).OnComplete(() =>
            {
                this.PostEvent(EventID.CameraFlash);
                //    missions[missions.Count - 1].gameObject.SetActive(false);
                _isInSlot = true;
                isComplete = true;
                CompleteMission();
            });
        }
    }
}
