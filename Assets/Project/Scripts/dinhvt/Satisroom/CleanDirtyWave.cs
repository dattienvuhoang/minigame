using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class CleanDirtyWave : ArrangeItemWave
    {
        public override void NextMission()
        {
            if (_missionIndex == 2)
            {
                for (int i = 0; i < 6; i++)
                {
                    missions[_missionIndex + i].SetCanMissionComplete(true);
                    ToggleCollider(missions[_missionIndex + i], true);
                }
            }
            else
            {   
                missions[_missionIndex].SetCanMissionComplete(true);
                ToggleCollider(missions[_missionIndex], true);
            }
        }
    }
}
