using DG.Tweening;
using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class Mask : CleaningTool
    {
        private SpriteRenderer _spriteRend;

        [Header("Mask")]
        [SerializeField] SpriteRenderer face;
         
        public override void Start()
        {
            base.Start();

            _initialScale = transform.localScale;
            _spriteRend = GetComponent<SpriteRenderer>();
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            _spriteRend.sortingOrder += 2;
            transform.DOScale(Vector2.one, 0.3f);
            transform.position = touchPosition;
        }

        public override void ResetTransform(Vector3 touchPosition)
        {           
            if (face.bounds.Contains(touchPosition) && canComplete)
            {
                isComplete = true;
                transform.DOMove(face.transform.position, 0.5f).OnComplete(CheckComplete);
            }
            else
            {
                transform.DOMove(onScreenPos, moveTime).SetId(transform.name);
                transform.DOScale(_initialScale, 0.3f).OnComplete(() =>
                {
                    _spriteRend.sortingOrder -= 2;
                });
            }
        }

        public override void StartMission(float duration) { }
        public override void EndMission(float duration) { }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition;
        }
    }
}

