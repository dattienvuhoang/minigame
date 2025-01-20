using UnityEngine;

namespace dinhvt
{
    public class StainController : Mission
    {
        [SerializeField] Transform water;
        [SerializeField] float cleanTime;

        public float _timer;
        private SpriteRenderer _spriteRend;
        private Animator _animator;

        private void Awake()
        {
            _spriteRend = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();   
        }


        private bool _playOneTime;
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_playOneTime) return;

            if (collision.transform == water)
            {
                float alphaValue = 1 - _timer / cleanTime;
                Fade(alphaValue);

                if (alphaValue <= 0.1f && canComplete)
                {
                    CompleteMission();

                    _playOneTime = true;
                    //_animator.Play();
                }
                else
                {
                    _timer += Time.deltaTime;
                }
            }
        }

        private void Fade(float alphaValue)
        {
            Color color = _spriteRend.color;
            color.a = alphaValue;
            _spriteRend.color = color;
        }
    }
}
