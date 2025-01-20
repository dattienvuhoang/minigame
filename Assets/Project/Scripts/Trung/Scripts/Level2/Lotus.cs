using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Lotus : UseableObjects
    {
        [SerializeField] private List<LotusTruePos> truePos;
        [SerializeField] private Sprite setForm;
        [SerializeField] private Sprite oriForm;
        private LotusTruePos pos_;
        private bool moveToLotusTruePos = false;
        private bool canMove = true;
        public bool isDone { get; private set; } = false;

        private void Update()
        {
            if (canMove)
            {
                base.OnUpdate();
            }
            else
            {
                Debug.Log("go");
                MoveToLotusTruePos(pos_.pos);
            }
        }

        public override void OnMouseUp()
        {
            cancelClicked();
            MouseController.instance.MouseUp(transform);
            if (CheckTruePos())
            {
                moveToLotusTruePos = true;
            }
            else
            {
                moveToLotusTruePos = false;
            }
        }
        private void MoveToLotusTruePos(Vector3 pos)
        {
            if (moveToLotusTruePos)
            {
                Vector3 newPos = new Vector3(pos.x, pos.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPos, 5f * Time.deltaTime);
                if (Vector3.Distance(transform.position, newPos) < 0.03f)
                {
                    moveToLotusTruePos = false;
                }
            }
        }

        private bool CheckTruePos()
        {
            foreach (var pos in truePos)
            {
                if (Vector3.Distance(pos.pos, transform.position) < 0.6f && !pos.haveLotus)
                {
                    pos_ = pos;
                    gameObject.GetComponent<SpriteRenderer>().sprite = setForm;
                    canMove = false;
                    pos.SetLotus(true);
                    isDone = true;
                    return true;
                }
                pos.SetLotus(false);
            }
            canMove = true;
            isDone = false;
            GetComponent<SpriteRenderer>().sprite = oriForm;
            return false;
        }
    }
}


/*
[SerializeField] private List<LotusTruePos> truePos;
[SerializeField] private Sprite setForm;
private Sprite oriForm;
private bool isOnLotusTruePos;
private bool canMove;
private LotusTruePos curTruePos;

private void Awake()
{
    oriForm = gameObject.GetComponent<SpriteRenderer>().sprite;
}
private void Start()
{
    canMove = true;
    isOnLotusTruePos = false;
}
private void Update()
{
    if (canMove)
    {
        base.OnUpdate();
    }
    else
    {
        OnUpdate();
    }
}
public override void OnMouseDown()
{
    base.OnMouseDown();
    Debug.Log("down");
}
public override void OnUpdate()
{
    MoveToLotusTruePos(curTruePos.pos);

}
public override void OnMouseUp()
{
    canMove = false;
    if (CheckTruePos())
    {
        SetOnTruePos();
        cancelClicked();
    }
    else
    {
        canMove = true;
        base.OnMouseUp();
    }
}
private void MoveToLotusTruePos(Vector3 pos)
{
    if (isOnLotusTruePos)
    {
        Vector3 newPos = new Vector3(pos.x, pos.y, -0.1f);
        transform.position = Vector3.MoveTowards(transform.position, newPos, 5f * Time.deltaTime);
        if (Vector3.Distance(transform.position, newPos) < -0.1f)
        {
            isOnLotusTruePos = false;
        }
    }
}
private bool CheckTruePos()
{
    foreach (var pos in truePos)
    {
        if (Vector3.Distance(pos.pos, transform.position) < 0.3f && !pos.haveLotus)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = setForm;
            isOnLotusTruePos = true;
            curTruePos = pos;
            return true;
        }
    }
    gameObject.GetComponent<SpriteRenderer>().sprite = oriForm;
    canMove = true;
    return false;
}
*/
