using SR4BlackDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class MissionChecker : MonoBehaviour
    {
        private bool _isPlayResultAnim = true;
        private Mission _mission;

        private void Awake()
        {

            this.RegisterListener(EventID.ResetCheck, ResetCheck);
        }

        private void OnDestroy()
        {
            this.RemoveListener(EventID.ResetCheck, ResetCheck);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Mission>(out _mission))
            {   
                if (!_mission.GetCanMissionComplete() && _isPlayResultAnim)
                {
                    //Debug.Log("False: " + transform.name + " " + collision.name);
                    _isPlayResultAnim = false;
                    this.PostEvent(EventID.OnMissionResult, false);
                }
            }
        }

        private void ResetCheck(object sender, object param)
        {
            _isPlayResultAnim = true;
        }
    }
}
