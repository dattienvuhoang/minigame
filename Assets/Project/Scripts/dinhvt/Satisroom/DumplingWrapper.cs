using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class DumplingWrapper : ToolController
    {
        [Header("Dumpling Wrapper")]
        [SerializeField] GameObject dumpling;


        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            if (canComplete && transform.childCount == 1)
            {   
                isComplete = true;
                CheckComplete();
                dumpling.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
