using DG.Tweening;
using UnityEngine;

namespace VuTienDat
{
    public class OpenCloseCabin : MonoBehaviour
    {
        [SerializeField] public bool isOn = true;
        public float moveY;
        public Vector3 rotation;
        private Vector3 lastPos;
        public static OpenCloseCabin instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            lastPos = transform.position;
        }
        public void OnOff()
        {
            if (!isOn)
            {
                isOn = true;
                this.gameObject.transform.DOMoveY(moveY, 0);
                this.gameObject.transform.DORotate(rotation, 0);
            }
            else
            {
                isOn = false;
                this.gameObject.transform.DOMoveY(lastPos.y, 0);
                this.gameObject.transform.DORotate(Vector3.zero, 0);
            }
        }
    }
}
