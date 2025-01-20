using SR4BlackDev.UISystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VuTienDat_Game2
{
    public class PopupWinGame : PopupBase
    {
        [SerializeField] private Image imgLike ;

        /*protected override  Start()
        {
            imgLike.enabled = true;
        }*/
        protected override void OnOpenStart()
        {
            base.OnOpenStart(); 
            imgLike.enabled = true;
        }
    }
}
