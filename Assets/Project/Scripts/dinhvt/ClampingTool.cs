using DG.Tweening;
using SR4BlackDev;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class ClampingTool : CleaningTool
    {
        public int targetPerClamp;

        [Header("Clamping Tool Settings")]
        public int _targetCount;
        [SerializeField] protected int numOfTarget;
        [SerializeField] int maxTargetPerClamp;
        protected List<IDropTarget> targetList = new List<IDropTarget>();
        [Space(20)]
        public Transform frontTool;
        [SerializeField] Vector3 clampRotation;

        protected Vector3 _initialFrontRotation;

        public override void Start()
        {
            base.Start();
            _initialFrontRotation = frontTool.localEulerAngles;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (CompareTagValue(collision, "Trash") && canComplete && !IsFull())
            {
                Clamping(collision.transform);
            }
        }

        public virtual void OnTriggerExit2D(Collider2D collision)
        {   
            if(CompareTagValue(collision, "Target") && canComplete && targetList.Count > 0)
            {
                Drop(collision.transform);

            }

            //if (collision.CompareTag("Target") && canComplete && targetList.Count > 0)
            //{
            //    Drop(collision.transform);
            //}
        }
  
        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            foreach (Transform child in transform)
            {
                child.GetComponent<SpriteRenderer>().sortingOrder += 2;
            }
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            foreach (Transform child in transform)
            {
                child.GetComponent<SpriteRenderer>().sortingOrder -= 2;
            }
        }

        public virtual void Clamping(Transform target)
        {
            IDropTarget dropTarget = target.GetComponent<IDropTarget>();
            targetList.Add(dropTarget);
            dropTarget.Dragged(transform);

            _targetCount++;
            targetPerClamp++;
            frontTool.DOLocalRotate(clampRotation, 0.05f);
        }

        public virtual void Drop(Transform slot)
        {
            foreach (IDropTarget target in targetList)
            {
                target.Dropped(slot);
                targetPerClamp--;
            }

            targetList.Clear();
            
            if (_targetCount == numOfTarget) isComplete = true;
            
            frontTool.DOLocalRotate(_initialFrontRotation, 0.2f);
        }

        public bool IsFull()
        {
            return targetPerClamp == maxTargetPerClamp;
        }
    }
}
