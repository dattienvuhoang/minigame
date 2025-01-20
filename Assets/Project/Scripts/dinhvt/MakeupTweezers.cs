using DG.Tweening;
using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class MakeupTweezers : Tweezers
    {

        private SlotManager _slotManager;
        private Clump clump;
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (CompareTagValue(collision, "Slot") && targetList.Count > 0)
            {
                _timer += Time.deltaTime;

                if (_timer > timeThreshold)
                {
                    foreach (IDropTarget target in targetList)
                    {
                        _slotManager = collision.GetComponent<SlotManager>();
                        clump = target as Clump;
                        if (clump.id == _slotManager.GetID() && _slotManager.IsHaveSlot(clump.inSlotId))
                        {
                            Drop(target, _slotManager.GetSlot(clump.inSlotId).transform);
                            targetList.Remove(target);
                            break;
                        }
                    }
                }
            }
        }

        protected virtual void Drop(IDropTarget target, Transform slot)
        {
            target.Dropped(slot);
            targetPerClamp--;
            if (_targetCount == numOfTarget) isComplete = true;

            frontTool.DOLocalRotate(_initialFrontRotation, 0.2f);
        }
    }
}
