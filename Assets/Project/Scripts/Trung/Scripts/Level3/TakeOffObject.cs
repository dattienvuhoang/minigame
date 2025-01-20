using System.Collections;
using System.Collections.Generic;
using Trung;
using UnityEngine;

namespace Trung
{
    public class TakeOffObject : MonoBehaviour
    {
        private bool canMove;
        private float distance;
        private Vector3 firstPos;
        [SerializeField] private Material objectMaterial;
        private Color originalColor;
        private Color objectColor;
        private float fadeDuration = 0.5f;
        private void Awake()
        {
            originalColor = objectMaterial.color;  
            objectColor = objectMaterial.color;
        }
        private void Start()
        {
            CheckCanMove();
        }

        public void CheckCanMove()
        {
            if (LayerController2D.instance.curLeg == gameObject.name)
            {
                canMove = true;
            }
            else
            {
                canMove = false;
            }
        }
        private void OnMouseDown()
        {
            if (canMove)
            {
                Level3Fix();
                MouseController.instance.GetMousePos(transform);
            }

        }
        private void OnMouseDrag()
        {
            if (canMove)
            {
                transform.position = MouseController.instance.MouseDragging();
            }
        }

        private void OnMouseUp()
        {
            if (canMove)
            {
                MouseController.instance.MouseUp(transform);
                DistanceCount();
                if(distance > 0.5f)
                {
                    StartCoroutine(FadeOut());
                }
            }
        }
        private void DistanceCount()
        {
            distance = Vector3.Distance(transform.position, firstPos);
        }
        private IEnumerator FadeOut()
        {
            float fadeSpeed = 1f / fadeDuration;
            float alphaValue = objectColor.a;
            Level3();
            while (alphaValue > 0f)
            {
                alphaValue -= fadeSpeed * Time.deltaTime;
                objectMaterial.color = new Color(objectColor.r, objectColor.g, objectColor.b, alphaValue);
                yield return null;
            }
            Destroy(gameObject);
        }
        public void RestoreOriginalMaterial()
        {
            if (objectMaterial != null)
            {
                objectMaterial.color = originalColor;
            }
        }

        private void OnDestroy()
        {
            RestoreOriginalMaterial();           
        }
        private void Level3()
        {
            TagController tag = gameObject.GetComponent<TagController>();
            if (tag != null)
            {
                if (tag.tag == "sock")
                {
                    if (LayerController2D.instance != null)
                    {
                        LayerController2D.instance.TurnOnBrush();
                    }
                }
            }
        }
        private void Level3Fix()
        {
            TagController tag = gameObject.GetComponent<TagController>();
            if (tag != null)
            {
                if (tag.tag == "sock")
                {
                    if (LayerController2D.instance != null)
                    {
                        LayerController2D.instance.TurnOnSoapLeft();
                    }
                }
            }

        }
    }
    //
}
