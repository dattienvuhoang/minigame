using SR4BlackDev.UISystem;
using UnityEditor;
using UnityEngine;

namespace SR4BlackDev.Editor
{
    [CustomEditor(typeof(PopupAnim))]
    public class PopupAnimEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Accept Modify"))
            {
                PopupAnim popup = target as PopupAnim;
                
                 switch (popup.popupAnimType)
                {
                    case PopupAnimType.Animation:
                    {
                        if (popup.animationPopup == null)
                        {
                            popup.animationPopup = popup.gameObject.AddComponent<Animation>(); 
                            popup.openClip = Resources.Load<AnimationClip>("Animations/Popups/PopupBase/AnimationPopupOpen");
                            popup.closeClip = Resources.Load<AnimationClip>("Animations/Popups/PopupBase/AnimationPopupClose");

                        }
                        else
                        {
                            popup.animationPopup = popup.GetComponent<Animation>(); 
                            popup.openClip = Resources.Load<AnimationClip>("Animations/Popups/PopupBase/AnimationPopupOpen");
                            popup.closeClip = Resources.Load<AnimationClip>("Animations/Popups/PopupBase/AnimationPopupClose");
                        }

                        if (popup.animatorPopup)
                        {
                            DestroyImmediate(popup.animatorPopup);
                        }
                    }
                        break;
                    case PopupAnimType.Animator:
                        if (popup.animatorPopup == null)
                        {
                            popup.animatorPopup = popup.gameObject.AddComponent<Animator>();
                            UnityEditor.Animations.AnimatorController _controller = Resources.Load<UnityEditor.Animations.AnimatorController>("Animations/Popups/PopupBase/Popup");
                            popup.animatorPopup.runtimeAnimatorController = _controller;
                        }
                        
                        if (popup.animationPopup)
                        {
                            DestroyImmediate(popup.animationPopup);
                        }
                        break;
                }
            }
        }
    }
}