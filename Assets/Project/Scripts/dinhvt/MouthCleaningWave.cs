using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class MouthCleaningWave : Wave
    {
        [Header("Start Wave")]       
        [SerializeField] GameObject mouth;
        [SerializeField] List<SpriteRenderer> frontMouthSprite;
        [SerializeField] List<GameObject> backMouthSprite;

        [Header("End Wave")]
        [SerializeField] Wave chooseWave;

        public override void InitWave()
        {
            base.InitWave();

            if (missions.Count > 0) missions[0].SetCanMissionComplete(true);
        }

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            StartCoroutine(FadeMouth(duration));
        }

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            FadeObject(mouth, 0f, duration);
        }

        public override void CompleteAllMission()
        {
            CompleteWave();
            waveManager.NextWave(this, chooseWave);
        }

        IEnumerator FadeMouth(float duration)
        {
            mouth.SetActive(true);
            foreach (SpriteRenderer spriteRenderer in frontMouthSprite)
            {
                spriteRenderer.DOFade(1f, duration);
            }

            yield return new WaitForSeconds(duration);

            foreach (GameObject gameObject in backMouthSprite)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
