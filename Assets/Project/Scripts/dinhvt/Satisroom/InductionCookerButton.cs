using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class InductionCookerButton : MonoBehaviour, ISelectableObject
    {
        [SerializeField] Animator cookerAnimator;

        private bool isSelected;

        public void Select(Vector3 touchPosition)
        {
            if (!isSelected)
            {
                isSelected = true;
                cookerAnimator.SetTrigger("TurnOff");
                this.PostEvent(EventID.OnStoveTurnedOff);
            }
        }

        public void Deselect(Vector3 touchPosition)
        {
        }
    }
}
