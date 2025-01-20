using UnityEngine;

namespace dinhvt
{
    public class Trash : MonoBehaviour, IDropTarget
    {
        protected Collider2D col;
        protected SpriteRenderer sp;

        public int inSlotSortingOrder;
        public float moveTime;

        private void Awake()
        {
            sp = GetComponent<SpriteRenderer>();
            col = sp.GetComponent<Collider2D>();
        }

        public virtual void Dragged(Transform dragger)
        {
            
        }

        public virtual void Dropped(Transform dropper)
        {
           
        }
    }
}
