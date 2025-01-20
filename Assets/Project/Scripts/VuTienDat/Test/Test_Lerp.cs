using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Test_Lerp : MonoBehaviour
    {
        [Header("Lerp color")]
        public Color startColor = Color.white;
        public Color endColor = Color.red;
        public float duration = 1f;
        private float t;

        [Header("Lerp Alpha")]
        public float startAlpha = 1f;  
        public float endAlpha = 0f;    
        public float durationAlpha = 1f;

        [Header("Sprite")]
        public SpriteRenderer spriteRenderer;

        void Start()
        {
            
        }

        void Update()
        {
            LerpAlpha();
        }
        public void LerpColor()
        {
            t += Time.deltaTime / duration;
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
        }
        public void LerpAlpha()
        {
            t += Time.deltaTime / durationAlpha;
            Color newColor = spriteRenderer.color;
            newColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
            spriteRenderer.color = newColor; 
            t = Mathf.Clamp01(t);
        }
    }
}
