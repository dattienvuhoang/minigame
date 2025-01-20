using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class ChildObj : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        SpriteRenderer parentSpriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            spriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder;
        }
    }
}
