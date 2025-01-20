using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SR4BlackDev.Data
{
    public partial class ScriptableObjectData
    {
        private const string GAME_SCENE_CONFIG = FOLDER + "GameSceneCollection";
        
        private static GameSceneCollection _gameSceneCollection;

        public static GameSceneCollection GameSceneCollection
        {
            get
            {
                if (_gameSceneCollection == null)
                {
                    _gameSceneCollection = Load<GameSceneCollection>(GAME_SCENE_CONFIG);
                    if (_gameSceneCollection == null)
                    {
                        Debug.Log("GAME_SCENE_CONFIG null");
                    }
                }

                return _gameSceneCollection;
            }
        }
    }

}
