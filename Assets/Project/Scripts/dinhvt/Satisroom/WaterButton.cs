using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class WaterButton : Mission
    {
        [SerializeField] Animator waterTank;
        [SerializeField] SpriteRenderer waterSpriteRend;
        [SerializeField] Collider2D waterCollider;
        [SerializeField] float fadeTime;

        private bool _isOpened;

        public override void StartMission(float duration)
        {
            base.StartMission(duration);

            if (!_isOpened) CompleteMission();
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            if (!_isOpened) TurnOn();
            else TurnOff();

            _isOpened = !_isOpened;
        }

        private void TurnOn()
        {
            waterTank.SetTrigger("On");
            waterCollider.enabled = true;
            waterSpriteRend.DOFade(1f, fadeTime);
            ResetMission();
        }

        private void TurnOff()
        {
            waterTank.SetTrigger("Off");
            waterCollider.enabled = false;
            waterSpriteRend.DOFade(0f, fadeTime);
            CompleteMission();
        }
    }
}
