using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class KitchenButton : MonoBehaviour
    {
        [SerializeField] private GameObject leftSpray;
        [SerializeField] private GameObject rightSpray;
        [SerializeField] private SpriteRenderer leftWaterSprite;
        [SerializeField] private SpriteRenderer rightWaterSprite;

        private void Start()
        {
        }
        public void Button_On()
        {
            leftSpray.SetActive(true);
            FadeInWater(leftWaterSprite);
        }

        private IEnumerator FadeInWater(SpriteRenderer water, float duration = 1f)
        {
            water.gameObject.SetActive(true);
            Color color = water.color;
            color.a = 0f;
            water.color = color;
            float timeElapsed = 0;

            while (timeElapsed < duration)
            {
                color.a = Mathf.Lerp(0f, 1f, timeElapsed / duration);
                water.color = color;
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            color.a = 1f;
            water.color = color;
        }
    }
}
