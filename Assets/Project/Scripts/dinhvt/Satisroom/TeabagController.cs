using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class TeabagController : Item
    {
        [Space(20)]
        [SerializeField] LayerMask targetLayer;
        [SerializeField] Transform originRaycast;
        [Space(20)]
        [SerializeField] SpriteRenderer water;
        [SerializeField] Sprite tea;

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            _spriteRenderer.sortingOrder += 2;
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            _spriteRenderer.sortingOrder -= 2;

            RaycastHit2D hitInfo = Physics2D.Raycast(originRaycast.position, Vector2.zero, Mathf.Infinity, targetLayer);

            if (hitInfo && canComplete)
            {
                Transform hitTransform = hitInfo.transform;
                if (hitTransform != null)
                {
                    water.sprite = tea;
                    gameObject.SetActive(false);

                    isComplete = true;
                    CompleteMission();

                    return;
                }
            }

            transform.DOMove(_initialPosition, moveTime);
            transform.DOScale(_initialScale, scaleTime);
            transform.DORotate(_initialRotation, rotateTime);
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition + offset;
        }
    }
}
