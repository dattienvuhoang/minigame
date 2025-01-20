using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SR4BlackDev.UISystem
{
    public static class TextExtension
    {
        public static void SetText(this GameObject gameObject, string value)
        {
            Text text = gameObject.GetComponent<Text>();
            if (text != null)
            {
                text.text = value;
                return;
            }
        
            TextMeshProUGUI textMesh = gameObject.GetComponent<TextMeshProUGUI>();
            if (textMesh != null)
            {
                textMesh.text = value;
            }
        }
    }
}