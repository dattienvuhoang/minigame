using SR4BlackDev.Editor;
using UnityEngine;
using System.Collections;
namespace SR4BlackDev.UISystem
{
    public class PopupAnim : PopupAnimBase
    {
        public PopupAnimType popupAnimType = PopupAnimType.Animator;
        private float _openDuration;
        private float _closeDuration;
        #region Animation
        [EditorAtributeCustom.ConditionEnum("popupAnimType", (int)PopupAnimType.Animation)]
        public Animation animationPopup;
        
        [EditorAtributeCustom.ConditionEnum("popupAnimType", (int)PopupAnimType.Animation)]
        public AnimationClip openClip;
        
        [EditorAtributeCustom.ConditionEnum("popupAnimType", (int)PopupAnimType.Animation)]
        public AnimationClip closeClip;
        
        #endregion
        
        #region Animatior
        
        [EditorAtributeCustom.ConditionEnum("popupAnimType", (int)PopupAnimType.Animator)]
        public Animator animatorPopup;
        private static readonly int _open = Animator.StringToHash("Open");
        private static readonly int _close = Animator.StringToHash("Close");
        #endregion
        
        private YieldInstruction _waitForClose, _waitForOpen;
        private CustomYieldInstruction _waitForSecondsRealtimeClose, _waitForSecondsRealtimeOpen;    
        private bool _unscaleTime;
        
        public void Awake()
        {
            switch (popupAnimType)
            {
                case PopupAnimType.Animation:
                    if (animationPopup)
                    {
                        openClip.legacy = true;
                        closeClip.legacy = true;
                        _openDuration = openClip.length;
                        _closeDuration = closeClip.length;
                        _waitForOpen = new WaitForSeconds(_openDuration);
                        _waitForClose = new WaitForSeconds(_closeDuration);

                    }
                    break;
                case PopupAnimType.Animator:
                    if (animatorPopup)
                    {
                        var clips = animatorPopup.runtimeAnimatorController.animationClips;
                        _openDuration = clips[0].length;
                        _closeDuration = clips[1].length;
                        _waitForOpen = new WaitForSeconds(_openDuration);
                        _waitForClose = new WaitForSeconds(_closeDuration);
                        if (animatorPopup.updateMode == AnimatorUpdateMode.UnscaledTime)
                        {
                            _waitForSecondsRealtimeOpen = new WaitForSecondsRealtime(_openDuration);
                            _waitForSecondsRealtimeClose = new WaitForSecondsRealtime(_closeDuration);
                            _unscaleTime = true;
                        }
                    }

                    break;
            }

        }
        
                public override IEnumerator Open()
        {
            switch (popupAnimType)
            {
                case PopupAnimType.Animation:
                    animationPopup.AddClip(openClip , openClip.name);
                    if (animationPopup.clip != openClip)
                    {
                        animationPopup.clip = openClip;
                        animationPopup.CrossFade(openClip.name, .15f);
                    }
                    else
                    {
                        animationPopup.Play(openClip.name);
                    }
                    yield return _waitForOpen;
                    animationPopup.clip = null;
                    break;
                case PopupAnimType.Animator:
                    if(!animatorPopup) yield break;
                    animatorPopup.SetTrigger(_open);
                    if(!_unscaleTime)
                        yield return _waitForOpen;
                    else
                    {
                        yield return _waitForSecondsRealtimeOpen;
                    }
                    break;
            }
        }

        public override IEnumerator Close()
        {
            switch (popupAnimType)
            {
                case PopupAnimType.Animation:
                    animationPopup.AddClip(closeClip, closeClip.name);
                    if (animationPopup.clip != closeClip)
                    {
                        animationPopup.clip = closeClip;
                        animationPopup.CrossFade(closeClip.name, .15f);
                    }
                    else
                    {
                        animationPopup.Play(closeClip.name);
                    }
                    yield return _waitForClose;
                    animationPopup.clip = null;
                    break;
                case PopupAnimType.Animator:
                    if(!animatorPopup) yield break;
                    animatorPopup.SetTrigger(_close);
                    if(!_unscaleTime)
                        yield return _waitForClose;
                    else
                    {
                        yield return _waitForSecondsRealtimeClose;
                    }
                    break;
            }

        }
    }
}