using Destructible2D;
using SR4BlackDev;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class PeelerController : CleaningTool
    {
        [Header("Destroy Settings")]
        public LayerMask Layers;
        public D2dDestructible.PaintType Paint;
        public Texture2D Shape;
        public Color Color = Color.white;
        public Vector2 Size = Vector2.one;
        [SerializeField] Transform cleanHolder;
        [SerializeField] float alphaRatioThreshold = 0.1f;
        [Space(20)]
        [SerializeField] KnifeController knife;

        [SerializeField] private VegetablesController _vegetable;
        [SerializeField]private D2dDestructibleSprite _vegetablePeel;

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            if (_vegetablePeel?.AlphaRatio < alphaRatioThreshold)
            {
                _vegetablePeel.gameObject.SetActive(false);
                knife.UpdateVegetableInBoard(_vegetable);
            } 
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);
            Clean(cleanHolder.position);
        }

        public void Clean(Vector2 position)
        {
            D2dStamp.All(Paint, position, Size, 0, Shape, Color, Layers);
        }

        public void UpdateVegetablePeel(VegetablesController vegetable, D2dDestructibleSprite peel)
        {
            _vegetable = vegetable;
            _vegetablePeel = peel;

            if (_vegetablePeel == null) knife.UpdateVegetableInBoard(_vegetable);
        }
    }
}
