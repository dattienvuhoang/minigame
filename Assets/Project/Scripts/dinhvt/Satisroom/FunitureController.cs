using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class FunitureController : MonoBehaviour, DraggableObject, ISelectableObject
    {
        protected Vector3 _initialRotation;
        protected Vector3 _initialScale;
        protected Vector3 _initialPosition;
        protected SpriteRenderer _spriteRenderer;

        protected ShelfManager _shelfManager;

        [Header("ID Settings")]
        public int id;
        public int slotID;

        [Header("Select Settings")]
        [SerializeField] protected float time;
        [SerializeField] protected Vector3 selectRotation;
        [SerializeField] protected Vector3 selectScale = Vector3.one;

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
        public virtual void Select(Vector3 touchPosition)
        {
            transform.DORotate(selectRotation, time);
            transform.DOScale(selectScale, time);
        }

        public virtual void Deselect(Vector3 touchPosition)
        {
            
        }

        public virtual void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            transform.position = touchPosition + offset;
        }
    }
}
