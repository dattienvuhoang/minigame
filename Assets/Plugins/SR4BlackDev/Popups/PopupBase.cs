using UnityEngine;
using System.Collections;
namespace SR4BlackDev.UISystem
{
    [RequireComponent(typeof(PopupAnim), typeof(CanvasGroup))]
    public class PopupBase : MonoBehaviour
    {
        [SerializeField] protected PopupAnimBase _popupAnim;
        private Coroutine _openCoroutine;
        private Coroutine _closeCoroutine;
        public PopupData PopupData { get; set; }
        public bool CanClose { get; set; }
        public LayerPopup LayerPopup => PopupData?.Layer ?? LayerPopup.Default;

        private void Reset()
        {
            _popupAnim = GetComponentInChildren<PopupAnimBase>();
        }

        public void Open(PopupData popupData)
        {
            Cancel();
            CanClose = true;
            PopupData = popupData;
            gameObject.SetActive(true);
            _openCoroutine = StartCoroutine(OpenAnim());
        }
                
        public virtual void Close()
        {
            if(!CanClose) return;
            PopupManager.Close(LayerPopup);
        }

        public void DoClose()
        {
            Cancel();
            _closeCoroutine = StartCoroutine(CloseAnim());
        }
        
        private void Cancel()
        {
            if(_openCoroutine != null) StopCoroutine(_openCoroutine);
            if(_closeCoroutine != null) StopCoroutine(_closeCoroutine);
        }

        IEnumerator OpenAnim()
        {
//            PopupManager.Popup.OpenStart(PopupData.Layer);
            OnOpenStart();
            if(_popupAnim) yield return _popupAnim.Open();
            OnOpenFinish();
//            PopupManager.Popup.OpenFinish(PopupData.Layer);
        }

        IEnumerator CloseAnim()
        {
            yield return null;
            PopupManager.CloseStart(PopupData.Layer);
            OnCloseStart();
            if(_popupAnim) yield return _popupAnim.Close();
            OnCloseFinish();
            gameObject.SetActive(false);
            PopupManager.CloseFinish(PopupData.Layer);
        }
        
        public virtual void Init(){}
        protected virtual void OnOpenStart(){}
        protected virtual void OnOpenFinish(){}
        protected virtual void OnCloseStart(){}
        protected virtual void OnCloseFinish(){}
    }
}