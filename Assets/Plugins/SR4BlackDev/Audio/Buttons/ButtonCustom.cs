using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SR4BlackDev.UI
{
     public class ButtonCustom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
     {
//          [SerializeField]
//          private AudioClip _downAudio, _upAudio, _clickAudio;
          [SerializeField]
          private AudioClip clipClick;
          [SerializeField]
          private bool useShowInter;
          [SerializeField]
          private Button.ButtonClickedEvent m_OnClick = new Button.ButtonClickedEvent();
          [SerializeField]
          private Button.ButtonClickedEvent m_OnDown = new Button.ButtonClickedEvent();
          [SerializeField]
          private Button.ButtonClickedEvent m_OnUp = new Button.ButtonClickedEvent();
          
          [SerializeField]
          private Button.ButtonClickedEvent m_InSelect = new Button.ButtonClickedEvent();
          
          [SerializeField]
          private Button.ButtonClickedEvent m_OutSelect = new Button.ButtonClickedEvent();

          public bool conditionInter = true;
          
          public void ForceInvoke()
          {
               m_OnClick.Invoke();
          }
          public Button.ButtonClickedEvent onClick
          {
               get { return m_OnClick; }
               set { m_OnClick = value; }
          }
          public Button.ButtonClickedEvent onUp
          {
               get { return m_OnUp; }
               set { m_OnUp = value; }
          }

          public Button.ButtonClickedEvent inSelect
          {
               get { return m_InSelect; }
               set { m_InSelect = value; }
          }
          
          public Button.ButtonClickedEvent outSelect
          {
               get { return m_OutSelect; }
               set { m_OutSelect = value; }
          }
          public void OnPointerDown(PointerEventData eventData)
          {
               UpTween.Kill();
               DownTween.Play();
               m_OnDown.Invoke();
          }

          public void OnPointerUp(PointerEventData eventData)
          {
               UpTween.Play();
               DownTween.Kill();
               m_OnUp.Invoke();
          }

          public void OnPointerClick(PointerEventData eventData)
          {
               m_OnClick.Invoke();
               InSelect();
               if (clipClick)
               {
                    AudioManager.PlayEffect(clipClick);
               }

               if (useShowInter)
               {
                    if(conditionInter)
                         this.PostEvent(EventID.ADS_SHOW_INTER);
               }
          }

          public Tweener DownTween
          {
               get { return transform.DOScale(Vector3.one * .9f, .1f); }
          }
          public Tweener UpTween
          {
               get { return transform.DOScale(Vector3.one, .1f); }
          }

          public virtual void OutSelect()
          {
               m_InSelect.Invoke();
          }

          public virtual void InSelect()
          {
              m_OutSelect.Invoke(); 
          }
     }

}
