using UnityEngine;

namespace dinhvt
{
    public class MoldController : ToolController
    {
        [Header("Mold Settings")]
        [SerializeField] int numOfTagets;

        private int _targetCount;

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition); 

            if (canComplete && transform.childCount == 1)
            {
                transform.GetChild(0).GetComponent<Dumpling>().Shaped();
                _targetCount++;
                if (_targetCount == numOfTagets)
                {
                    isComplete = true;
                    CheckComplete();
                }
            }
        }
    }
}
