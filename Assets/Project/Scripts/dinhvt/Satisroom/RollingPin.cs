using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class RollingPin : ToolController
    {
        [Header("Target Settings")]
        [SerializeField] List<TargetObjects> targetObjects = new List<TargetObjects>();

        private bool _isInTargetCollider;
        private Transform _targetTrans;

        [System.Serializable]
        public class TargetObjects
        {
            public List<PieceOfFlour> pieceOfFlours = new List<PieceOfFlour>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (CompareTagValue(collision, "Small Flour") && canComplete)
            {
                _isInTargetCollider = true;
                _targetTrans = collision.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (CompareTagValue(collision, "Small Flour") && canComplete)
            {
                _isInTargetCollider = false;
                _targetTrans = null;
            }
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition + offset;

            if (_isInTargetCollider)
            {
                RollOutFlour();
            }
        }
        
        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            _spriteRenderer.sortingOrder += 2;
            transform.DORotate(selectRotation, 0.3f);
            DOTween.Kill(transform.name);
        }

        public override void Deselect(Vector3 touchPosition)
        {   
            base.Deselect(touchPosition);

            if (canComplete) isComplete = IsRollOutComplete();

            _spriteRenderer.sortingOrder -= 2;
            transform.DORotate(_initialRotation, 0.3f);
            transform.DOMove(_initialPosition, moveTime).SetId(transform.name).OnComplete(CheckComplete);
        }

        private void RollOutFlour()
        {
            _targetTrans.GetComponent<PieceOfFlour>().RolledOut();
        }

        private bool IsRollOutComplete()
        {
            foreach (PieceOfFlour piece in targetObjects[_useIndex].pieceOfFlours)
            {   
                if (!piece.IsRolledOutComplete()) return false;
            }

            return true;
        }
    }
}
