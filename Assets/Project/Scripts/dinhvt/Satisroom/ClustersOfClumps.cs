using UnityEngine;

namespace dinhvt
{
    public class ClustersOfClumps : MonoBehaviour
    {
        [SerializeField] float timeThreshold;
        [SerializeField] SpriteRenderer clustersOfClumps;
        [SerializeField] SpriteRenderer powder;


        private float _totalTimeGround;
        private float _groundTimeRatio;

        public void GroundIntoPowder()
        {
            _totalTimeGround += Time.deltaTime;
            _groundTimeRatio = _totalTimeGround / timeThreshold;

            Fade(clustersOfClumps, 1 - _groundTimeRatio);
            Fade(powder, _groundTimeRatio);

            if (_groundTimeRatio > 0)
            {
                powder.gameObject.SetActive(true);
            }

            if (_groundTimeRatio >= 1)
            {
                clustersOfClumps.gameObject.SetActive(false);
            }
        }

        private void Fade(SpriteRenderer spriteRenderer, float alphaValue)
        {
            Color color = spriteRenderer.color;
            color.a = alphaValue;
            spriteRenderer.color = color;
        }

        public bool IsGroundIntoComplete()
        {
            return _totalTimeGround >= timeThreshold;
        }
    }
}
