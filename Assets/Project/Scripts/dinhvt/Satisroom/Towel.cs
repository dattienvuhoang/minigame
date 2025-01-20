using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class Towel : Funiture
    {

        [SerializeField] Sprite selectSprite;
        [SerializeField] Sprite deselectSprite;

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);
           
            _spriteRenderer.sprite = selectSprite;
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            if (!_isComplete) _spriteRenderer.sprite = deselectSprite;
        }
    }
}
