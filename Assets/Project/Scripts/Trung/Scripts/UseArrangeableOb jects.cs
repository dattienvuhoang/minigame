using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Trung
{
    public class UseArrangeableObjects : ArrangeObject
    {
        [SerializeField] private Sprite useForm;
        [SerializeField] private float rotZ = 0;
        [SerializeField] private float scale = 1f;
        [SerializeField] private List<GameObject> doneObjects;
        private Sprite oriForm;
        private float oriRotZ;
        private GameObject doneObject;
        private void Awake()
        {
            base.OnAwake();
            oriForm = gameObject.GetComponent<SpriteRenderer>().sprite;
            oriRotZ = transform.rotation.z;
            foreach (var doneObject in doneObjects)
            {
                doneObject.SetActive(false);
            }
        }
        private void Update()
        {
            base.OnUpdate();
            if(isOnTruePos)
            {
                FadeIn();
                doneObject = doneObjects[curPosIndex];
                gameObject.GetComponent<SpriteRenderer>().enabled = false;  
                gameObject.GetComponent<BoxCollider2D> ().enabled = false;
            }
        }
        public override void OnMouseDown()
        {
            base.OnMouseDown();
            if(useForm != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = useForm;
            }
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotZ);
            transform.localScale *= scale;
        }
        public override void OnMouseUp()
        {
            base.OnMouseUp();
            gameObject.GetComponent<SpriteRenderer>().sprite = oriForm;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, oriRotZ);
            transform.localScale /= scale;
        }

        private void FadeIn()
        {
            StartCoroutine(FadeInSprite(0.3f));
        }
        private IEnumerator FadeInSprite(float duration)
        {
            if(doneObject != null)
            {
                doneObject.SetActive(true);
                SpriteRenderer spriteRenderer = doneObject.GetComponent<SpriteRenderer>();
                Color color = spriteRenderer.color;
                color.a = 0f;
                spriteRenderer.color = color;

                float timeElapsed = 0;

                while (timeElapsed < duration)
                {
                    color.a = Mathf.Lerp(0f, 1f, timeElapsed / duration);
                    spriteRenderer.color = color;

                    timeElapsed += Time.deltaTime;
                    yield return null;
                }

                color.a = 1f;
                spriteRenderer.color = color;
            }
        }
    }
}
