using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class PlayAnim : MonoBehaviour
    {
        public Animator animator;
        public string animationStateName;
        private float currentAnimationTime = 0f;
        private bool isPlaying = false;
        private bool isMouseDown = false;

        private void Start()
        {

        }
        void Update()
        {
            /*isMouseDown = DragController_Level_4_2.instance.isDragging;
            if (isMouseDown)
            {
                if (!isPlaying)
                {
                    animator.Play(animationStateName, 0, currentAnimationTime);
                    animator.speed = 1f;
                    isPlaying = true;
                }
            }
            else if (!isMouseDown)
            {
                if (isPlaying)
                {
                    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                    currentAnimationTime = stateInfo.normalizedTime % 1;
                    animator.speed = 0f;
                    isPlaying = false;
                }
            }*/
        }
        public void Play()
        {
            animator.Play(animationStateName, 0, currentAnimationTime);
            animator.speed = 1f;

        }

        float nTime;
        public void Stop()
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            currentAnimationTime = stateInfo.normalizedTime % 1;

            nTime = stateInfo.normalizedTime;
            if (nTime > 1.0f)
            {
                if (this.name == "BoxDragHead")
                {
                    this.transform.GetComponent<TagGameObject>().tagValue = "SnowMan";
                }
                DragController_Level_4_2.instance.UpIndexSnow();
                Debug.Log("Finished   2");
                animator.enabled = false;
            }
            animator.speed = 0f;
        }
    }
}
