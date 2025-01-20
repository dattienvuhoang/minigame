using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class FacialSprayer : MonoBehaviour
    {
        private void OnEnable()
        {
            this.PostEvent(EventID.EyeStateChanged, false);
        }

        private void OnDisable()
        {
            this.PostEvent(EventID.EyeStateChanged, true);
        }
    }
}
