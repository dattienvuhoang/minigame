using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class Bandage : DropTool
    {
        [Header("Bandage")]
        [SerializeField] Transform bandages;
        [SerializeField] Vector2 bandagesPosition;

        public override void StartMission(float duration)
        {
            base.StartMission(duration);

            bandages.gameObject.SetActive(true);
            bandages.DOMove(bandagesPosition, moveTime);
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            transform.DOScale(selectScale, moveTime);
        }


        protected override void ResetToInitialState()
        {
            base.ResetToInitialState();

            transform.DOScale(_initialScale, moveTime);
        }
    } 
}
