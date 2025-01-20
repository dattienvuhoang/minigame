using Destructible2D;
using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class WiperController : ObjectController
    {
        [SerializeField] Sprite openSprite;
        [SerializeField] Sprite closeSprite;

        [Header("Destructible Settings")]
        public D2dDestructibleSprite destroyedObject;
        public LayerMask Layer;
        public D2dDestructible.PaintType Paint;
        public Texture2D Shape;
        public Color Color = Color.white;
        public Vector2 Size = Vector2.one;
        [SerializeField] float alphaRatioThreshold = 0.1f;

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            _spriteRenderer.sprite = openSprite;
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            _spriteRenderer.sprite = closeSprite;
            transform.DOMove(_initialPosition, time);

            D2dDestructibleSprite d2DDestructible = destroyedObject.GetComponent<D2dDestructibleSprite>();
            if (d2DDestructible.AlphaRatio < alphaRatioThreshold && !_isComplete)
            {
                _isComplete = true;
                _taskManager.UpdateTaskCount(_isComplete);
                destroyedObject.gameObject.SetActive(false);
            }
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset); 

            transform.position = touchPosition + offset;

            ClearDirty();
        }

        public virtual void ClearDirty()
        {
            D2dStamp.All(Paint, transform.position, Size, 0, Shape, Color, Layer);
        }
    }
}
