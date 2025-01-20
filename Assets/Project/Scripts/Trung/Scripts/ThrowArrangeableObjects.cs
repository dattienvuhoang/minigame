using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class ThrowArrangeableObjects : ArrangeObject
    {
        private void Awake()
        {
            base.OnAwake();
        }
        private void Update()
        {
            base.OnUpdate();
            if (isOnTruePos)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
