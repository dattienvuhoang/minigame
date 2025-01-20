using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class SprayTool : DestructionTool
    {
        [Header("Spray Tool")]         
        [SerializeField] GameObject sprayObject;

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            sprayObject.SetActive(true);
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            sprayObject.SetActive(false);
        }
    }
}
