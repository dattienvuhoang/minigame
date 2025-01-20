using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SR4BlackDev.Editor
{
    [CustomEditor(typeof(ImportSceneToData))]
    public class ImportSceneToDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("ReImport"))
            {
                ImportSceneToData obj = target as ImportSceneToData;
                obj.ReImport();
            }
        }
    }    
}
