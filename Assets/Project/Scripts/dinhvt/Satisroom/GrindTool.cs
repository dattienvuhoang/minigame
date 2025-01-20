using DG.Tweening;
using SR4BlackDev;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class GrindTool : CleaningTool
    {
        [Header("Target Settings")]
        [SerializeField] List<ClustersOfClumps> clusters = new List<ClustersOfClumps>();

        private bool _isInTargetCollider;
        private Transform _targetTrans;
        private ClustersOfClumps _clusterOfClumps;

        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (CompareTagValue(collision, "Clusters Of Clumps") && canComplete)
            {
                _clusterOfClumps = collision.GetComponent<ClustersOfClumps>();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (CompareTagValue(collision, "Clusters Of Clumps") && canComplete)
            {
                _clusterOfClumps = null;
            }
        }

        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition + offset;

            _clusterOfClumps?.GroundIntoPowder();
        }

        public override void Deselect(Vector3 touchPosition)
        {
            if (canComplete) isComplete = IsGroundIntoComplete();

            base.Deselect(touchPosition);
        }

        private bool IsGroundIntoComplete()
        {
            foreach (ClustersOfClumps cluster in clusters)
            {
                if (!cluster.IsGroundIntoComplete()) return false;
            }

            return true;
        }
    }
}
