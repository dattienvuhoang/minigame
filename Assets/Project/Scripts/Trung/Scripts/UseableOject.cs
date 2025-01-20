using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trung
{
    public class UseableOject : ObjectController
    {
        [SerializeField] private Sprite useForm;
        private Sprite originalForm;
        public bool isClicked { get; private set; } = false;
        private SpriteRenderer spr;
        private Vector3 originalPos;
        private bool canMoveUseable;
        private bool isOnTruePos;
        private float speed;

        private void Update()
        {
            MoveToTruePos();
        }
        private void Start()
        {
            canMoveUseable = true;
            speed = 8f;
            isClicked = false;
            isOnTruePos = true;
        }
        private void Awake()
        {
            originalPos = gameObject.transform.position;
            spr = GetComponent<SpriteRenderer>();
            originalForm = spr.sprite;
        }

        private void MoveToTruePos()
        {
            if (!isClicked && !isOnTruePos)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPos, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, originalPos) < 0.1f)
                {
                    isOnTruePos = true;
                }
            }
        }
        private void OnMouseDown()
        {
            if (canMoveUseable)
            {
                isClicked = true;
                LayerController2D.instance.CheckLoseHeart();
                isOnTruePos = false;
                spr.sprite = useForm;
                MouseController.instance.GetMousePos(transform);
                transform.position = new Vector3(MouseController.instance.GetMouseWorldPos().x, MouseController.instance.GetMouseWorldPos().y, transform.position.z);
            }

        }
        private void OnMouseDrag()
        {
            if (canMoveUseable)
            {
                transform.position = new Vector3(MouseController.instance.GetMouseWorldPos().x, MouseController.instance.GetMouseWorldPos().y, transform.position.z);
            }
        }
        public void UpdateOriginPos()
        {
            originalPos = transform.position;
        }
        private void OnMouseUp()
        {
            if (canMoveUseable)
            {
                isClicked = false;
                TagController tagGame = gameObject.GetComponent<TagController>();

                if (tagGame != null)
                {
                    if (tagGame.tag == "paper")
                    {
                        if (LayerController2D.instance.curLeg == "left" && LayerController2D.instance.currentState == 4)
                        {
                            ClearWaxTask.instance.CheckWaxPaper(transform.position.y, transform.position.x);
                        }
                        else if (LayerController2D.instance.curLeg == "right" && LayerController2D.instance.currentState == 4)
                        {
                            ClearWaxTaskR.instance.CheckWaxPaperR(transform.position.y, transform.position.x);
                        }
                    }
                }

                spr.sprite = originalForm;
                MouseController.instance.MouseUp(transform);
            }
        }


    }
}
