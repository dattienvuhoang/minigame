using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class DrawerController : ShelfManager, ISelectableObject
    {
        private BoxCollider2D _boxCol;
        private SpriteRenderer _spriteRenderer;

        public bool _isOpen = false;
        public int numOfFunitures;

        [SerializeField] Sprite openSprite;
        [SerializeField] Sprite closeSprite;


        private void Awake()
        {
            _boxCol = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Select(Vector3 touchPosition)
        {
            _isOpen = !_isOpen;
            _spriteRenderer.sprite = _isOpen ? openSprite : closeSprite;
        }

        public void Deselect(Vector3 touchPosition) { }


        public override void CheckFull()
        {
            if (_funitureCount == numOfFunitures)
            {
                _wardrobeManager.IncreaseCount();
                _spriteRenderer.sprite = closeSprite;
                _boxCol.enabled = false;
            }
        }
    }
}
