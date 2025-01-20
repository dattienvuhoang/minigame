using Destructible2D;
using UnityEngine;

namespace VuTienDat_Game2
{
    public class Cleanner : MonoBehaviour
    {
        public LayerMask Layers = -1;
        public D2dDestructible.PaintType Paint;
        public Texture2D Shape;
        public Color Color = Color.white;
        public Vector2 Size = Vector2.one;
  
        private void Update()
        {
            Vector3 position  = this.transform.position;

            D2dStamp.All(Paint, position, Size, 0, Shape, Color, Layers);
        }
    }
}
