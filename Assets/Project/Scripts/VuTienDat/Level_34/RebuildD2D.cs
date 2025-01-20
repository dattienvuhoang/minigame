using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class RebuildD2D : MonoBehaviour
    {
        [SerializeField] private D2dDestructibleSprite sprite;
        private void Awake()
        {
            sprite = transform.GetComponent<D2dDestructibleSprite>();
        }
        private void OnEnable()
        {
            sprite.Rebuild();
        }
      
    }
}
