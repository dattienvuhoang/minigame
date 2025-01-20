using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Trung
{
    public class ArrangeObject : MonoBehaviour
    {
        public List<TruePos> truePos;

        public int curPosIndex { get; private set; }    
        public TruePos curTruePos { get; set; }
        public float delta = 0.5f;
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
            curPosIndex = -1;
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
            //if(Level5Controller.instance == null )
            //{
            //    transform.position = new Vector3(0, -10, 0);
            //}
            CheckOnTruePos();
        }

        private void Update()
        {
            OnUpdate();

        }
        public void OnUpdate()
        {
            MoveToTruePos();
            //if (Level5Controller.instance == null)
            //{
            //    StartMove();
            //}
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
        public virtual void OnMouseDown()
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
                curTruePos = null;
                spriteRenderer.sortingOrder = 3;
                moveToTruePos = false;
                isOnTruePos = false;
                MouseController.instance.GetMousePos(transform);
                transform.rotation = new Quaternion(originalRot.x, originalRot.y, 0, 0);
            }
            if (Level14Controller.instance != null)
            {
                Level14Controller.instance.CloseAirPod();
                Level14Controller.instance.CheckPadOut();
                Level14Controller.instance.CheckGold();
            }
        }

        
        private void OnMouseDrag()
        {
            if (canMove)
            {
                transform.position = MouseController.instance.MouseDragging();
            }
        }

        public virtual void OnMouseUp()
        {
            if (canMove)
            {
                spriteRenderer.sortingOrder = 2;
                MouseController.instance.MouseUp(transform);
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
                    if (Level14Controller.instance != null)
                    {
                        Level14Controller.instance.CloseAirPod();
                        Level14Controller.instance.CheckPadOut();
                        Level14Controller.instance.CheckGold();
                    }
                    spriteRenderer.sortingOrder = 1;
                    transform.rotation = new Quaternion(originalRot.x, originalRot.y, 0, 0);
                    moveToTruePos = false;
                    transform.position = curTruePos.pos + new Vector3(0, 0, 0.1f);
                    isOnTruePos = true;
                    if(AllArangeObjects.instance != null)
                    {
                        if (AllArangeObjects.instance.CheckWinCondition())
                        {
                            Debug.Log("Dung cho");
                        }
                    }
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
                pos.UpdatePosition();
                if (!pos.isHavingObject && Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(pos.pos.x, pos.pos.y,0)) < delta)
                {
                    curTruePos = pos;
                    curPosIndex = truePos.IndexOf(pos);
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
        public void SetTruePos(List<TruePos> list)
        {
            foreach(var pos in list)
            {
                truePos.Add(pos);
            }
        }
        public void ClearTruePos()
        {
            truePos.Clear();
            curTruePos = null;
            isOnTruePos = false;
        }
        public void SetIsOnTruePos(bool status)
        {
            isOnTruePos = status;
        }
    }
}
