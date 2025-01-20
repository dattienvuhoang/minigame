using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SR4BlackDev
{
    [CreateAssetMenu(fileName = "GameSceneCollection",menuName = "GameDataConfig/GameSceneCollection")]
    public class GameSceneCollection : ScriptableObject
    {
        public GameLevelSceneConfig[] GameSceneConfig;
    }
    [System.Serializable]
    public class GameLevelSceneConfig
    {
        public int Level;
        public string NameScene;
    }
}
