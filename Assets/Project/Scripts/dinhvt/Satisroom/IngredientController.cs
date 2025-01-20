using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class IngredientController : ToolController
    {
        [Header("Ingredient Settings")]
        [SerializeField] LayerMask targetLayer;

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            _spriteRenderer.sortingOrder += 2;
            transform.DOScale(selectScale, scaleTime);
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            RaycastHit2D hitInfo = Physics2D.Raycast(touchPosition, Vector2.zero, Mathf.Infinity, targetLayer);

            if (hitInfo && canComplete)
            {
                Transform hitTransform = hitInfo.transform;

                if (hitTransform != null && CompareTagValue(hitTransform.GetComponent<Collider2D>(), "Dumpling Wrapper")
                        && hitTransform.childCount == 0)
                {
                    isComplete = true;

                    transform.GetComponent<Collider2D>().enabled = false;   
                    transform.SetParent(hitTransform);
                    transform.DOScale(Vector3.one, scaleTime);
                    transform.DOMove(hitTransform.position, moveTime).OnComplete(CheckComplete);

                    return;
                }
            }

            _spriteRenderer.sortingOrder -= 2;
            transform.position = _initialPosition;
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition + offset;
        }
    }
}
