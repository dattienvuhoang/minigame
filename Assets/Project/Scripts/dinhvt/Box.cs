using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class Box : MonoBehaviour
    {
        [SerializeField] Transform[] slots;

        public Transform GetSlot(int index)
        {
            return slots[index];
        }
    }
}
