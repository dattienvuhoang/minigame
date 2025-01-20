using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class EasyPlaceTool : ToolController
    {
        [Header("Easy Place Tool")]
        [SerializeField] SpriteRenderer slot;
        [SerializeField] protected float moveTargetTime;

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            DOTween.Kill(transform.name);
            _spriteRenderer.sortingOrder += 2;
            transform.DOScale(selectScale, scaleTime);
            transform.DORotate(selectRotation, scaleTime);
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);
                
            if (slot.bounds.Contains(touchPosition) && canComplete)
            {
                isComplete = true;

                _spriteRenderer.sortingOrder -= 2;
                transform.GetComponent<Collider2D>().enabled = false;
                slot.transform.GetComponent<Collider2D>().enabled = false;
                transform.DOScale(_initialScale, scaleTime);
                transform.DOMove(slot.transform.position, moveTargetTime).OnComplete(CheckComplete);
            }
            else
            {
                transform.DOScale(_initialScale, scaleTime);
                transform.DORotate(_initialRotation, scaleTime);
                transform.DOMove(_initialPosition, moveTime).SetId(transform.name).OnComplete(() =>
                {
                    _spriteRenderer.sortingOrder -= 2;
                });
            }
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition + offset;
        }
    }
}
