using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class ServingTray : Mission
    {
        [Header("Serving Tray")]
        [SerializeField] SpriteRenderer rotatingTray;
        [SerializeField] Transform ingredient;
        [SerializeField] int ingredientSortingOrder;
        [SerializeField] float moveTime;
        [SerializeField] int parentIndex;

        private Vector3 _initialPosition;
        private SpriteRenderer _spriteRend;
        private SpriteRenderer _ingredientSpriteRend;
        private Collider2D _collider2D;

        private void Awake()
        {
            _ingredientSpriteRend = ingredient.GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
            _spriteRend = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _initialPosition = transform.position;
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            DOTween.Kill(transform.name);

            _spriteRend.sortingOrder++;
            _ingredientSpriteRend.sortingOrder++;
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            if (rotatingTray.bounds.Contains(touchPosition) && canComplete)
            {
                CompleteIngredientMove(rotatingTray.transform);
            }
   

            ResetPosition();
        }

        private void ResetPosition()
        {
            transform.DOMove(_initialPosition, moveTime).SetId(transform.name).OnComplete(() =>
            {
                _spriteRend.sortingOrder--;
                if (!isComplete) _ingredientSpriteRend.sortingOrder--;
            });
        }

        private void CompleteIngredientMove(Transform hitTransform)
        {
            _collider2D.enabled = false;
            isComplete = true;
            _ingredientSpriteRend.sortingOrder = ingredientSortingOrder;
            ingredient.DOMove(hitTransform.position, moveTime).OnComplete(() =>
            {
                ingredient.SetParent(hitTransform.GetChild(parentIndex));
                CompleteMission();
            });
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition + offset;
        }
    }
}
