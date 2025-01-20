
using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using UnityEngine;

namespace dinhvt
{
    public class ShapeDoughWave : Wave
    {
        [Header("Start Wave")]
        [SerializeField] Transform missionsOfWave;
        [SerializeField] Vector3 onScreenPos;
        [SerializeField] float time;

        [Space(20)]
        [SerializeField] GameObject flour;

        public override void StartWave(float duration)
        {
            base.StartWave(duration);

            flour.SetActive(true);
            
            missionsOfWave.gameObject.SetActive(true);
            missionsOfWave.DOMove(onScreenPos, time).OnComplete(() =>
            {
                flour.transform.SetParent(missionsOfWave);
            });

            if (missions.Count > 0) missions[0].SetCanMissionComplete(true);
        }

        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            StartCoroutine(DisableWave(duration));
        }

        IEnumerator DisableWave(float duration)
        {
            PopupManager.Open(PopupPath.POPUP_PANEL, LayerPopup.Overlay);
            yield return new WaitForSeconds(duration);
            missionsOfWave.gameObject.SetActive(false);
        }

        public override void NextMission()
        {
            if (_missionIndex == 3 || _missionIndex == 8)
            {
                for(int i = 0; i < 5; i++)
                {
                    missions[_missionIndex + i].SetCanMissionComplete(true);
                }
            }
            else if (_missionIndex == 13)
            {
                for (int i = 0; i < 6; i++)
                {
                    missions[_missionIndex + i].SetCanMissionComplete(true);
                }
            }
            else
            {
                missions[_missionIndex].SetCanMissionComplete(true);
            }
        }
    }
}
