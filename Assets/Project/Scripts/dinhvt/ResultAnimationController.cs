using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class ResultAnimationController : MonoBehaviour
    {       
        public static ResultAnimationController Instance;
        private Animator _animator;

        private void Awake()
        {
            Instance = this;

            this.RegisterListener(EventID.OnMissionResult, OnMissionResult);
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            this.RemoveListener(EventID.OnMissionResult, OnMissionResult);
        }

        public void OnMissionResult(object sender, object result)
        {
            if ((bool) result)
                _animator.Play("Like_Emoji", 0, 0f);
            else
                _animator.Play("Angry_Emoji", 0, 0f);
        }
    }
}
