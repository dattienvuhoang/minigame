using UnityEngine;

namespace dinhvt
{
    public class Slot : MonoBehaviour
    {
        public int id;
        public bool isFilled;
        private SpriteRenderer _spriteRend;

        private void Awake()
        {
            _spriteRend = GetComponent<SpriteRenderer>();
        }

        public int GetSortingOrder()
        {
            return _spriteRend.sortingOrder;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public Vector3 GetRotation()
        {
            return transform.eulerAngles;
        }

        public Vector3 GetLocalScale()
        {
            return transform.localScale;
        }
    }
}
