using UnityEngine;

namespace SR4BlackDev.Data
{
    public partial class ScriptableObjectData
    {
        public static T Load<T>(string path) where T : ScriptableObject
        {
            return Resources.Load<T>(path);
        }
        
        private const string FOLDER = "ScriptableObjectData/";
    }

}

