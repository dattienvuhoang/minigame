using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trung
{
    public class UseableObjects : ClickableObject
    {
        [SerializeField] private Sprite useForm;
        public float rotZ = 0;
        public float scale = 1f;
        private Sprite originalForm;
        private SpriteRenderer spr;
        private Vector3 originalPos;
        public bool isOnTruePos { get; private set; }
        private float speed;

        private void Update()
        {
            OnUpdate();
        }
        public virtual void OnUpdate()
        {
            MoveToTruePos();
        }
        private void Start()
        {
            OnStart();
        }

        public void OnStart()
        {
            speed = 9f;
            isOnTruePos = true;
        }
        private void Awake()
        {
            OnAwake();
        }

        public void OnAwake()
        {
            originalPos = gameObject.transform.position;
            spr = GetComponent<SpriteRenderer>();
            originalForm = spr.sprite;
        }
        public virtual void MoveToTruePos()
        {
            if (!isClicked && !isOnTruePos)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPos, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, originalPos) < 0.02f)
                {
                    isOnTruePos = true;
                }
            }
        }
        public void SetOnTruePos()
        {
            isOnTruePos = true;
        }
        public override void OnMouseDown()
        {
            if (true)
            {
                base.OnMouseDown();
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotZ);
                transform.localScale *= scale; 
                isOnTruePos = false;
                if (useForm!= null)
                {
                    spr.sprite = useForm;
                }
                
            }

        }
        private void OnMouseDrag()
        {
            if (true)
            {
                transform.position = new Vector3(MouseController.instance.GetMouseWorldPos().x, MouseController.instance.GetMouseWorldPos().y, transform.position.z);
            }
        }
        public virtual void UpdateOriginPos()
        {
            originalPos = transform.position;
        }
        public override void OnMouseUp()
        {
            if (true)
            {
                base.OnMouseUp();
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
                transform.localScale /= scale;
                if (originalForm!= null)
                {
                    spr.sprite = originalForm;
                }
            }
        }
    }
}
