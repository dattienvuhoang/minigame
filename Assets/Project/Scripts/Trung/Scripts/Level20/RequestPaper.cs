using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Trung
{
    public class RequestPaper : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer darkPanel;
        [SerializeField] private RectTransform imageRect;
        private bool canClickX;
        private BoxCollider2D col;
        private bool canSetBig;
        private bool canSetSmall;
        private void Awake()
        {
            col = GetComponent<BoxCollider2D>();
        }
        void Start()
        {
            canSetBig = true;
            canSetSmall = true;
            canClickX = false;
        }


        private void OnMouseDown()
        {
            canSetBig = true;
            col.enabled = false;
            MoveOut();
        }

        private void MoveOut()
        {
            if (canSetBig)
            {
                StartCoroutine(Bigger(0.3f, new Vector3(1f, 1f, 1f)));
                canSetBig = false;
            }
        }
        public void MoveInClick()
        {
            if (canClickX)
            {
                canSetSmall = true;
                canClickX = false;
                MoveIn();
            }
        }
        private void MoveIn()
        {
            if (canSetSmall)
            {
                StartCoroutine(Smaller(0.3f, new Vector3(0.2f, 0.2f, 0.2f)));
                canSetBig = false;
            }
        }

        private IEnumerator Bigger(float duration, Vector3 targetScale)
        {
            Vector3 startScale = imageRect.localScale;

            darkPanel.gameObject.SetActive(true);
            Color color = darkPanel.color;
            color.a = 0f;
            darkPanel.color = color;

            float timeElapsed = 0;

            while (timeElapsed < duration)
            {
                imageRect.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / duration);

                color.a = Mathf.Lerp(0f, 0.6f, timeElapsed / duration);
                darkPanel.color = color;

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            imageRect.localScale = targetScale;

            darkPanel.color = color;
            canClickX = true;
            color.a = 0.6f;
        }

        private IEnumerator Smaller(float duration, Vector3 targetScale)
        {
            Vector3 startScale = imageRect.localScale;
            Color color = darkPanel.color;
            color.a = 0.6f;
            darkPanel.color = color;

            float timeElapsed = 0;

            while (timeElapsed < duration)
            {
                imageRect.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / duration);

                color.a = Mathf.Lerp(0f, 0.0f, timeElapsed / duration);
                darkPanel.color = color;

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            imageRect.localScale = targetScale;

            darkPanel.color = color;
            col.enabled = true;
            color.a = 0.0f;
            darkPanel.gameObject.SetActive(false);
        }

    }
}
