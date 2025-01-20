using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class Wiper : WiperController
    {
        [Header("Wiper")]
        [SerializeField] DirtyObject dirtyRabbit;
        [SerializeField] GlassDoorController glassDoor;

        public override void ClearDirty()
        {
            if (dirtyRabbit.GetIsComplete() && glassDoor.GetIsComplete()) return;

            D2dStamp.All(Paint, transform.position, Size, 0, Shape, Color, Layer);
        }
    }
}
