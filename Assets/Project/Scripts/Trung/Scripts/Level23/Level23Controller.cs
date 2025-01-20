using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Level23Controller : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = 60;
            PopupManager.Open(PopupPath.MainPopUpTrung, LayerPopup.Main);
        }

        // Update is called once per frame
        void Update()
        {
            if (AllArangeObjects.instance != null)
            {
                if (AllArangeObjects.instance.isWon)
                {
                    AllArangeObjects.instance.ShowWinToast();
                }
            }
        }
    }
}
