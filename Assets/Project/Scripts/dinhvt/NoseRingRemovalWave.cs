using DG.Tweening;
using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{
    public class NoseRingRemovalWave : Wave
    {
        [Header("End Wave")]
        [SerializeField] Transform jewelryBox;
        [SerializeField] Sprite closedBox;
        [SerializeField] Vector2 screenOffPos;
        [SerializeField] float moveTime;
        public override void EndWave(float duration)
        {   
            base.EndWave(duration);

            this.PostEvent(EventID.OnMissionResult, true);

            jewelryBox.GetComponent<SpriteRenderer>().sprite = closedBox;
            jewelryBox.GetComponent<Collider2D>().enabled = false;  
            jewelryBox.DOMove(screenOffPos, moveTime).OnComplete(() =>
            {
                jewelryBox.gameObject.SetActive(false);
            });
        }
    }
}

