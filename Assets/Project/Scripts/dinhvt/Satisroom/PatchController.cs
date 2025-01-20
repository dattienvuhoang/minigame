using UnityEngine;

namespace dinhvt
{
    public class PatchController : Mission
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            if (_spriteRenderer.enabled == false)
            {
                _spriteRenderer.enabled = true;
                isComplete = true;
                CompleteMission();
            }
        }
    }
}
