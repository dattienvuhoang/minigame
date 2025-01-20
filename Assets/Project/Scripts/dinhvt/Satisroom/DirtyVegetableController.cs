using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class DirtyVegetableController : ItemController
    {
        [Space(20)]
        [SerializeField] SpriteRenderer waterSR;
        [SerializeField] Transform cleanVegetable;
        [SerializeField] SpriteRenderer dirtyInSink;
        [SerializeField] float fadeTime;

      

        public override void AssignToSlot(Slot slot, bool enableCollider = true)
        {
            _collider.enabled = enableCollider;
            _slot = slot;
            SetSortingOrder(_slot.GetSortingOrder());
            transform.DOMove(_slot.GetPosition(), moveInSlotTime).OnComplete(() =>
            {
                _isInSlot = true;
                if (_slotManager.IsHaveSlot(idInSlot))
                {
                    _slotManager.SetIsFilledSlot(_slot, true);
                    Clean();
                }
            });
        }

        public void Clean()
        {
            cleanVegetable.gameObject.SetActive(true);
            cleanVegetable.position = transform.position;
            cleanVegetable.GetComponent<SpriteRenderer>().sortingOrder = waterSR.sortingOrder - 1;  
            dirtyInSink.DOFade(1f, fadeTime);
            _spriteRenderer.DOFade(0f, fadeTime).OnComplete(() =>
            {   
                isComplete = true;
                CompleteMission();
            });
        }

        public override void CompleteMission()
        {
            base.CompleteMission();

            gameObject.SetActive(false);
        }
    }
}
