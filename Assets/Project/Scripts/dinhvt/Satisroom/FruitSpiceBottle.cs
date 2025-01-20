using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class FruitSpiceBottle : DestructionTool
    {
        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            wave.UpdateMissionSortingOrder(_spriteRenderer);
        }
    }
}
