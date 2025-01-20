using DG.Tweening;
using SR4BlackDev;
using SR4BlackDev.UISystem;
using System.Collections;
using UnityEngine;

namespace dinhvt
{
    public class AddFruitSpiceWave : Wave
    {
        [Header("Start Wave")]
        [SerializeField] GameObject[] fruits;

        [Header("End Wave")]
        [SerializeField] SpriteRenderer fruitsSR;
        [SerializeField] Sprite mixedFruitSprite;
        [SerializeField] GameObject[] waves;

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            if (missions.Count > 0) missions[0].SetCanMissionComplete(true);

            foreach (GameObject fruit in fruits)
            {
                fruit.SetActive(true);
            }
        }

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            StartCoroutine(TransitionWave(duration / 3));
        }

        IEnumerator TransitionWave(float duration)
        {
            fruitsSR.DOFade(0f, duration);
            yield return new WaitForSeconds(duration);
            fruitsSR.sprite = mixedFruitSprite;
            fruitsSR.DOFade(1f, duration);
            yield return new WaitForSeconds(duration);

            this.PostEvent(EventID.TransitionWave);
            yield return new WaitForSeconds(duration);
            foreach (GameObject wave in waves)
            {
                wave.SetActive(false);
            }
        }
    }
}
