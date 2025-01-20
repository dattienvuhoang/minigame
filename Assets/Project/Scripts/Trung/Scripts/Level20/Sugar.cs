using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Trung
{
    public class Sugar : UseableObjects
    {
        [SerializeField] private Transform usePos;
        [SerializeField] private Animator animator;
        private Vector3 oriPos;
        private void Awake()
        {
            base.OnAwake();
            animator = GetComponent<Animator>();
        }
        private void Start()
        { 
            base.OnStart();
            oriPos = transform.position;
        }
        public override void OnMouseUp()
        {
            base.OnMouseUp();
            if(Vector3.Distance(transform.position, usePos.position)<1f)
            {
                animator.enabled = true;
                animator.Play("SugarBottle", 0,0);
            }
        }
    }
}
