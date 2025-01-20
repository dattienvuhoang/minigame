using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Billet : UseableObjects
    {
        [SerializeField] private D2dDestructibleSprite soapLayer;
        [SerializeField] private D2dDestructibleSprite dirtyLayer;
        [SerializeField] private GameObject cleaner;
        private bool canMove = false;
        void Start()
        {
            cleaner.SetActive(false);
        }
        void Update()
        {
            base.OnUpdate();
            CheckAlpha();
            MoveOut();
        }

        public override void OnMouseDown()
        {
            base.OnMouseDown();
            dirtyLayer.enabled = true;
            soapLayer.enabled = false;
        }

        public override void OnMouseUp()
        {
            base.OnMouseUp();
            dirtyLayer.enabled = false;
        }

        private void CheckAlpha()
        {
            if (dirtyLayer.AlphaRatio < 0.02 && !isClicked)
            {
                canMove = true;
            }
        }

        private void MoveOut()
        {
            if (canMove)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(10, 0, 0) + gameObject.transform.position, 20f * Time.deltaTime);
                if (gameObject.transform.position.x > 8)
                {
                    canMove = false;
                    gameObject.SetActive(false);
                    cleaner.SetActive(true);
                    soapLayer.enabled = true;
                }
            }
        }
    }
}
