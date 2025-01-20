using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class Dumpling : ToolController
    {
        [Header("Dumpling")]
        [SerializeField] SpriteRenderer mold;
        [SerializeField] float moveTargetTime;
        [SerializeField] Sprite targetSprite;
        [SerializeField] Transform parentWave;

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);
            if (canComplete)
            {
                DOTween.Kill(transform.name);
                _spriteRenderer.sortingOrder += 2;
                transform.DOScale(selectScale, scaleTime);
            }
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            if (canComplete && mold.bounds.Contains(touchPosition) && mold.transform.childCount == 0)
            {
                isComplete = true;

                transform.GetComponent<Collider2D>().enabled = false;
                transform.SetParent(mold.transform);
                transform.DOScale(_initialScale, scaleTime);
                transform.DOMove(mold.transform.position, moveTargetTime).OnComplete(CheckComplete);
            }
            else
            {
                transform.DOScale(_initialScale, scaleTime);
                transform.DOMove(_initialPosition, moveTime).SetId(transform.name).OnComplete(() =>
                {
                    _spriteRenderer.sortingOrder -= 2;
                });
            }
        }


        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            if (canComplete) transform.position = touchPosition + offset;
        }

        public void Shaped()
        {
            _spriteRenderer.sprite = targetSprite;
            _spriteRenderer.sortingOrder -= 2;
            transform.DOMove(_initialPosition, moveTime);
            transform.SetParent(parentWave);
        }
    }
}
