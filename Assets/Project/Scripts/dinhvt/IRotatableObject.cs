using UnityEngine;

namespace dinhvt
{
    public interface IRotatableObject
    {
        void UpdateRotation(Vector3 touchPosition, float offsetAngle);
    }
}
