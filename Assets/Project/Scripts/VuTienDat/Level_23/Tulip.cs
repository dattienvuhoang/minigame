using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuTienDat;

namespace VuTienDat
{
    public class Tulip : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spVisual_1, spViusal_2;

        public void FadeTulip()
        {
            spVisual_1.DOFade(0, 1f);
            spViusal_2.DOFade(1, 1f);
        }
    }
}
