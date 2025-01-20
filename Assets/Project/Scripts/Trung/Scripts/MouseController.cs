using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Trung
{

    public class MouseController : MonoBehaviour
    {
        private Vector3 offset;
        private float originalRotationZ;
        private PolygonCollider2D objCollider;
        private float zPos;
        public static MouseController instance;
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

        public void GetMousePos(Transform objTransform)
        {
            zPos = objTransform.position.z;
            originalRotationZ = objTransform.eulerAngles.z;
            offset = objTransform.position - GetMouseWorldPos();
            PickUpEffect(objTransform);
        }

        public Vector3 MouseDragging()
        {
            Vector3 newPos = GetMouseWorldPos() + offset;
            return newPos;
        }
        private void PickUpEffect(Transform objTransform, bool isRotate = false)
        {
            //bigger
            objTransform.localScale *= 1.3f;

            //Set rotation = 0
            if (isRotate)
            {
                Vector3 currentRotation = objTransform.eulerAngles;
                currentRotation.z = 0;
                objTransform.eulerAngles = currentRotation;
            }
            //hide collider
            objCollider = objTransform.gameObject.GetComponent<PolygonCollider2D>();
            if (objCollider != null)
            {
                objCollider.enabled = false;
            }

        }
        private void DropDownEffect(Transform objTransform, bool isRotate = false)
        {
            //set original size
            objTransform.localScale /= 1.3f;

            //set original rotation
            if (isRotate)
            {
                Vector3 currentRotation = objTransform.eulerAngles;
                currentRotation.z = originalRotationZ;
                objTransform.eulerAngles = currentRotation;
            }


            //show collider
            if (objCollider != null)
            {
                objCollider.enabled = true;
                objCollider = null;
            }
        }
        public Vector3 GetMouseWorldPos()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = zPos;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }
        public void MouseUp(Transform objTransform)
        {
            DropDownEffect(objTransform);
        }

        public void MoveToTruePosition(Transform objTransform, Transform truePosition)
        {
            StartCoroutine(MoveToTruePos(objTransform, truePosition));
        }
        private IEnumerator MoveToTruePos(Transform objTransform, Transform truePosition)
        {
            Vector3 startPosition = objTransform.position;
            Vector3 endPosition = truePosition.position;
            float duration = 1f;
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                objTransform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            objTransform.position = endPosition;
        }
    }
}