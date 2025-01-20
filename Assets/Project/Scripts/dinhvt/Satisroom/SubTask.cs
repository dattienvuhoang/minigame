using UnityEngine;

namespace dinhvt
{
    public class SubTask : MonoBehaviour, DraggableObject, ISelectableObject
    {
        protected TaskManager _taskManager;
        protected bool _isComplete;
        public virtual void Init(TaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        public virtual void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {

        }


        public virtual void Select(Vector3 touchPosition)
        {

        }

        public virtual void Deselect(Vector3 touchPosition)
        {

        }

        public virtual bool GetIsComplete()
        {
            return _isComplete;
        }

        public void UpdateRotation(Vector3 touchPosition, Vector3 offsetAngle)
        {
        }
    }
}
