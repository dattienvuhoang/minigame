using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SR4BlackDev
{

    public class ImportSceneToData : MonoBehaviour
    {
#if UNITY_EDITOR
        public SceneAsset[] scenes;
        public GameSceneCollection collection;
        public void ReImport()
        {
            int length = scenes.Length;
            
            List<GameLevelSceneConfig> tempData = new List<GameLevelSceneConfig>();
            for (int i = 0; i < length; i++)
            {
                int level = i;
                string nameLevel = scenes[i].name;
                tempData.Add(new GameLevelSceneConfig()
                {
                    Level = level,
                    NameScene = nameLevel
                });
            }

            collection.GameSceneConfig = tempData.ToArray();
        }
#endif
    }

}
