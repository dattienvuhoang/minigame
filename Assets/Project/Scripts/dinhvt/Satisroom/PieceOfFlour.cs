using UnityEngine;

namespace dinhvt
{
    public class PieceOfFlour : MonoBehaviour
    {
        [SerializeField] Vector3 targetScale;
        [Space(20)]
        [SerializeField] Transform rolledOutFlour;
        [SerializeField] Vector3 targetRolledOutFlourScale;

        private float _totalTimeRoll;
        private float _rollingTimeRatio;
        [SerializeField] float timeThreshold;

        private SpriteRenderer _spriteRend;
        private SpriteRenderer _rolledOutSpriteRend;

        private Vector3 _initialScale;
        private Vector3 _initialRolledOutFlourScale;

        private Vector3 _scaleOffset;
        private Vector3 _scaleRolledOutFlourOffset;

        private void Awake()
        {
            _spriteRend = GetComponent<SpriteRenderer>();   
            _rolledOutSpriteRend = rolledOutFlour.GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _initialScale = transform.localScale;
            _scaleOffset = targetScale - _initialScale;

            _initialRolledOutFlourScale = rolledOutFlour.localScale;
            _scaleRolledOutFlourOffset = targetRolledOutFlourScale - _initialRolledOutFlourScale;
        }

        public void RolledOut()
        {
            _totalTimeRoll += Time.deltaTime;
            _rollingTimeRatio = _totalTimeRoll / timeThreshold;

            if (!rolledOutFlour.gameObject.activeInHierarchy) rolledOutFlour.gameObject.SetActive(true);

            transform.localScale = _initialScale + _rollingTimeRatio * _scaleOffset;
            rolledOutFlour.localScale = _initialRolledOutFlourScale + _rollingTimeRatio * _scaleRolledOutFlourOffset;

            Fade(_spriteRend, 1 - _rollingTimeRatio);
            Fade(_rolledOutSpriteRend, _rollingTimeRatio);

            if (_rollingTimeRatio >= 1)
            {   
                gameObject.SetActive(false);
            }
        }

        private void Fade(SpriteRenderer spriteRenderer, float alphaValue)
        {
            Color color = spriteRenderer.color;
            color.a = alphaValue;
            spriteRenderer.color = color;
        }

        public bool IsRolledOutComplete()
        {
            return _totalTimeRoll >= timeThreshold;
        }
    } 
}
