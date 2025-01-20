using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class SinkButtonController : ToolController
    {
        [Header("Button Settings")]
        [SerializeField] Animator sinkAnimator;

        public bool _isFaucetOpen;

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            if (!canComplete) return;

            _isFaucetOpen = !_isFaucetOpen;
            sinkAnimator.SetTrigger(_isFaucetOpen ? "Open" : "Close");
            isComplete = true;
            CheckComplete();
        }
    }
}
