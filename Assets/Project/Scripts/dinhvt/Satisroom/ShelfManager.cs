using UnityEngine;

namespace dinhvt
{
    public class ShelfManager : MonoBehaviour
    {
        protected WardrobeManager _wardrobeManager;

        public int id;
        protected int _funitureCount = 0;

        public void Init(WardrobeManager wardrobeManager)
        {
            _wardrobeManager = wardrobeManager;
        }

        public Transform GetSlot(int slotID = -1)
        {
            if (slotID < 0) return transform.GetChild(_funitureCount);

            return transform.GetChild(slotID);
        }

        public virtual void IncreaseCount()
        {
            _funitureCount++;
            CheckFull();
        }

        public virtual void DecreaseCount()
        {
            _funitureCount--;
        }

        public virtual void CheckFull()
        {
            if (IsFull())
            {
                _wardrobeManager.IncreaseCount();
            }
        }

        public virtual bool IsFull()
        {
            return _funitureCount == transform.childCount;
        }
    }
}
