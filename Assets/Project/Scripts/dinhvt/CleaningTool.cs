using DG.Tweening;
using UnityEngine;
using SR4BlackDev;

namespace dinhvt
{
    public class CleaningTool : Mission, ISelectableObject
    {
        protected Vector2 _initialPosition;
        protected Vector3 _initialRotation;
        protected Vector3 _initialScale;
        protected SpriteRenderer _spriteRenderer;
        protected Collider2D _collider;

        [Header("Cleaning Tool")]
        public Vector2 onScreenPos;
        public Vector3 offScreenPos;
        [Space(20)]
        public float moveTime;
        public float rotateTime = 0.2f;
        public float scaleTime = 0.2f;
        [SerializeField] protected Vector3 selectRotation;
        [SerializeField] protected Vector3 dragRotation;
        [SerializeField] protected Vector3 selectScale = Vector3.one;
        [Space(20)]
        [SerializeField] Sprite selectSprite;
        [SerializeField] Sprite deselectSprite;

        public virtual void Start()
        {
            _initialPosition = transform.position;
            _initialRotation = transform.eulerAngles;
            _initialScale = transform.localScale;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>(); 
        }

        public override void StartMission(float duration)
        {
            gameObject.SetActive(true);
            DOTween.Kill("EndMission" + transform.GetInstanceID());
            transform.DOMove(onScreenPos, duration).SetId("StartMission" + transform.GetInstanceID());
        }

        public override void EndMission(float duration)
        {
            DOTween.Kill("StartMission" + transform.GetInstanceID());
            transform.DOMove(offScreenPos, duration).OnComplete(() =>
            {
                gameObject.SetActive(false);
            }).SetId("EndMission" + transform.GetInstanceID());
        }

        public override void Select(Vector3 touchPosition)
        {
            transform.position = touchPosition;

            _spriteRenderer.sortingOrder += 2;
            if (selectSprite) _spriteRenderer.sprite = selectSprite;

            transform.DOScale(selectScale, scaleTime);
            transform.DORotate(selectRotation, rotateTime);
            DOTween.Kill("Reset" + transform.GetInstanceID());
        }

        public override void Deselect(Vector3 touchPosition)
        {
            this.PostEvent(EventID.ResetCheck);
            ResetTransform(touchPosition);
        }

        public virtual void ResetTransform(Vector3 touchPosition)
        {
            _spriteRenderer.sortingOrder -= 2;
            if (deselectSprite) _spriteRenderer.sprite = deselectSprite;

            string tweenID = "Reset" + transform.GetInstanceID();
            transform.DOScale(_initialScale, moveTime).SetId(tweenID);
            transform.DORotate(_initialRotation, moveTime).SetId(tweenID);
            transform.DOMove(onScreenPos, moveTime).SetId(tweenID).OnComplete(CheckComplete);
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {   
            transform.position = touchPosition;

            UpdateRotationBasedOnPosition(transform.position);
        }

        public virtual void CheckComplete()
        {
            if (canComplete && isComplete)
            {
                CompleteMission();
                this.PostEvent(EventID.OnMissionResult, true);
            }
        }

        public virtual void UpdateRotationBasedOnPosition(Vector3 newPosition)
        {
            if (HaveSameSign(newPosition.x, _initialPosition.x))
            {
                transform.DORotate(selectRotation, 0.3f);
            }
            else
            {
                transform.DORotate(selectRotation + dragRotation, 0.3f);
            }
        }

        public virtual bool HaveSameSign(float a, float b)
        {
            return (a >= 0 && b >= 0) || (a < 0 && b < 0);
        }
    }
}
