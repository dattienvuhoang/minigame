using DG.Tweening;
using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class DropTool : CleaningTool
    {
        [Header("Drop Tool")]
        [SerializeField] Transform startAnimHolder;
        [SerializeField] Animator animator;
        [SerializeField] AnimationClip clip;
        [Space(20)]
        [SerializeField] SpriteRenderer target;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            //_collider.enabled = false;
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            //_collider.enabled = true;
        }

        public override void ResetTransform(Vector3 touchPosition)
        {
            if (target.bounds.Contains(touchPosition) && canComplete)
            {
                this.PostEvent(EventID.OnToggleDragAbility, false);

                transform.DOMove(startAnimHolder.position, 0.5f).OnComplete(() =>
                {
                    _spriteRenderer.enabled = false;
                    animator.Play(clip.name, 0, 0f);
                    isComplete = true;

                    DOVirtual.DelayedCall(clip.length, () =>
                    {
                        _spriteRenderer.enabled = true;
                        transform.DORotate(Vector3.zero, 0.3f);
                        transform.DOMove(onScreenPos, moveTime).OnComplete(CheckComplete);

                        this.PostEvent(EventID.OnToggleDragAbility, true);
                    });
                });
            }
            else
            {
                ResetToInitialState();
            }
        }

        protected virtual void ResetToInitialState()
        {
            transform.DORotate(Vector3.zero, 0.3f);
            transform.DOMove(onScreenPos, moveTime).SetId(transform.name);
        }
    }
}
