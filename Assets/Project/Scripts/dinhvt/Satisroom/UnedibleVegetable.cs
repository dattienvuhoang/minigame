using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class UnedibleVegetable : ItemController
    {
        [Space(20)]
        [SerializeField] float deltaMove;
        [SerializeField] SpriteRenderer board;

        private float _deltaX;
        private float _deltaY;

        private void OnEnable()
        {
            _spriteRenderer.sortingOrder += ((ArrangeItemWave)wave).missionSpriteRends.Count;
            wave.AddMissionSpriteRend(_spriteRenderer);
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            transform.DOScale(_initialScale, scaleTime);

            if (!board.bounds.Contains(touchPosition))
            {   
                Vector3 boardPos = board.transform.position;
                _deltaX = touchPosition.x - boardPos.x;
                _deltaY = touchPosition.y - boardPos.y;

                if (Mathf.Abs(_deltaX) > Mathf.Abs(_deltaY))
                {
                    Move(_deltaX, true);
                }
                else
                {
                    Move(_deltaY, false);
                }
            }
            else
            {
                transform.DORotate(_initialRotation, rotateTime);
            }
        }

        public void Move(float delta, bool isMoveX)
        {
            isComplete = true;
            if (isMoveX)
            {
                transform.DOMoveX(Mathf.Sign(delta) * deltaMove, 0.5f).OnComplete(CompleteMission);
            }
            else
            {
                transform.DOMoveY(Mathf.Sign(delta) * deltaMove, 0.5f).OnComplete(CompleteMission);
            }
        }

        public override void CompleteMission()
        {
            base.CompleteMission();

            gameObject.SetActive(false);
        }
    }
}
