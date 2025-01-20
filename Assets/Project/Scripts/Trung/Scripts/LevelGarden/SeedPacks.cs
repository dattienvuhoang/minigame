using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class SeedPacks : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D seedBox;

        private void Start()
        {
            seedBox.enabled = false;
        }

        private void OnMouseDown()
        {
            seedBox.enabled = true;
        }
        private void OnMouseUp()
        {
          seedBox.enabled=false;
        }
    }
}
