using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class DragBody : MonoBehaviour
    {
        public int index;
        public GameObject o;
        public Animator anim; 
        public float scale1, scale2, scale3;
        private void Start()
        {
            index = 0;
        }
       
        public void UpIndex()
        {
            index++;
            if (index == 1)
            {
                o.transform.DOScale(scale1, 0.25f);
            }
            else if (index == 2)
            {
                o.transform.DOScale(scale2, 0.25f);
            }
            else if (index == 3)
            {
                DragController_Level_4_2.instance.ShowDone();
                o.transform.DOScale(scale3, 0.25f).OnComplete(() =>
                {
                    anim.enabled = true;
                    o.transform.GetComponent<PlayAnim>().enabled = true;
                });
            }
        }
    }
}
