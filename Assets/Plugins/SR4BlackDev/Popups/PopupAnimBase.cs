using UnityEngine;
using System.Collections;

namespace SR4BlackDev.UISystem
{
    public class PopupAnimBase : MonoBehaviour
    {
        public virtual IEnumerator Open()
        {
            yield return null;
        }

        public virtual IEnumerator Close()
        {
            yield return null;
        }
    }
}