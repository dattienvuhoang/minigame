using UnityEngine;

namespace dinhvt
{
    public class ChooseMission : Mission, ISelectableObject
    {       
        private Animator _animator;
        [SerializeField] AnimationClip animationClip;

        [Header("Choose Wave")]
        [SerializeField] Wave nextWave;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void StartMission(float duration)
        {
            wave.FadeObject(gameObject, 1f, duration);
        }

        public override void EndMission(float duration)
        {
            wave.FadeObject(gameObject, 0f, duration);
        }

        public override void Select(Vector3 touchPosition)
        {
            wave.GetWaveManager().NextWave(wave, nextWave);
            isComplete = true;
            CompleteMission();
        }

        public override void Deselect(Vector3 touchPosition) {  }


        public override void SetCanMissionComplete(bool isCan)
        {
            base.SetCanMissionComplete(isCan);

            GetComponent<Collider2D>().enabled = isCan;
        }

        public override void Blink()
        {
            _animator.Play(animationClip.name, 0, 0f);
        }
    }
}
