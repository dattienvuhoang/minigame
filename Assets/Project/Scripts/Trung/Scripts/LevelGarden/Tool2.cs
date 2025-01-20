using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Tool2 : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagController tag = collision.gameObject.GetComponent<TagController>();
            if(tag.tag != null)
            {
                if (tag.tag == "soil1")
                {
                    SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
                    Color color = sr.color;
                    color.a += 0.25f;
                    if (color.a <= 1f)
                    {
                        sr.color = color;
                    }
                }
            }
        }
    }
}
