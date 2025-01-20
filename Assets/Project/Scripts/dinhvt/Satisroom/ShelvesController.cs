using SR4BlackDev;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class ShelvesController : Mission
    {
        [Header("Shelves")]
        [SerializeField] List<SlotManager> slotManagers = new List<SlotManager>();
        [SerializeField] List<GameObject> itemsInShelf = new List<GameObject>();
        [SerializeField] GameObject door;

        private bool canOpen;
        private bool canClose;
        private Collider2D _collider;
        private SpriteRenderer _doorSprite;
        private List<Collider2D> slotManagerCols = new List<Collider2D>();


        private void Awake()
        {
            this.RegisterListener(EventID.OnSlotManagerFull, SetCanClose);
            this.RegisterListener(EventID.OnStoveTurnedOff, SetCanOpen);

            _collider = GetComponent<Collider2D>();
            _doorSprite = door.GetComponent<SpriteRenderer>();

            foreach (SlotManager slotManager in slotManagers)
            {
                slotManagerCols.Add(slotManager.GetComponent<Collider2D>());    
            }
        }
        private void OnDestroy()
        {
            this.RemoveListener(EventID.OnStoveTurnedOff, SetCanOpen);
            this.RemoveListener(EventID.OnSlotManagerFull, SetCanClose);
        }

        private void Update()
        {
            if (canClose)
            {
                foreach (GameObject item in itemsInShelf)
                {   
                    if (_doorSprite.bounds.Contains(item.transform.position) || !item.activeInHierarchy)
                    {
                        return;
                    }
                }

                Close();
            }
        }


        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            if (canOpen)
            {
                canOpen = false;
                Open();
            }
        }

        private void Open()
        {
            canOpen = false;
            door.SetActive(false);

            _collider.enabled = false;
            foreach (Collider2D col in slotManagerCols)
            {
                col.enabled = true;
            }

            foreach(GameObject item in itemsInShelf)
            {
                item.SetActive(true);
            }
        }

        private void Close()
        {
            door.SetActive(true);
            canClose = false;
        }

        private void SetCanOpen(object sender, object result)
        {
            canOpen = true;
        }

        private void SetCanClose(object sender, object result)
        {
            SlotManager slot = (SlotManager)sender;
            if (slotManagers.Contains(slot))
            {
                foreach (SlotManager slotManager in slotManagers)
                {
                    if (!slotManager.IsFull()) return;
                }

                canClose = true;
            }
        }
    }
}
