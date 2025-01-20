using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class Level2 : WaveManager
    {
        [SerializeField] Wave chooseWave;
        private Wave prevWave;

        public override void NextWave(Wave currentWave, Wave nextWave)
        {
            base.NextWave(currentWave, nextWave);
        }

        public override void ValidateSuccess()
        {
            foreach (Wave wave in waves)
            {
                if (!wave.GetWaveComplete())
                {
                    return;
                }
            }

            Debug.Log("WIN");
        }
    }
}
