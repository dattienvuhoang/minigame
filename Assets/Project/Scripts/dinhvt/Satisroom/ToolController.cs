using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class ToolController : Mission
    {
        protected Vector3 _initialPosition;
        protected Vector3 _initialRotation;
        protected Vector3 _initialScale;
        protected SpriteRenderer _spriteRenderer;
        protected int _initialSortingOrder;
        protected Collider2D _collider;
        protected int _useIndex;

        [Header("Tool Controller")]
        public float moveTime;
        public float rotateTime;
        public float scaleTime;
        [SerializeField] protected Vector3 selectRotation;
        //[SerializeField] protected Vector3 dragRotation;
        [SerializeField] protected Vector3 selectScale = Vector3.one;

        [SerializeField] protected int numOfUse;


        public virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>(); 
        }

        public virtual void Start()
        {
            _initialPosition = transform.localPosition;
            _initialRotation = transform.localEulerAngles;
            _initialScale = transform.localScale;
            _initialSortingOrder = _spriteRenderer.sortingOrder;
        }

        public virtual void CheckComplete()
        {
            if (canComplete && isComplete)
            {
                CompleteMission();

                if (_useIndex < numOfUse - 1)
                {
                    ResetMission();
                }
            }
        }

        public override void ResetMission()
        {
            isComplete = false;
            _useIndex++;
        }
    }
}
