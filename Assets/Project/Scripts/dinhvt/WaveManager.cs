using SR4BlackDev;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class WaveManager : MonoBehaviour
    {
        //public bool isPopup = true;

        private bool _isComplete;
        protected int _waveIndex;

        [Header("Wave")]
        public int numOfInitialWave = 1;
        public List<Wave> waves = new List<Wave>();
        public float waveDuration;

        protected virtual void Start()
        {
            Init();

            //if (isPopup) PopupManager.Open(PopupPath.POPUP_LEVEL_3, LayerPopup.Main);

            //if (waves.Count > 0) waves[0].StartWave(waves[0].startWaveTime);
            for (int i = 0; i < numOfInitialWave; i++) {
                waves[i].StartWave(waves[i].startWaveTime);
            }
        }

        public virtual void Init()
        {
            for (int i = 0; i < waves.Count; i++)
            {
                waves[i].Init(this);
            }
        }

        public virtual void NextWave()
        {
            _waveIndex++;
            if (_waveIndex <= waves.Count - 1)
            {
                StartCoroutine(TransitionToNextWave(waves[_waveIndex - 1], waves[_waveIndex]));
            }
            else
            {
                StartCoroutine(EndWaveAndValidate(_waveIndex - 1));
                //ValidateSuccess();
            }
        }

        public virtual void NextWave(Wave currentWave, Wave nextWave)
        {
            ValidateSuccess();

            if (!_isComplete)
            {
                StartCoroutine(TransitionToNextWave(currentWave, nextWave));
                _waveIndex = waves.IndexOf(nextWave);
            }
        }

        public virtual void ValidateSuccess()
        {
            foreach (Wave wave in waves)
            {
                if (!wave.GetWaveComplete())
                {
                    return;
                }
            }

            _isComplete = true;
            Debug.Log("WIN");
            this.PostEvent(EventID.Log, "WIN");

            this.PostEvent(EventID.OnToggleDragAbility, false);
            PopupManager.ShowToast("WIN");
        }

        IEnumerator TransitionToNextWave(Wave currentWave, Wave nextWave)
        {
            currentWave.EndWave(currentWave.endWaveTime);

            yield return new WaitForSeconds(currentWave.transitionTime);

            nextWave.StartWave(nextWave.startWaveTime);
        }

        IEnumerator EndWaveAndValidate(int waveIndex)
        {
            Wave currentWave = waves[waveIndex];
            float transitionTime = currentWave.transitionTime;
            currentWave.EndWave(transitionTime);

            yield return new WaitForSeconds(transitionTime);

            ValidateSuccess();
        }

        public virtual bool GetIsComplete()
        {
            return _isComplete;
        }
    }
}

