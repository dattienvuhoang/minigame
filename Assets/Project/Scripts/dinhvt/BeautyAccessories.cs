using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class BeautyAccessories : Mission, ISelectableObject
    {
        [SerializeField] float dragThreshold;
        [SerializeField] float deltaMove;

        private Vector3 _initialPos;
        private Vector3 _deselectPos;
        private float _deltaX;
        private float _deltaY;

        private void Start()
        {
            _initialPos = transform.position;
        }

        public override void Deselect(Vector3 touchPosition)
        {   
            _deselectPos = transform.position;
            _deltaX = _deselectPos.x - _initialPos.x;
            _deltaY = _deselectPos.y - _initialPos.y;

            if (Mathf.Abs(_deltaX) > Mathf.Abs(_deltaY))
            {
                Move(_deltaX, true);
            }
            else
            {
                Move(_deltaY, false);
            }
        }

        public override void Select(Vector3 touchPosition) { }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition + offset;   
        }

        public void Move(float delta, bool isMoveX)
        {   
            if (Mathf.Abs(delta) > dragThreshold)
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
            else
            {
                transform.DOMove(_initialPos, 0.3f);
            } 
        }
    }
}
