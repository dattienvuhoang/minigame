using DG.Tweening;
using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class Otoscope : Mission, ISelectableObject
    {
        private BoxCollider2D _boxCollider;
        private bool isInTargetCollider;

        [SerializeField] Vector2 onScreenPos;
        [SerializeField] float moveTime;

        [SerializeField] LayerMask targetSlot;
        [SerializeField] Transform ear;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform == ear)
            {
                isInTargetCollider = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform != ear) return;
            if (wave.missions.IndexOf(this) == wave.GetMissionIndex())
            {
                isInTargetCollider = true;
            }
            else
            {
                this.PostEvent(EventID.OnMissionResult, false);
            }
        }
        public override void Deselect(Vector3 touchPosition)
        {
            if (isInTargetCollider)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, targetSlot);

                if (hitInfo)
                {
                    if (hitInfo.transform != null)
                    {
                        Transform hitTransform = hitInfo.transform;
                        if (hitTransform == ear)
                        {
                            transform.DOMove(ear.position, moveTime).OnComplete(() =>
                            {
                                this.PostEvent(EventID.OnMissionResult, true);
                                CompleteMission();

                            });
                            _boxCollider.enabled = false;
                        }
                    }
                }
            }
            else
            {
                transform.DOMove(onScreenPos, moveTime).SetId(transform.name);
            }
        }

        public override void StartMission(float duration)
        {
            transform.DOMove(onScreenPos, duration);
        }

        public override void Select(Vector3 touchPosition)
        {
            DOTween.Kill(transform.name);
        }
    }
}
