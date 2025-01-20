using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class GlassDoorController : SubTask
    {
        private bool _isOpen = false;
        private Vector3 _closePosition;

        [SerializeField] GlassDoorController otherDoor;
        [SerializeField] Vector3 openPosition;
        [SerializeField] float moveTime;
        [SerializeField] Collider2D slot;

        private void Start()
        {
            _closePosition = transform.position;
            _isComplete = !_isOpen;
        }

        public override void Select(Vector3 touchPosition)
        {   
            base.Select(touchPosition); 

            ToggleDoor();
            otherDoor?.ToggleDoor();
        }

        public void ToggleDoor()
        {   
            _isOpen = !_isOpen;
            slot.enabled = _isOpen;
            _isComplete = !_isOpen;
            _taskManager.UpdateTaskCount(_isComplete);

            Vector3 targetPos = _isOpen ? openPosition : _closePosition;
            transform.DOMove(targetPos, moveTime);
        }
    }
}
