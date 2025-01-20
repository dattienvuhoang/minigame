using System.Collections;
using UnityEngine;

namespace dinhvt
{
    public class PlusButton : Mission
    {
        [Header("Settings")]
        [SerializeField] Animator animator;
        [SerializeField] AnimationClip clip;

        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            if (canComplete)
            {
                isComplete = true;

                _collider.enabled = false;

                animator.enabled = true;
                animator.Play(clip.name, 0, 0f);

                StartCoroutine(DisableAnimator());
            }
        }

        IEnumerator DisableAnimator()
        {
            yield return new WaitForSeconds(clip.length + 0.1f);
            animator.enabled = false;
            CompleteMission();
        }

    }
}
