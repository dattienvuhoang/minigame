using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class Funiture : FunitureController
    {
        protected static int _maxOrderSorting;
        protected static int _minOrderSorting;
        protected bool _isComplete;

        [SerializeField] protected LayerMask targetLayer;
        [SerializeField] protected float moveTime;

        public override void Start()
        {   
            base.Start();
            _maxOrderSorting = _spriteRenderer.sortingOrder;
            _minOrderSorting = 1;
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            _spriteRenderer.sortingOrder = ++_maxOrderSorting;

            if (_isComplete)
            {
                _shelfManager.DecreaseCount();
            }
            _isComplete = false;
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, targetLayer);

            if (hitInfo && hitInfo.transform != null)
            {
                if (hitInfo.transform.TryGetComponent<ShelfManager>(out _shelfManager))
                {
                    if (_shelfManager.id == id && !_shelfManager.IsFull())
                    {
                        _isComplete = true;
                        MoveToSlot();

                        return;
                    }
                }
            }

            transform.DORotate(_initialRotation, time);
        }

        public virtual void MoveToSlot()
        {
            _spriteRenderer.sortingOrder = _minOrderSorting;
            transform.DOMove(_shelfManager.GetSlot(slotID).position, moveTime).OnComplete(() =>
            {
                _shelfManager.IncreaseCount();
            });     
        }
    }
}
