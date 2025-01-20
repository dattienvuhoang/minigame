using SR4BlackDev;
using SR4BlackDev.UISystem;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class WardrobeManager : MonoBehaviour
    { 
        private int _shelfCount;

        [SerializeField] List<ShelfManager> shelfManagers = new List<ShelfManager>();

        private void Start()
        {
            InitShelf();
        }

        private void InitShelf()
        {
            foreach (ShelfManager shelfManager in shelfManagers)
            {
                shelfManager.Init(this);
            }
        }

        public void IncreaseCount()
        {
            _shelfCount++;
            CheckComplete();
            Debug.Log(_shelfCount);
        }

        public void DecreaseCount()
        {
            _shelfCount--;
        }

        public void CheckComplete()
        {
            if (_shelfCount == shelfManagers.Count)
            {
                Debug.Log("WIN");

                PopupManager.ShowToast("WIN");
                this.PostEvent(EventID.OnToggleDragAbility, false);
                //this.PostEvent(EventID.Log, "WIN");
            }
        }
    }
}
