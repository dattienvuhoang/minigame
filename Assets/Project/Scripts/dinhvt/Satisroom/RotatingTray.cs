using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class RotatingTray : Mission
    {

        [Header("Rotating Tray")]
        [SerializeField] float rotateSpeed;
        [SerializeField] List<SpriteRenderer> ingredients = new List<SpriteRenderer>();
        [SerializeField] float timeThreshold;

        private int _ingredientIdx;
        public float _totalRotateTime;

        public override void UpdateRotation(Vector3 touchPosition, float angleOffset)
        {
            base.UpdateRotation(touchPosition, angleOffset);

            Vector3 offsetPos = transform.position - touchPosition;
            float angle = Mathf.Atan2(offsetPos.y, offsetPos.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle * rotateSpeed + angleOffset);

            if (canComplete) MixIngredients();
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            if (_totalRotateTime > timeThreshold)
            {   

                isComplete = true;

                _totalRotateTime = 0f;
                if (_ingredientIdx > 0) ingredients[_ingredientIdx - 1].gameObject.SetActive(false);
                foreach (Transform child in ingredients[_ingredientIdx].transform)
                {
                    child.gameObject.SetActive(false);
                }

                CompleteMission();
            }
        }

        public override void CompleteMission()
        {
            base.CompleteMission();

            if (_ingredientIdx < ingredients.Count - 1)
            {
                _ingredientIdx++;
                isComplete = false;
            }
        }

        private void MixIngredients()
        {
            if (_totalRotateTime <= timeThreshold)
            {
                _totalRotateTime += Time.deltaTime;
                Fade(ingredients[_ingredientIdx], _totalRotateTime / timeThreshold);
                if (_ingredientIdx > 0) 
                    Fade(ingredients[_ingredientIdx - 1], 1 - _totalRotateTime / timeThreshold);
            }
        }

        private void Fade(SpriteRenderer spriteRenderer, float alphaValue)
        {
            Color color = spriteRenderer.color;
            color.a = alphaValue;
            spriteRenderer.color = color;
        }
    }
}
