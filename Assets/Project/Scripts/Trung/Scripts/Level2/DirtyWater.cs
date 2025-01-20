using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class DirtyWater : MonoBehaviour
    {
        private bool isOut = false;
        private D2dDestructibleSprite d2d;
        private Level2Controller level2Controller;
        private Level2Controller Level2Controller
        {
            get
            {
                if (level2Controller == null)
                {
                    level2Controller = GameObject.Find("GameController").GetComponent<Level2Controller>();
                }
                return level2Controller;
            }
            set
            {
                level2Controller = value;
            }
        }
        private void Awake()
        {
            d2d = gameObject.GetComponent<D2dDestructibleSprite>();
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(d2d.AlphaRatio <= 0.009 && !isOut)
            {
                Level2Controller.GoNextStatus();
                isOut = true;
                Debug.Log("1");
            }
        }
    }
}
