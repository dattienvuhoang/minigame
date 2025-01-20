using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Manhole : ClickableObject
    {
        private bool moveToTruePos;
        private bool isOut = false;
        private float speed;
        private bool isFadingOut;
        [SerializeField] private Transform truePos;
        [SerializeField] private GameObject dirtyWater;
        [SerializeField] private GameObject manholeAnim;

        //lerp
        public float scaleStart = 0.59f;
        public float scaleEnd = 0.55f;
        public float fadeDuration = 2f;
        private float currentLerpTime;
        private SpriteRenderer spriteRenderer;
        private Vector3 initialScale;
        public bool isOnTruePos { get; private set; }

        private Level2Controller level2Controller;
        private Level2Controller Level2Controller
        {
            get
            {
                if (level2Controller == null)
                {
                    level2Controller = GameObject.Find("GameController").GetComponent<Level2Controller>();
                }
                return level2Controller;
            }
            set
            {
                level2Controller = value;
            }
        }

        private void Awake()
        {
            spriteRenderer = dirtyWater.GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            isFadingOut = false;
            moveToTruePos = false;
            speed = 10f;
            truePos.position = new Vector3(truePos.position.x, truePos.position.y, transform.position.z);
            CheckOnTruePos();
        }
        private void Update()
        {
            MoveToTruePos();
            FadeOut();
            CheckColliderByStatus();
        }
        public override void OnMouseDown()
        {
            base.OnMouseDown();
            moveToTruePos = false;
            isOnTruePos = false;
            MouseController.instance.GetMousePos(transform);
        }
        private void OnMouseDrag()
        {
            transform.position = MouseController.instance.MouseDragging();
        }
        public override void OnMouseUp()
        {
            base.OnMouseUp();
            MouseController.instance.MouseUp(transform);
            Debug.Log(Vector3.Distance(transform.position, truePos.position));
            if (Vector3.Distance(transform.position, truePos.position) < 0.5f)
            {
                moveToTruePos = true;
            }
            else if (true)
            {
                isFadingOut = true;
            }
        }
        private void MoveToTruePos()
        {
            if (moveToTruePos)
            {
                transform.position = Vector3.MoveTowards(transform.position, truePos.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, truePos.position) < 0.02f)
                {
                    moveToTruePos = false;
                    isOnTruePos = true;
                }
            }
        }
        private void CheckOnTruePos()
        {
            if (Vector3.Distance(transform.position, truePos.position) < 0.1f)
            {
                isOnTruePos = true;
            }
            else
            {
                isOnTruePos = false;
            }
        }
        private void FadeOut()
        {
            if (isFadingOut)
            {
                manholeAnim.SetActive(true);
                currentLerpTime += Time.deltaTime;
                float lerpRatio = currentLerpTime / fadeDuration;

                float currentScale = Mathf.Lerp(scaleStart, scaleEnd, lerpRatio);
                dirtyWater.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

                Color currentColor = spriteRenderer.color;
                currentColor.a = Mathf.Lerp(1f, 0f, lerpRatio); 
                spriteRenderer.color = currentColor;

                if (lerpRatio >= 1f && !isOut)
                {
                    isFadingOut = false;
                    isOut = true;
                    dirtyWater.gameObject.SetActive(false);
                    Level2Controller.GoNextStatus();
                    Level2Controller.ShowBroomAndBrush();
                    Debug.Log("3");

                }
            }
        }
        
        private void CheckColliderByStatus()
        {
            if (Level2Controller.status != 1 && Level2Controller.status != 4)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
