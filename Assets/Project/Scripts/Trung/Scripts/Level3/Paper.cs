using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Trung
{
    public class Paper : MonoBehaviour
    {
        [SerializeField] private Animator legShakeAnim;
        [SerializeField] private GameObject cryEmoji;
        [SerializeField] private SpriteRenderer waxSprite;
        private Color waxColor;
        private SpriteRenderer paperSprite;
        private Color paperColor;

        public float fadeDuration = 1f;

        private void Awake()
        {
            paperSprite = GetComponent<SpriteRenderer>();
            paperColor = paperSprite.color;
            waxColor = waxSprite.color;
        }
        private void Start()
        {
            legShakeAnim.enabled = false;
            cryEmoji.SetActive(false);
        }

        private void OnMouseDown()
        {
            if (ClearWaxTask.instance.done == 5)
            {
                StartCoroutine(FadeOut());
                if (LayerController2D.instance.curLeg == "left")
                {
                    if (LayerController2D.instance.DecreaseLeft() == 0)
                    {
                        LayerController2D.instance.SetDoneWax();
                    }

                }
            }
            else if (ClearWaxTaskR.instance.done == 5)
            {
                StartCoroutine(FadeOut());
                if (LayerController2D.instance.curLeg == "right")
                {
                    if (LayerController2D.instance.DecreaseRight() == 0)
                    {
                        LayerController2D.instance.SetWin();
                    }
                }
            }
        }
        private IEnumerator FadeOut()
        {
            float fadeSpeed = 1f / fadeDuration;
            float paperAlphaValue = paperColor.a;
            float waxAlphaValue = waxColor.a;
            if(legShakeAnim != null)
            {
                legShakeAnim.enabled = true;
            }
            if (cryEmoji != null)
            {
                cryEmoji.SetActive(true);
            }
            while (paperAlphaValue > 0f)
            {
                paperAlphaValue -= fadeSpeed * Time.deltaTime;
                waxAlphaValue -= fadeSpeed * Time.deltaTime;
                paperSprite.color = new Color(paperColor.r, paperColor.g, paperColor.b, paperAlphaValue);
                waxSprite.color = new Color(waxColor.r, waxColor.g, waxColor.b, waxAlphaValue);
                yield return null;
            }
            waxSprite.gameObject.SetActive(false);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
