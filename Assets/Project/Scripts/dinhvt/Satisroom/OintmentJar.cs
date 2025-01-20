using DG.Tweening;
using SR4BlackDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class OintmentJar : CleaningTool
    {
        public int targetCount;

        [Header("Ointment Jar Settings")]
        [SerializeField] int numOfTarget;
        [SerializeField] float fadeDuration;
        [SerializeField] Sprite medicineSprite;

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (CompareTagValue(collision, "Acne scar") && canComplete)
            {
                targetCount++;
                collision.transform.GetComponent<Collider2D>().enabled = false;
                SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
                Color color = spriteRenderer.color;
                color.a = 0f;
                spriteRenderer.color = color;
                spriteRenderer.sprite = medicineSprite;
                spriteRenderer.DOFade(1f, fadeDuration).OnComplete(() =>
                {
                    spriteRenderer.DOFade(0f, fadeDuration).OnComplete(() =>
                    {
                        collision.gameObject.SetActive(false);
                    });
                });

                if (targetCount == numOfTarget) isComplete = true;
            }
        }
    }
}
