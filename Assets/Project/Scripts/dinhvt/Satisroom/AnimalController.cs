using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class AnimalController : ObjectController
    {
        protected int _selectSortingOrder = 7;
        protected int _slotSortingOrder = 3;
        protected int _initialSortingOrder;
        protected ShelfManager _shelfManager;

        [SerializeField] LayerMask targetLayer;

        public override void Start()
        {
            base.Start();
            _initialSortingOrder = _spriteRenderer.sortingOrder;
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            transform.DOScale(_initialScale, 0.5f);

            RaycastHit2D hitInfo = Physics2D.Raycast(touchPosition, Vector3.zero, Mathf.Infinity, targetLayer);
            if (hitInfo && hitInfo.transform != null)
            {
                if (hitInfo.transform.TryGetComponent<ShelfManager>(out _shelfManager))
                {
                    if (_shelfManager.id == id)
                    {                         
                        MoveToSlot();

                        return;
                    }
                }
            }

            UpdateSortingOrder(_initialSortingOrder);
            transform.DOMove(_initialPosition, time).SetId(transform.name);
        }

        public override void Select(Vector3 touchPosition)
        {   
            base.Select(touchPosition);

            DOTween.Kill(transform.name);
            UpdateSortingOrder(_selectSortingOrder);
            if (_isComplete)
            {
                _isComplete = false;
                _taskManager.UpdateTaskCount(_isComplete);
            }
        }

        public virtual void MoveToSlot()
        {
            UpdateSortingOrder(_slotSortingOrder);
            transform.DOMove(_shelfManager.GetSlot(slotID).position, time).SetId(transform.name).OnComplete(() =>
            {
                _isComplete = true;
                _taskManager.UpdateTaskCount(_isComplete);
            });
        }

        public virtual void UpdateSortingOrder(int sortingOrder)
        {
            _spriteRenderer.sortingOrder = sortingOrder;
        }
    }
}
