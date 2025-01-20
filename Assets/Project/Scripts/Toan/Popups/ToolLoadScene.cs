using System;
using System.Collections;
using System.Collections.Generic;
using SR4BlackDev.Data;
using SR4BlackDev.UI;
using SR4BlackDev.UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SR4BlackDev
{
    public class ToolLoadScene : MonoBehaviour
    {
        public ButtonCustom NextMap;
        public ButtonCustom PreviousMap;

        public int currentMap;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            NextMap.onClick.AddListener(Next);
            PreviousMap.onClick.AddListener(Previous);
        }

        void Next()
        {
            if (currentMap < 0)
            {
                currentMap = 0;
                LoadSceneByIndex(currentMap);
                return;
            }

            currentMap += 1;
            LoadSceneByIndex(currentMap);
        }

        void Previous()
        {
            if (currentMap < 0)
            {
                currentMap = 0;
                LoadSceneByIndex(currentMap);
                return;
            }

            currentMap -= 1;
            LoadSceneByIndex(currentMap);
        }


        void LoadSceneByIndex(int index)
        {
            int length = ScriptableObjectData.GameSceneCollection.GameSceneConfig.Length;
            if (index >= 0 && index < length)
            {
                string nameScene = ScriptableObjectData.GameSceneCollection.GameSceneConfig[index].NameScene;
                SceneManager.LoadScene(nameScene);
            }
            else
            {
                PopupManager.ShowToast("Fail Load");
            }

        }
        
    }
}
