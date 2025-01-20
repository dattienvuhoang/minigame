using UnityEngine;

namespace dinhvt
{
    public class PlateController : ItemController
    {
        [Header("Plate Settings")]
        [SerializeField] Collider2D vegetableSlotCol;

        public override void CompleteMission()
        {   
            isComplete = true;
            vegetableSlotCol.enabled = true;
            base.CompleteMission();
        }
    }
}
