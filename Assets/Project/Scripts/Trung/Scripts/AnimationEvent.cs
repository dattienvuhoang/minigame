using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class AnimationEvent : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        public void TurnOffObj()
        {
            gameObject.SetActive(false);
        }
        public void TurnOffAnim()
        {
            animator.enabled = false;
        }       

    }
}
