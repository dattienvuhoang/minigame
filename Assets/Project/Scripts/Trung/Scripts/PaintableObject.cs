using PaintCore;
using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class PaintableObject : ObjectController
    {
        [SerializeField] private GameObject brushes;
        [SerializeField] private Material cleanMaterial;
        private bool isClear;
        private bool cleared;
        
        private void Awake()
        {

        }
        private void Start()
        {
            TurnOffP3D();
        }
        private void TurnOffP3D()
        {
            isClear = false;
            cleared = false;
            brushes.SetActive(false);
        }
        void Update()
        {
            
        }
    }
}