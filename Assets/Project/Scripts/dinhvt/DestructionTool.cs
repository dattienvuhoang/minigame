using Destructible2D;
using SR4BlackDev;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class DestructionTool : CleaningTool
    {
        protected D2dDestructibleSprite _destructibleSprite;

        [Header("Destroy Settings")]
        public int _useIndex = 0;
        [SerializeField] protected List<GameObject> destroyedObjects;
        public LayerMask Layers;
        public D2dDestructible.PaintType Paint;
        public Texture2D Shape;
        public Color Color = Color.white;
        public Vector2 Size = Vector2.one;
        [SerializeField] Transform cleanHolder;
        [SerializeField] float alphaRatioThreshold = 0.1f;


        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            if (canComplete) Clean(cleanHolder.position);
        }

        public virtual void Clean(Vector2 position)
        {
            D2dStamp.All(Paint, position, Size, 0, Shape, Color, Layers);

            if (_destructibleSprite.AlphaRatio < alphaRatioThreshold)
            {
                isComplete = true;
            }
        }

        public override void CompleteMission()
        {
            base.CompleteMission();

            destroyedObjects[_useIndex].SetActive(false);

            if (_useIndex < destroyedObjects.Count - 1)
            {
                isComplete = false;
                _useIndex++;
            }
        }

        public override void SetCanMissionComplete(bool isCan)
        {
            base.SetCanMissionComplete(isCan);

            if (_useIndex < destroyedObjects.Count)
            {   
                _destructibleSprite = destroyedObjects[_useIndex].GetComponent<D2dDestructibleSprite>();
                _destructibleSprite.enabled = true;
            }
        }
    }
}
