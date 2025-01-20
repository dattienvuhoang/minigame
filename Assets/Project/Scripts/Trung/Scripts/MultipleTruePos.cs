using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class MultipleTruePos : TruePos
    {
        public override void SetObject(bool status)
        {
            status = false;
            base.SetObject(status);
        }
    }
}
