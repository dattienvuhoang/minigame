using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat_Game3_LamBanh
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> listFish;

        public static GameManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            PopupManager.Open(PopupPath.POPUPUI_LamBanh, LayerPopup.Main);
        }
        public void RemoveFish(GameObject fish)
        {
            if (listFish.Contains(fish))
            {
                listFish.Remove(fish);
            }
        }
    }
}
