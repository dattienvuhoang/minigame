using SR4BlackDev;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class SlotManager : MonoBehaviour
    {
        [SerializeField] int id;
        [SerializeField] List<Slot> slots = new List<Slot>();
        [SerializeField] bool disableColliderIfFull;

        public int _slotCount;
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {   
            foreach (Transform slot in transform)
            {
                slots.Add(slot.GetComponent<Slot>());
            }
        }

        public Slot GetSlot(int idInSlot)
        {          
            foreach (Slot slot in slots)
            {
                if (slot.id == idInSlot) return slot;
            }
            
            return slots[0];
        }

        public Slot GetSlot()
        {
            foreach (Slot slot in slots)
            {
                if (!slot.isFilled) return slot;
            }

            return null;
        }

        public int GetID() { return id; }

        public bool IsFull() { return _slotCount == slots.Count; }

        public bool IsHaveSlot(int idInSlot)
        {
            foreach (Slot slot in slots)
            {
                if (slot.id == idInSlot) return true;
            }

            return false;
        }

        public virtual void SetIsFilledSlot(Slot slot, bool isFilled)
        {
            slot.isFilled = isFilled;
            _slotCount = _slotCount + (isFilled ? 1 : -1);

            _collider.enabled = (disableColliderIfFull && _slotCount == slots.Count) ? false : true;

            if (IsFull()) this.PostEvent(EventID.OnSlotManagerFull);
        }
    }
}
