using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class Booger : MonoBehaviour, IDropTarget
    {
        [SerializeField] float positionY;
        [SerializeField] float moveTime;

        private Collider2D col;

        private void Awake()
        {
            col = GetComponent<Collider2D>();
        }

        public void Dragged(Transform dragger)
        {
            transform.SetParent(dragger);
        }

        public void Dropped(Transform dropper)
        {   
            col.enabled = false;
            transform.DOMoveY(positionY, moveTime).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}
