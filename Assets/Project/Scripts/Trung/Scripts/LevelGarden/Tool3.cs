using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Tool3 : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagController tag = collision.gameObject.GetComponent<TagController>();
            if(tag != null)
            {
                if (tag.tag == "soil2")
                {
                    SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
                    Color color = sr.color;
                    if (color.a < 1f)
                    {
                        color.a += 0.25f;
                        sr.color = color;
                    }
                }
                else if (tag.tag == "soil1")
                {
                    SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
                    Color color = sr.color;
                    if (color.a > 0f)
                    {
                        color.a -= 0.25f;
                        sr.color = color;
                    }
                }
            }

        }
    }
}
