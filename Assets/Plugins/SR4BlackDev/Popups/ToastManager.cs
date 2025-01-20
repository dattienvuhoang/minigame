using System.Collections.Generic;
using UnityEngine;

namespace SR4BlackDev.UISystem
{
    public class ToastManager : MonoBehaviour
    {
        [SerializeField] RectTransform _layerOverlay;
        private int _currentIndex;
        private const int ToastMessagePool = 8;
        private readonly List<ToastItem> _activeToast = new List<ToastItem>();
        public GameObject prefab;
        private void Start()
        {
            if (_activeToast.Count == 0)
            {
                Init();
            }
        }

        void Init()
        {
            if (prefab == null)
            {
                Debug.LogError("Missing Prefab Toast, Can't Show Toast -> Don't worry , game still running , just return");
                return;
            }
            for (int i = 0; i < ToastMessagePool; i++)
            {
                GameObject temp = Instantiate(prefab, _layerOverlay);
                temp.SetActive(true);
                _activeToast.Add(temp.GetComponent<ToastItem>());
            }
        }

        public void ShowToast(string message)
        {
            if (_activeToast.Count == 0)
            {
                Init();
            }
            //After Init
            if (_activeToast.Count == 0)
            {
                return;
            }
            
            var toastPrevius = _activeToast[_currentIndex];
            toastPrevius.Hide();
            _currentIndex++;
            if (_currentIndex >= _activeToast.Count)
                _currentIndex = 0;
            
            var toast = _activeToast[_currentIndex];
            toast.Show(message);

        }

    }
}