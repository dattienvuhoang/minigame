using DG.Tweening;
using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class LightingTool : CleaningTool
    {
        [Header("Light Tool Settings")]
        [SerializeField] GameObject target;
        [SerializeField] LayerMask targetLayer;
        [SerializeField] SpriteRenderer Light;


        public override void ResetTransform(Vector3 touchPosition)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, targetLayer);
            if (hitInfo && canComplete)
            {
                if (hitInfo.transform != null)
                {
                    isComplete = true;
                    transform.DOMove(hitInfo.transform.position, 0.5f).OnComplete(() =>
                    {
                        Light.DOFade(1f, 1.5f).OnComplete(() =>
                        {
                            target.GetComponent<SpriteRenderer>().DOFade(0f, 1.5f);
                            Light.DOFade(0f, 1.5f).OnComplete(() =>
                            {
                                target.SetActive(false);
                                transform.DOMove(onScreenPos, moveTime).OnComplete(CheckComplete);
                            });
                        });
                    });
                }
            }
            else
            {
                transform.DOMove(onScreenPos, moveTime).SetId(transform.name);
            }
        }
    }
}
