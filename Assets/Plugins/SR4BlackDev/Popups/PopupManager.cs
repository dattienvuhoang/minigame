using System.Collections.Generic;
using UnityEngine;

namespace SR4BlackDev.UISystem
{
    public partial class PopupManager : MonoBehaviour
    {
                
        #region Singleton
        private static PopupManager _instance;
        public static PopupManager instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject prefab = Resources.Load<GameObject>("Popups/PopupManager");
                    GameObject temp = Instantiate(prefab);
                    _instance = temp.GetComponent<PopupManager>();
                    DontDestroyOnLoad(temp);
                }
                return _instance;
            }
        }
        #endregion

        #region Unity Method

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        #region Properties
        
        [SerializeField] private LayerData[] _layerDatas;
        [SerializeField] private ToastManager _toast;
        [SerializeField] private GameObject _locker;
        private readonly Dictionary<LayerPopup, Queue<PopupData>> _popupQueue = new Dictionary<LayerPopup, Queue<PopupData>>();
        private readonly Dictionary<LayerPopup, PopupBase> _popupOpening = new Dictionary<LayerPopup, PopupBase>();
        private readonly Dictionary<string, PopupBase> _popupPool = new Dictionary<string, PopupBase>();
        private readonly List<LayerPopup> _popupBackList = new List<LayerPopup>();
        private static readonly LayerPopup[] PopupSequences = { LayerPopup.Notify };
        private static readonly LayerPopup[] PopupBackable = { LayerPopup.Sub1, LayerPopup.Sub2, LayerPopup.Notify };
        private const string PopupResourcePath = "Popups/";
        
        #endregion

        #region Private Method

        private void LockScreen(bool locked) => _locker.SetActive(locked);

        private void ShowToastItem(string message)
        {
            if(_toast)
                _toast.ShowToast(message);
            else
            {
                Debug.LogError("Missing Toast  game still running , just return");
            }
        }
        private void OpenPopup(string popupName, LayerPopup layerPopup, object data)
        {
            if (!_popupQueue.TryGetValue(layerPopup, out var queue))
            {
                if(!_popupQueue.ContainsKey(layerPopup))
                    _popupQueue.Add(layerPopup, queue = new Queue<PopupData>());
                else
                {
                    queue = new Queue<PopupData>();
                    _popupQueue[layerPopup] = queue;
                }
            }

            queue.Enqueue(new PopupData(){Name = popupName, Layer = layerPopup, Data = data});
            if(!_popupOpening.ContainsKey(layerPopup))
                OpenPopup(layerPopup);
            else if(!IsPopupSequence(layerPopup))
                ClosePopupWithLayer(layerPopup);
        }
        
        private void OpenPopup(LayerPopup layerPopup)
        {
            if(!HasPopupInQueue(layerPopup)) return;
            DoOpen(_popupQueue[layerPopup].Dequeue());
            if (_popupQueue[layerPopup].Count == 0) 
                _popupQueue.Remove(layerPopup);
        }
        
        private void DoOpen(PopupData popupData)
        {
            if (!LoadPopup(popupData.Name, popupData.Layer, out var popupBase)) return;
            popupBase.transform.SetParent( GetLayerRoot(popupData.Layer));
            popupBase.transform.SetAsLastSibling();
            popupBase.Open(popupData);
            if (!_popupOpening.ContainsKey(popupData.Layer))
                _popupOpening.Add(popupData.Layer, popupBase);
            else
                _popupOpening[popupData.Layer] = popupBase;
            if(IsBackable(popupData.Layer)) _popupBackList.Add(popupData.Layer);
        }
        
        private void ClosePopupWithLayer(LayerPopup layerPopup)
        {
            if(!IsLayerOpening(layerPopup)) return;
            if(!_popupOpening[layerPopup].CanClose) return;
            DoClose(layerPopup);
            if (layerPopup != LayerPopup.Main) return;
            if(IsLayerOpening(LayerPopup.Sub1)) DoClose(LayerPopup.Sub1);
            if(IsLayerOpening(LayerPopup.Sub2)) DoClose(LayerPopup.Sub2);
        }
        
        private void DoClose(LayerPopup layerPopup)
        {
            _popupOpening[layerPopup].DoClose();
            _popupOpening.Remove(layerPopup);
            if(IsBackable(layerPopup)) _popupBackList.Remove(layerPopup);
        }

        private void DoBack()
        {
            if(_popupBackList.Count == 0) return;
            ClosePopupWithLayer(_popupBackList[_popupBackList.Count - 1]);
        }

        private void CloseStartPopup(LayerPopup layerPopup)
        {
            if(IsPopupSequence(layerPopup)) return;
            OpenPopup(layerPopup);
        }

        private void CloseFinishPopup(LayerPopup layerPopup)
        {
            if(!IsPopupSequence(layerPopup)) return;
            OpenPopup(layerPopup);
        }
        
        private bool LoadPopup(string popupName, LayerPopup layerPopup, out PopupBase popupBase)
        {
            if (_popupPool.ContainsKey(popupName))
            {
                popupBase = _popupPool[popupName];
                return true;
            }
            var prefab = Resources.Load<PopupBase>(PopupResourcePath + popupName);
            if (prefab != null)
            {
                var popup = Instantiate(prefab, GetLayerRoot(layerPopup));
                popup.Init();
                popup.gameObject.SetActive(false);
                _popupPool.Add(popupName, popup);
                popupBase = popup;
                return true;
            }
            popupBase = null;
            Debug.LogError("Missing popup prefab: " + PopupResourcePath + popupName);
            return false;
        }
        
        private bool GetPopup(LayerPopup layerPopup, out PopupBase popupBase)
        {
            if (_popupOpening.ContainsKey(layerPopup))
            {
                popupBase = _popupOpening[layerPopup];
                return true;
            }
            popupBase = null;
            return false;
        }

        private void ClearPopup()
        {
            foreach (var popup in _popupOpening)
                Destroy(popup.Value.gameObject);
            foreach (var popup in _popupPool)
                Destroy(popup.Value.gameObject);
            
            _popupPool.Clear();
            _popupQueue.Clear();
            _popupOpening.Clear();
            _popupBackList.Clear();
        }
        
        private bool IsLayerOpening(LayerPopup layerPopup)
        {
            return _popupOpening.ContainsKey(layerPopup);
        }

        private bool HasPopupInQueue(LayerPopup layerPopup)
        {
            return _popupQueue.ContainsKey(layerPopup);
        }
        
        private bool IsPopupSequence(LayerPopup layerPopup)
        {
            for (int i = 0; i < PopupSequences.Length; i++)
                if (PopupSequences[i] == layerPopup)
                    return true;
            return false;
        }
        
        private bool IsBackable(LayerPopup layerPopup)
        {
            for (int i = 0; i < PopupBackable.Length; i++)
                if (PopupBackable[i] == layerPopup)
                    return true;
            return false;
        }
        
        private RectTransform GetLayerRoot(LayerPopup layer)
        {
            for (int i = 0; i < _layerDatas.Length; i++)
            {
                if (layer == _layerDatas[i].Layer)
                {
                    return _layerDatas[i].Rect;
                }
            }
            return null;
        }
        #endregion

        #region Static Method
        public static void SpawnPopup(string popupName)
        {
            instance.LoadPopup(popupName, LayerPopup.Main, out PopupBase popupBase);
        }

        public static void Open(string popupName, LayerPopup layerPopup, object data)
        {
            instance.OpenPopup(popupName,layerPopup,data);
        }
        
        public static void Open(string popupName, LayerPopup layerPopup)
        {
            instance.OpenPopup(popupName,layerPopup,null);
        }
        
        public static void Close(LayerPopup layerPopup)
        {
            instance.ClosePopupWithLayer(layerPopup);
        }
        
        public static Transform GetLayerTransform(LayerPopup layerPopup)
        {
            return instance.GetLayerRoot(layerPopup).transform;
        }
        
        public static void Lock(bool isLock)
        {
            instance.LockScreen(isLock);
        }
        public static void CloseStart(LayerPopup layerPopup)
        {
            instance.CloseStartPopup(layerPopup);
        }

        public static void CloseFinish(LayerPopup layerPopup)
        {
            instance.CloseFinishPopup(layerPopup);
        }

        public static void ShowToast(string textValue)
        {
            instance.ShowToastItem(textValue);
        }
        #endregion
    }
}