using DG.Tweening;
using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class Cord : Mission, ISelectableObject
    {
        [SerializeField] Vector2 targetPos;
        public override void Select(Vector3 touchPosition)
        {
            if (canComplete)
            {
                isComplete = true;
                transform.DOMove(targetPos, 0.5f).OnComplete(() => 
                {
                    this.PostEvent(EventID.EyeStateChanged, false);
                    CompleteMission();
                });
            }
        }
        public override void Deselect(Vector3 touchPosition) { }
    }
}
