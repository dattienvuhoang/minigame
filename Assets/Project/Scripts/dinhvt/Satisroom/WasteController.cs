using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class WasteController : Mission
    {
        [SerializeField] Vector3 selectScale;
        [SerializeField] float scaleTime;
        [SerializeField] float moveOutScreenTime;
        [Space(20)]
        [SerializeField] float movementMagnitude;
        [SerializeField] Transform shelf;

        private Vector3 _deselectPos;
        private Vector3 _initialScale;
        private float _deltaX;
        private float _deltaY;
        private SpriteRenderer _shelfSR;
        private SpriteRenderer _spriteRenderer;

        public void Awake()
        {   
            _shelfSR = shelf.GetComponent<SpriteRenderer>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _initialScale = transform.localScale;
        }

        public override void Deselect(Vector3 touchPosition)
        {
            transform.DOScale(_initialScale, scaleTime);
            if (_shelfSR.bounds.Contains(transform.position)) return;

            _deselectPos = transform.position;
            _deltaX = _deselectPos.x - shelf.position.x;
            _deltaY = _deselectPos.y - shelf.position.y;

            if (Mathf.Abs(_deltaX) > Mathf.Abs(_deltaY))
            {
                Move(_deltaX, true);
            }
            else
            {
                Move(_deltaY, false);
            }
        }

        public override void Select(Vector3 touchPosition) 
        {
            transform.DOScale(selectScale, scaleTime);

            wave.UpdateMissionSortingOrder(_spriteRenderer);
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition + offset;
        }

        public void Move(float delta, bool isMoveX)
        {
            isComplete = true;
            if (isMoveX)
            {
                transform.DOMoveX(Mathf.Sign(delta) * movementMagnitude, moveOutScreenTime).OnComplete(() =>
                {
                    CompleteMission();
                    gameObject.SetActive(false);
                });
            }
            else
            {
                transform.DOMoveY(Mathf.Sign(delta) * movementMagnitude, moveOutScreenTime).OnComplete(() =>
                {
                    CompleteMission();
                    gameObject.SetActive(false);
                });
            }
        }
    }
}
