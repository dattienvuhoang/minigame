using SR4BlackDev;
using SR4BlackDev.UISystem;
using System.Collections;
using UnityEngine;

namespace dinhvt
{
    public class SteamDumplingsWave : Wave
    {
        [Header("Start Wave")]
        [SerializeField] GameObject missionsOfWave;
        [SerializeField] Sprite background;

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            if (missions.Count > 0) missions[0].SetCanMissionComplete(true);
            this.PostEvent(EventID.UpdateBackground, background);
            missionsOfWave.gameObject.SetActive(true);
        }
    }
}
