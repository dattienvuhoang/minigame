using DG.Tweening;
using SR4BlackDev;
using System.Collections;
using UnityEngine;

namespace dinhvt
{
    public class MakeupTray : CleaningTool
    {
        [Space(20)]
        [SerializeField] Transform startPourHolder;
        [SerializeField] Sprite pourSprite;
        [SerializeField] Sprite emptySprite;
        [SerializeField] SpriteRenderer powderSR;
        [SerializeField] SpriteRenderer metalPanSR;


        public override void Deselect(Vector3 touchPosition)
        {   
            if (metalPanSR.bounds.Contains(touchPosition) && canComplete)
            {
                isComplete = true;
                StartCoroutine(PourPowder(touchPosition));
            }
            else
            {
                base.Deselect(touchPosition);
            }
        }

        public override void SetCanMissionComplete(bool isCan)
        {
            base.SetCanMissionComplete(isCan);

            _collider.enabled = true;
        }

        IEnumerator PourPowder(Vector3 touchPosition)
        {       
            _collider.enabled = false;
            transform.DOMove(startPourHolder.position, 0.3f);
            yield return new WaitForSeconds(0.3f);

            powderSR.enabled = false;
            _spriteRenderer.sprite = pourSprite;
            yield return new WaitForSeconds(0.3f);

            metalPanSR.DOFade(1f, 0.7f);
            yield return new WaitForSeconds(0.7f);

            _spriteRenderer.sprite = emptySprite;
            base.Deselect(touchPosition);
        }
    }
}
