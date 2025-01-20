using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Level2GoOutObjects : UseableObjects
    {
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
        private void Update()
        {
            MoveToTruePos();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagController tag = collision.gameObject.GetComponent<TagController>();
            if(tag != null )
            {
                if (tag.tag == "destroy")
                {
                    gameObject.SetActive(false);
                    Level2Controller.TurnOffDestroy();
                    Level2Controller.ShowNextObject();
                }

            }
        }
        
        public override void MoveToTruePos()
        {
            if(Level2Controller.status != Level2Controller.curIndex)
            {
                Level2Controller.GoOut();
            }
            else
            {
                base.MoveToTruePos();
            }
        }

    }
}
