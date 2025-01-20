
using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class Tweezers : ClampingTool
    {
        [Header("Tweezers")]
        [SerializeField] protected float timeThreshold;

        protected float _timer;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (CompareTagValue(collision, "Slot") && canComplete && targetList.Count > 0)
            {
                _timer += Time.deltaTime;

                if (_timer > timeThreshold)
                {
                    Drop(collision.transform);
                }
            }
        }

        public override void OnTriggerExit2D(Collider2D collision)
        {
            _timer = 0;
        }

        
    }
}
