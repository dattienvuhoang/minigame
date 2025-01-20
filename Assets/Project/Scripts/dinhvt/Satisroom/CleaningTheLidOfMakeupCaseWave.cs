using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace dinhvt
{
    public class CleaningTheLidOfMakeupCaseWave : Wave
    {
        [Header("Start Wave")]
        [SerializeField] SpriteRenderer boxSR;
        [SerializeField] Sprite clossingBoxSprite;
        [SerializeField] Sprite closedBoxSprite;
        [Space(20)]
        [SerializeField] GameObject targets;
        

        [Header("End Wave")]
        [SerializeField] Transform box;
        [SerializeField] Vector3 offScreenBoxPos;

        public override void Start()
        {
        }

        public override void StartWave(float duration)
        {   
            base.StartWave(duration);

            InitWave();
            StartCoroutine(CloseBox(duration));
        }

        public override void EndWave(float duration)
        {
            box.DOMove(offScreenBoxPos, duration).OnComplete(() =>
            {
                box.gameObject.SetActive(false);
            });
        }

        IEnumerator CloseBox(float duration)
        {
            boxSR.sprite = clossingBoxSprite;
            yield return new WaitForSeconds(duration);
            boxSR.sprite = closedBoxSprite;

            targets.SetActive(true);
        }
    }
}
