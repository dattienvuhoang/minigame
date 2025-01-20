using Destructible2D;
using SR4BlackDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class PressTool : CleaningTool
    {   
        protected List<D2dDestructibleSprite> _destructibleSprites = new List<D2dDestructibleSprite>();

        [Header("Destroy Settings")]
        [SerializeField] protected List<GameObject> destroyedObjects;
        public LayerMask Layers;
        public D2dDestructible.PaintType Paint;
        public Texture2D Shape;
        public Color Color = Color.white;
        public Vector2 Size = Vector2.one;
        [SerializeField] float alphaRatioThreshold = 0.1f;

        [SerializeField] List<GameObject> finePowders = new List<GameObject>();

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);
        }

        public override void Deselect(Vector3 touchPosition)
        {
            StartCoroutine(Press(transform.position));
        }

        public virtual void Clean(Vector2 position)
        {
            D2dStamp.All(Paint, position, Size, 0, Shape, Color, Layers);
            CheckAlphaRatio();
        }

        private void CheckAlphaRatio()
        {
            float totalAlphaRatio = 0f;
            foreach (D2dDestructibleSprite d2dDestructible in _destructibleSprites)
            {
                totalAlphaRatio += d2dDestructible.AlphaRatio;
                Debug.Log(d2dDestructible.name + " : " + d2dDestructible.AlphaRatio);
            }
            Debug.Log("Total : " + totalAlphaRatio);

            if (totalAlphaRatio < alphaRatioThreshold)
            {
                isComplete = true;

                foreach (GameObject obj in destroyedObjects)
                {
                    obj.SetActive(false);
                }
            }
        }

        public override void SetCanMissionComplete(bool isCan)
        {
            base.SetCanMissionComplete(isCan);

            foreach (GameObject obj in destroyedObjects)
            {
                D2dDestructibleSprite d2dDestructible = obj.GetComponent<D2dDestructibleSprite>();
                _destructibleSprites.Add(d2dDestructible);
            }

            foreach (D2dDestructibleSprite d2dDestructible in _destructibleSprites)
            {
                d2dDestructible.enabled = isCan;
            }

            foreach (GameObject finePowder in finePowders)
            {
                finePowder.SetActive(isCan);
            }
        }


        IEnumerator Press(Vector3 position)
        {
            Clean(position);
            yield return new WaitForSeconds(0.2f);

            base.Deselect(position);
        }
    }
}
