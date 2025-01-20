using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trungggg
{
    public class ArrangeObject : MonoBehaviour
    {
        public List<TruePos> truePos;

        private TruePos curTruePos;
        private SpriteRenderer spriteRenderer;
        private bool canMove;
        private bool moveToTruePos;
        private bool setStarted;
        public bool isOnTruePos { get; private set; }
        private float speed;
        private Quaternion originalRot;
        private Vector3 originalPos;

        private void Awake()
        {
            OnAwake();
        }
        public void OnAwake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            OnStart();
        }
        public void OnStart()
        {
            canMove = true;
            moveToTruePos = false;
            setStarted = true;
            speed = 6f;
            originalRot = transform.rotation;
            originalPos = transform.position;
            {
                transform.position = new Vector3(0, -10, 0);
            }
            CheckOnTruePos();
        }

        private void Update()
        {
            OnUpdate();

        }
        public void OnUpdate()
        {
            MoveToTruePos();
            {
                StartMove();
            }
        }

        private void StartMove()
        {
            if (setStarted)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPos, speed * 4 * Time.deltaTime);
                if (Vector3.Distance(transform.position, originalPos) < 0.01f)
                {
                    setStarted = false;
                }
            }
        }
        private void OnMouseDown()
        {
            if (canMove)
            {
                if (isOnTruePos)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                }
                if (curTruePos != null)
                {
                    curTruePos.SetObject(false);
                }
                spriteRenderer.sortingOrder = 3;
                moveToTruePos = false;
                isOnTruePos = false;
                transform.rotation = new Quaternion(originalRot.x, originalRot.y, 0, 0);
            }

        }

        
        private void OnMouseDrag()
        {
            if (canMove)
            {
            }
        }

        private void OnMouseUp()
        {
            if (canMove)
            {
                spriteRenderer.sortingOrder = 2;
                transform.rotation = originalRot;
                moveToTruePos = CheckTruePos();
            }
        }
        private void MoveToTruePos()
        {
            if (moveToTruePos)
            {
                transform.rotation = new Quaternion(originalRot.x, originalRot.y, 0, 0);
                transform.position = Vector3.MoveTowards(transform.position, curTruePos.pos, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, curTruePos.pos) < 0.01f)
                {

                    spriteRenderer.sortingOrder = 1;
                    transform.rotation = new Quaternion(originalRot.x, originalRot.y, 0, 0);
                    moveToTruePos = false;
                    transform.position = curTruePos.pos + new Vector3(0, 0, 0.1f);
                    isOnTruePos = true;

                }
            }
        }
        private void CheckOnTruePos()
        {
            if (CheckTruePos())
            {
                isOnTruePos = true;
                curTruePos.SetObject(true);
            }
            else
            {
                isOnTruePos = false;
            }
        }

        private bool CheckTruePos()
        {
            foreach(var pos in truePos)
            {
                if (!pos.isHavingObject && Vector3.Distance(transform.position, pos.pos) < 0.5f)
                {
                    curTruePos = pos;
                    curTruePos.SetObject(true);
                    return true;
                }
            }
            return false;
        }

        public void SetTruePos(TruePos pos)
        {
            truePos.Add(pos);
        }
        public void RemoveTruePos()
        {
            if(truePos.Count > 0)
            {
                truePos.RemoveAt(0);
            }
        }
    }
}
