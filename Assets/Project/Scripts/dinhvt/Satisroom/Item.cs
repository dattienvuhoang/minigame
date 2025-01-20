using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class Item : Mission
    {
        protected Vector3 _initialPosition, _initialRotation, _initialScale;
        protected SpriteRenderer _spriteRenderer;
        protected Collider2D _collider;

        [SerializeField] protected Vector3 selectRotation;
        [SerializeField] protected Vector3 selectScale = Vector3.one;
        [SerializeField] protected float moveTime, scaleTime, rotateTime;

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

            transform.DOScale(selectScale, scaleTime);
            transform.DORotate(selectRotation, rotateTime);
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            //transform.DOScale(_initialScale, scaleTime);
            //transform.DORotate(_initialRotation, rotateTime);
        }
    }
}
