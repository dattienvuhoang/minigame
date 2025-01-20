using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class NoseRing : Mission, ISelectableObject
    {
        public int id;
        private BoxCollider2D _boxCollider;
        private Vector2 _initialPos;

        [SerializeField] Vector2 selectScale;
        [SerializeField] LayerMask targetLayer;
        [SerializeField] float moveTime;

        private void Awake()
        {
            _initialPos = transform.position;
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        public override void Select(Vector3 touchPosition)
        {   
            DOTween.Kill(transform.name);
            transform.DOScale(selectScale, moveTime);
        }

        public override void Deselect(Vector3 touchPosition)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, targetLayer);

            if (hitInfo && hitInfo.transform != null)
            {
                MoveToBox(hitInfo.transform);
            }
            else
            {
                MoveToInitialPosition();
            }

            transform.DOScale(Vector2.one * 0.8f, moveTime);
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            transform.position = touchPosition + offset;
        }

        private void MoveToBox(Transform hitTransform)
        {
            Box jewelryBox2 = hitTransform.GetComponent<Box>();
            Transform slot = jewelryBox2.GetSlot(id);

            transform.DOMove(slot.position, moveTime).OnComplete(() =>
            {
                CompleteMission();
            });

            _boxCollider.enabled = false;
        }

        private void MoveToInitialPosition()
        {
            transform.DOMove(_initialPos, moveTime).SetId(transform.name);
        }


        public override void EndMission(float duration)
        {
            base.EndMission(duration);

            gameObject.SetActive(false);
        }
    }
}
