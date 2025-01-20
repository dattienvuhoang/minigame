using UnityEngine;
using UnityEngine.UI;

namespace dinhvt
{
    public class ScaleCamera : MonoBehaviour
    {
        private CanvasScaler canvasScaler;

        private void Start()
        {
            canvasScaler = GetComponent<CanvasScaler>();
            if (canvasScaler == null || Screen.width > Screen.height) return;
            Scale();
        }

        private void Scale()
        {
            Camera.main.GetComponent<Camera>().orthographicSize *= GetScale();
        }

        public float GetScale()
        {
            //For match = 1
            Vector2 referenceResolution = canvasScaler.referenceResolution;
            float scaleWidth = Screen.width / referenceResolution.x;
            float scaleHeight = Screen.height / referenceResolution.y;
            return scaleHeight / scaleWidth;
        }
    }
}
