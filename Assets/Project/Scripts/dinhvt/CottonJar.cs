using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class CottonJar : DestructionTool
    {
        private SpriteRenderer _spriteRend;

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


        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);
            transform.DOScale(_initialScale, 0.3f).OnComplete(() =>
            {
                _spriteRend.sortingOrder -= 2;
            });
        }

        public override void ResetTransform(Vector3 touchPosition)
        {
            transform.DORotate(Vector3.zero, 0.3f);
            transform.DOLocalMove(onScreenPos, moveTime).SetId(transform.name).OnComplete(CheckComplete);
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
