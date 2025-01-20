using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class SinkStopperController : ToolController
    {
        [Header("Sink Stopper")]
        [SerializeField] SpriteRenderer sinkStopperSlot;
        [Space(20)]
        [SerializeField] SpriteRenderer DirtySR;

        private bool _isOpen = false;

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition + offset;
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            _spriteRenderer.sortingOrder += 2;
            transform.DOScale(selectScale, scaleTime);
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            if (sinkStopperSlot.bounds.Contains(touchPosition) && !_isOpen)
            {
                Open();
            }
            else if (!sinkStopperSlot.bounds.Contains(touchPosition) && _isOpen)
            {
                Close();
            }
            else
            {
                _spriteRenderer.sortingOrder -= 2;
            }
            transform.DOScale(_initialScale, scaleTime);
        }

        private void Close()
        {
            if (DirtySR.color.a != 0f) DirtySR.DOFade(0f, 0.5f).OnComplete(() =>
            {
                _spriteRenderer.sortingOrder = _initialSortingOrder;
                _isOpen = false;
                isComplete = true;
                CheckComplete();
            });
        }

        private void Open()
        {
            transform.DOMove(sinkStopperSlot.transform.position, moveTime).OnComplete(() =>
            {
                _spriteRenderer.sortingOrder = sinkStopperSlot.sortingOrder;
                _isOpen = true;
                isComplete = true;
                CheckComplete();
            });
        }

        public override void SetCanMissionComplete(bool isCan)
        {
            base.SetCanMissionComplete(isCan);

            _collider.enabled = isCan;
        }

        public override void CompleteMission()
        {
            base.CompleteMission();

            _collider.enabled = false;
        }
    }
}
