using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class FeelingTool : MonoBehaviour
    {
        public static FeelingTool instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        private IEnumerator FadeOut(SpriteRenderer layer, float duration = 1f)
        {
            Color color = layer.color;
            float timeElapsed = 0;
            while (timeElapsed < duration)
            {
                color.a = Mathf.Lerp(1f, 0f, timeElapsed / duration);
                layer.color = color;
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            color.a = 0f;
            layer.color = color;
            layer.gameObject.SetActive(false);
        }
        private IEnumerator FadeOut(GameObject obj, float duration = 1f)
        {
            SpriteRenderer layer = obj.GetComponent<SpriteRenderer>();
            Color color = layer.color;
            float timeElapsed = 0;
            while (timeElapsed < duration)
            {
                color.a = Mathf.Lerp(1f, 0f, timeElapsed / duration);
                layer.color = color;
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            color.a = 0f;
            layer.color = color;
            layer.gameObject.SetActive(false);
        }

        public void FadeOutImplement(GameObject obj, float duration = 1f)
        {
            StartCoroutine(FadeOut(obj, duration));
        }
        public void FadeOutImplement(SpriteRenderer layer, float duration = 1f)
        {
            StartCoroutine(FadeOut(layer, duration));
        }

        private IEnumerator FadeIn(GameObject obj, float duration = 1f)
        {
            obj.SetActive(true);
            SpriteRenderer layer = obj.GetComponent<SpriteRenderer>();
            Color color = layer.color;
            color.a = 0f;
            layer.color = color;
            float timeElapsed = 0;
            while (timeElapsed < duration)
            {
                color.a = Mathf.Lerp(0f, 1f, timeElapsed / duration);
                layer.color = color;
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            color.a = 1f;
            layer.color = color;
        }
        public void FadeInImplement(GameObject obj, float duration = 1f)
        {
            StartCoroutine(FadeIn(obj, duration));
        }

    }
}
