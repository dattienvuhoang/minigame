using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class CleanMetalPanWave : Wave
    {
        [Header("Start Wave")]
        [SerializeField] List<MovableObject> onScreenObject = new List<MovableObject>();
        [SerializeField] GameObject targets;
        [SerializeField] List<GameObject> stains = new List<GameObject>();

        [Header("End Wave")]
        [SerializeField] Transform fabricSheet;
        [SerializeField] Vector3 offScreenFabricPos;


        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            InitWave();
            StartCoroutine(MoveObject(duration));
        }

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            targets.SetActive(false);
            fabricSheet.DOMove(offScreenFabricPos, duration).OnComplete(() =>
            {
                fabricSheet.gameObject.SetActive(false);
            });
        }

        IEnumerator MoveObject(float duration)
        {
            foreach (MovableObject obj in onScreenObject)
            {
                obj.transform.gameObject.SetActive(true);
                obj.transform.DOMove(obj.targetPosition, duration);
            }

            yield return new WaitForSeconds(duration);

            foreach (GameObject stain in stains)
            {
                stain.SetActive(false);
            }
            targets.SetActive(true);
        }
    }
}
