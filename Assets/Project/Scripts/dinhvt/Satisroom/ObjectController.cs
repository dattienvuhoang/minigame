using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class ObjectController : SubTask
    {
        protected Vector3 _initialRotation;
        protected Vector3 _initialScale;
        protected Vector3 _initialPosition;
        protected SpriteRenderer _spriteRenderer;

        [Header("ID Settings")]
        public int id;
        public int slotID;

        [Header("Select Settings")]
        [SerializeField] protected Vector3 selectRotation;
        [SerializeField] protected Vector3 selectScale = Vector3.one;
        [SerializeField] protected float time;

        public virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public virtual void Start()
        {
            _initialRotation = transform.eulerAngles;
            _initialScale = transform.localScale;
            _initialPosition = transform.position;
        }
        public override void Select(Vector3 touchPosition)
        {   
            base.Select(touchPosition);

            transform.DORotate(selectRotation, time);
            transform.DOScale(selectScale, time);
        }

        public override void Deselect(Vector3 touchPosition)
        {

        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {   
            base.UpdatePosition(touchPosition, offset);
            transform.position = touchPosition + offset;
        }
    }
}
