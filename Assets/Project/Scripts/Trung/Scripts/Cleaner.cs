using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Cleanner : MonoBehaviour
    {
        public LayerMask Layers = -1;
        public D2dDestructible.PaintType Paint;
        public Texture2D Shape;
        public Color Color = Color.white;
        public Vector2 Size = Vector2.one;
        private Cleanner cleaner;

        private void Awake()
        {
            cleaner = gameObject.GetComponent<Cleanner>();
        }

        private void Start()
        {
            //cleaner.enabled = false;
        }
        private void Update()
        {
            Vector3 position = gameObject.transform.position;
            D2dStamp.All(Paint, position, Size, 0, Shape, Color, Layers);
        }

        private void OnMouseDown()
        {
            cleaner.enabled = true;
        }

        private void OnMouseUp()
        {
            cleaner.enabled = false;
        }
    }
}
