using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Trung
{
    public class Soap : ClickableObject
    {
        [SerializeField] private Animator anim;
        [SerializeField] private Transform usePos;
        [SerializeField] private SpriteRenderer soapLayer;
        [SerializeField] private GameObject waterLayer;

        private void OnMouseDrag()
        {
            if (true)
            {
                transform.position = new Vector3(MouseController.instance.GetMouseWorldPos().x, MouseController.instance.GetMouseWorldPos().y, transform.position.z);
            }
        }
        public override void OnMouseUp()
        {
            base.OnMouseUp();
            if(Vector3.Distance(transform.position, usePos.position) < 1.5f)
            {
                anim.enabled = true;
                StartCoroutine(FadeInSoap(soapLayer));
            }
        }
        private IEnumerator FadeInSoap(SpriteRenderer soap, float duration = 1.5f)
        {
            soap.gameObject.SetActive(true);
            waterLayer.SetActive(false);
            Color color = soap.color;
            color.a = 0f;
            soap.color = color;
            float timeElapsed = 0;

            while (timeElapsed < duration)
            {
                color.a = Mathf.Lerp(0f, 1f, timeElapsed / duration);
                soap.color = color;
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            color.a = 1f;
            soap.color = color;
            KitchenController.instance.GoNextStatus();
        }
    }
}
