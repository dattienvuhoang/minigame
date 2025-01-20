using DG.Tweening;
using SR4BlackDev;
using System.Collections;
using UnityEngine;

namespace dinhvt
{
    public class TowelController : CleaningTool
    {
        [Header("TowelController Settings")]
        [SerializeField] SpriteRenderer target;

        public override void Deselect(Vector3 touchPosition)
        {

            if (target.bounds.Contains(touchPosition) && canComplete)
            {
                StartCoroutine(Dry(touchPosition));
            }
            else
            {
                ResetTransform(touchPosition);
            }
        }

        IEnumerator Dry(Vector3 touchPosition)
        {
            _collider.enabled = false;
            transform.DOMove(target.transform.position, 0.5f);
            yield return new WaitForSeconds(2f);

            target.gameObject.SetActive(false);
            _collider.enabled = true;
            isComplete = true;
            ResetTransform(touchPosition);
        }
    }
}
