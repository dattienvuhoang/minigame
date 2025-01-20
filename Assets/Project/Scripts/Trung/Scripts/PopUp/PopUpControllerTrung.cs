using SR4BlackDev.UI;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trung
{
    public class PopUpControllerTrung : PopupBase
    {
        protected override void OnOpenStart()
        {
            base.OnOpenStart();
        }
        protected override void OnOpenFinish()
        {
            base.OnOpenFinish();
            Level1();
        }
        private void Level1()
        {
            //Washing Leg level
            Debug.Log(SceneManager.GetActiveScene().name);
            if (SceneManager.GetActiveScene().name == "Level1") 
            {
                LayerController2D.instance.SetUseObjectOnPos();
            }
        }
    }
}
