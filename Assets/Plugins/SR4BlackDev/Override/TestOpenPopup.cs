using UnityEditor;
using UnityEngine;

namespace SR4BlackDev.UISystem
{
    

public class TestOpenPopup : MonoBehaviour
{
    public string popupName;
    public LayerPopup layer;
}
#if UNITY_EDITOR
[CustomEditor(typeof(TestOpenPopup))]
public class CustomTestOpenPopup : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //TestOpenPopup yourClass = (TestOpenPopup)target;
        if (GUILayout.Button("Show Toast"))
        { 
            PopupManager.ShowToast("Debug Toast");
        }
        if (GUILayout.Button("Open Demo Popup"))
        { 
            PopupManager.Open("DemoPopup", LayerPopup.Sub1);
        }
        if (GUILayout.Button("Close Demo Popup"))
        { 
            PopupManager.Close( LayerPopup.Sub1);
        }
        if (GUILayout.Button("Open"))
        { 
            TestOpenPopup popup = (TestOpenPopup)target;
            PopupManager.Open(popup.popupName, popup.layer);
        }
        if (GUILayout.Button("Close"))
        { 
            TestOpenPopup popup = (TestOpenPopup)target;
            PopupManager.Close( popup.layer);
        }
    }
}
#endif
}
