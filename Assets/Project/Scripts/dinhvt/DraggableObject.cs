using UnityEngine;

namespace dinhvt
{
    public interface DraggableObject
    {
        void UpdatePosition(Vector3 touchPosition, Vector3 offset);
    }
}
