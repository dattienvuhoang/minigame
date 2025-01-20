using UnityEngine;

namespace dinhvt
{
    public class MetalPan : ItemController
    {

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition + offset;
        }

        public override void SetCanMissionComplete(bool isCan)
        {
            base.SetCanMissionComplete(isCan);

            _collider.enabled = isCan;
        }
    }
}
