using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace dinhvt
{
    public class ItemController : Mission
    {   
        protected Vector3 _initialPosition, _initialRotation, _initialScale;
        protected SpriteRenderer _spriteRenderer;
        protected Collider2D _collider;
        protected SlotManager _slotManager;

        public bool _isInSlot;
        protected Slot _slot;

        [Header("Item Controller")]
        [SerializeField] protected int id;
        [SerializeField] protected int idInSlot;
        [Space(20)]
        [SerializeField] Sprite selectSprite;
        [SerializeField] Sprite deselectSprite;
        [Space(20)]
        [SerializeField] protected Vector3 selectRotation;
        [SerializeField] protected Vector3 selectScale = Vector3.one;
        [SerializeField] protected float moveTime, scaleTime, rotateTime;
        [Space(20)]
        [SerializeField] LayerMask targetLayer;
        [SerializeField] protected float moveInSlotTime;
        [SerializeField] bool disableCollider;
        [SerializeField] bool moveInIncorrectSlot;
        [SerializeField] bool moveToOrderedPosition;
        [SerializeField] bool resetPositionOnDeselect;

        public virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();

            _initialPosition = transform.localPosition;
            _initialRotation = transform.localEulerAngles;
            _initialScale = transform.localScale;
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            DOTween.Kill(transform.GetInstanceID() + "AssignToSlot");

            if (selectSprite) _spriteRenderer.sprite = selectSprite;
            transform.DOScale(selectScale, scaleTime);
            transform.DORotate(selectRotation, rotateTime);

            wave?.UpdateMissionSortingOrder(_spriteRenderer);

            if (_isInSlot) ClearSlotAssignment();
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            transform.DOScale(_initialScale, scaleTime);

            if (RaycastAndHandleSlot()) return;

            if (deselectSprite) _spriteRenderer.sprite = deselectSprite;

            if (resetPositionOnDeselect)
                transform.DOMove(_initialPosition, moveTime);

            transform.DORotate(_initialRotation, rotateTime);
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);
            transform.position = touchPosition + offset;
        }

        public virtual void SetSortingOrder(int targetSortingOrder)
        {
            _spriteRenderer.sortingOrder = targetSortingOrder;
            SpriteRenderer spriteRenderer;
            foreach (Transform child in transform)
            {
                spriteRenderer = child.GetComponent<SpriteRenderer>();
                if (spriteRenderer) spriteRenderer.sortingOrder = targetSortingOrder;
            }
        }

        public virtual bool RaycastAndHandleSlot()
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, targetLayer);

            if (!hitInfo || !canComplete) return false;

            Transform hitTransform = hitInfo.transform;

            if (!hitTransform.TryGetComponent(out _slotManager)) return false;

            if (id == _slotManager.GetID() && !_slotManager.IsFull())
            {
                HandleSlotAssignment();
                return true;
            }
            return false;
        }

        public virtual void HandleSlotAssignment()
        {
            if (moveToOrderedPosition)
                AssignToSlot(_slotManager.GetSlot(), !disableCollider);
            else if (_slotManager.IsHaveSlot(idInSlot))
                AssignToSlot(_slotManager.GetSlot(idInSlot), !disableCollider);
            else if (moveInIncorrectSlot)
                AssignToSlot(_slotManager.GetSlot(idInSlot), _slotManager.IsHaveSlot(idInSlot) ? !disableCollider : true);
        }

        public virtual void AssignToSlot(Slot slot, bool enableCollider = true)
        {
            _collider.enabled = enableCollider;
            if (!enableCollider) wave.RemoveMissionSpriteRend(_spriteRenderer);
            _slot = slot;
            SetSortingOrder(_slot.GetSortingOrder());

            string tweenID = transform.GetInstanceID() + "MoveInSlot";
            transform.DOScale(_slot.GetLocalScale(), moveInSlotTime).SetId(tweenID);
            transform.DORotate(_slot.GetRotation(), moveInSlotTime).SetId(tweenID);
            transform.DOMove(_slot.GetPosition(), moveInSlotTime).SetId(tweenID).OnComplete(() =>
            {
                _isInSlot = true;

                if (moveInIncorrectSlot)
                {
                    _slotManager.SetIsFilledSlot(_slot, true);
                }

                if (_slotManager.IsHaveSlot(idInSlot))
                {
                    _slotManager.SetIsFilledSlot(_slot, true);
                    
                    isComplete = true;
                    CompleteMission();
                }
            });
        }

        public virtual void ClearSlotAssignment()
        {
            _isInSlot = false;
            _slotManager.SetIsFilledSlot(_slot, false);
            _slot = null;
            if (moveToOrderedPosition) ResetMission();
        }

        public override SpriteRenderer GetSpriteRenderer() { return _spriteRenderer; }
    }
}
