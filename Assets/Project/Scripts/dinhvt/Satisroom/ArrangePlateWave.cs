using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class ArrangePlateWave : ArrangeItemWave
    {
        [Header("Next Mission")]
        [SerializeField] List<PlateData> plateDatas = new List<PlateData>();

        private bool _check;

        [System.Serializable]
        public struct PlateData
        {
            public List<Mission> plateMissions;
            public Collider2D nextPlateCollider;
        }
        public void EnableNextPlateMisison()
        {
            foreach (PlateData plateData in plateDatas)
            {   
                _check = true;
                foreach (Mission mission in plateData.plateMissions)
                {   
                    if (!mission.GetMissionComplete())
                    {
                        _check = false;
                        break;
                    }
                }

                if (_check)
                {
                    plateData.nextPlateCollider.enabled = true;
                    plateDatas.Remove(plateData);
                }
            }
        }

        public override void ValidateSuccess()
        {
            base .ValidateSuccess();

            EnableNextPlateMisison();
        }
    }
}
