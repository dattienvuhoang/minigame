using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class EyesController : MonoBehaviour
    {
        [SerializeField] GameObject visual;

        private void OnEnable()
        {
            this.RegisterListener(EventID.EyeStateChanged, ChangeEyeState);
        }

        private void OnDisable()
        {
            this.RemoveListener(EventID.EyeStateChanged, ChangeEyeState);
        }

        public void ChangeEyeState(object sender, object isOpen)
        {
            visual.SetActive((bool) isOpen);
        }
    }
}
