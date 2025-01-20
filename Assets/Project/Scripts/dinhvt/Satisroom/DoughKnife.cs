using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class DoughKnife : ToolController
    {
        [Header("Dough Knife")]
        [SerializeField] float cooldownTime;

        private bool _isInTargetCollider;
        private LargePieceOfFlour _largePieceOfFlour;
        private float _timer;

        public override void Start()
        {
            base.Start();
            _timer = cooldownTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (CompareTagValue(collision, "Large Flour") && canComplete)
            {   
                _isInTargetCollider = true;
                _largePieceOfFlour = collision.transform.GetComponent<LargePieceOfFlour>();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (CompareTagValue(collision, "Large Flour") && canComplete)
            {
                _timer += Time.deltaTime;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (CompareTagValue(collision, "Large Flour") && canComplete)
            {
                _isInTargetCollider = false;
                _largePieceOfFlour = null;
            }
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            _spriteRenderer.sortingOrder += 2;
            transform.DORotate(selectRotation, 0.3f);
            DOTween.Kill(transform.name);
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            _spriteRenderer.sortingOrder -= 2;
            transform.DORotate(_initialRotation, 0.3f);
            transform.DOMove(_initialPosition, moveTime).SetId(transform.name).OnComplete(CheckComplete);
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition + offset;

            if (canComplete) CutFlour();
        }

        private void CutFlour()
        {
            if (_isInTargetCollider && _timer >= cooldownTime)
            {
                _timer = 0f;
                _largePieceOfFlour.DivideIntoPieces();
                if (_largePieceOfFlour.IsDivideComplete())
                {
                    _largePieceOfFlour.gameObject.SetActive(false);
                    isComplete = true;
                }

                CheckComplete();
            }
        }
    }
}
