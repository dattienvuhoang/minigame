using DG.Tweening;
using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class BlurringTool : CleaningTool
    {
        [Header("Blurring Tool Settings")]
        public int targetCount;
        [SerializeField] private int numOfAcne;
        [SerializeField] private float fadeDuration;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (canComplete && CompareTagValue(collision, "Acne"))
            {
                ProcessMissionCollision(collision);
            }
        }


        private void ProcessMissionCollision(Collider2D collision)
        {
            targetCount++;
            collision.enabled = false;

            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.DOFade(0f, fadeDuration).OnComplete(() =>
                {
                    collision.gameObject.SetActive(false);
                });
            }

            if (targetCount >= numOfAcne)
            {
                isComplete = true;
            }
        }
    }
}
