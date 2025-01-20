using UnityEngine;

namespace SR4BlackDev.UISystem
{
    public class ToastView : ToastItem
    {
//        [SerializeField] private Animator _animator;
        [SerializeField] private Animation _animation;
        [SerializeField] private GameObject _message;
        private void Reset() => _animation = GetComponent<Animation>();

        private void Awake()
        {
            if (_animation == null)
            {
                _animation = GetComponent<Animation>();
            }
        }

        public override void Show(string message)
        {
            _message.SetText(message);
            _animation.Play("showToast");
        }
    }
}