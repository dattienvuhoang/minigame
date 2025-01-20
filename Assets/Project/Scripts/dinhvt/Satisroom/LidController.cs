using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class LidController : EasyPlaceTool
    {
        [Header("Settings")]
        [SerializeField] Transform dumplings;
        [SerializeField] Vector3 dumplingsPos;


        private Collider2D _col;

        public override void Awake()
        {   
            base.Awake();
            _col = GetComponent<Collider2D>();
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            if (_useIndex == 1)
            {
                isComplete = true;

                dumplings.DOMove(dumplingsPos, moveTime);

                transform.DOScale(_initialScale, scaleTime);
                transform.DORotate(_initialRotation, scaleTime);
                transform.DOMove(_initialPosition, moveTime).OnComplete(() =>
                {
                    _spriteRenderer.sortingOrder += 2;
                    CheckComplete();
                });
            }
        }

        public override void Deselect(Vector3 touchPosition)
        {
            if (_useIndex == 1) return;

            base.Deselect(touchPosition);
        }

        public override void SetCanMissionComplete(bool isCan)
        {
            base.SetCanMissionComplete(isCan);

            _col.enabled = true;
        }
    }
}
