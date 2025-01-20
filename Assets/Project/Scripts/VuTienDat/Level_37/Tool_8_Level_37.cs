using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Tool_8_Level_37 : MonoBehaviour
    {
        public Animator anim;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision!=null && collision.name=="MongChan")
            {
                anim.enabled = true;
                StartCoroutine(DisableAnim());
            }
        }
        IEnumerator DisableAnim()
        {
            yield return new WaitForSeconds(3.5f);
            anim.enabled = false;
            DragController_Level_37.instance.MoveToLast();
        }
    }
}
