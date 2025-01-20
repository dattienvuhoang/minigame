
using UnityEngine;

namespace dinhvt
{
    public class ChooseMouthOrNoseWave : Wave
    {       
        [Header("Start Wave")]
        [SerializeField] GameObject noseHair;
        [SerializeField] Wave noseCleaningWave;
        

        [Header("End Wave")]
        [SerializeField] GameObject face;

        [Header("Choose Wave Settings")]
        [SerializeField] float timeoutThreshold;

        private float _timer;
        private bool _isCountingTime;
        private int _blinkAnimIndex = 0;

        private void Update()
        {
            _timer = _isCountingTime ? (_timer + Time.deltaTime) : 0f;

            if (_timer > timeoutThreshold)
            {
                _timer = 0f;
                BlinkMission();
            }
        }


        public override void StartWave(float duration)
        {
            base.StartWave(duration);
            
            _isCountingTime = true;
            UpdateBlinkAnimIndex();
            
            FadeHierarchy(face, 1f, duration);

            CheckNoseCleaningWaveComplete();
            UpdateCanMissionComplete();
        }


        public override void EndWave(float duration)
        {
            base.EndWave(duration);

            _isCountingTime = false;
            FadeHierarchy(face, 0f, duration);
        }

        public override void ValidateSuccess()
        {
            _missionIndex++;
            if (_missionIndex > missions.Count - 1)
            {   
                CompleteAllMission();
            }
        }

        public override void CompleteAllMission()
        {
            CompleteWave();
        }

        private void UpdateCanMissionComplete()
        {
            foreach (Mission mission in missions)
            {
                mission.SetCanMissionComplete(!mission.GetMissionComplete());
            }
        }

        private void CheckNoseCleaningWaveComplete()
        {
            foreach (Transform child in face.transform)
            {
                if (noseCleaningWave.GetWaveComplete() && child == noseHair.transform)
                    continue;

                child.gameObject.SetActive(true);
            }
        }

        private void UpdateBlinkAnimIndex()
        {
            for (int i = 0; i < missions.Count; i++)
            {
                if (!missions[i].GetMissionComplete())
                {
                    _blinkAnimIndex = i;
                    return;
                }
            }
        }

        private void BlinkMission()
        {
            missions[_blinkAnimIndex % missions.Count].Blink();

            _blinkAnimIndex++;
            while (missions[_blinkAnimIndex % missions.Count].GetMissionComplete())
            {
                _blinkAnimIndex++;
            }
        }
    }
}
