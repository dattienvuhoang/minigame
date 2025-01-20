using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Wave_6_Glass : MonoBehaviour
    {
        [SerializeField] private List<D2dDestructibleSprite> listD2D;

        private void Start()
        {
            for (int i = 0; i < listD2D.Count; i++)
            {
                listD2D[i].Rebuild();
            }
        }
    }
}
