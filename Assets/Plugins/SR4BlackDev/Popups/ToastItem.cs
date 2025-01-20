using UnityEngine;

namespace SR4BlackDev.UISystem
{
    public class ToastItem : MonoBehaviour
    {
        public virtual void Show(string message){}

        public virtual void Hide(){}
    }
}