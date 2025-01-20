using SR4BlackDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Racket : MonoBehaviour
    {
        [SerializeField] private GameObject waterAnim;
        private Vector3 pos;
        private float cd = 0.8f;

        private void Start()
        {
            pos = transform.position + new Vector3(0, 1.15f, 1.1f);
        }
        private void Update()
        {
            pos = transform.position + new Vector3(0, 1.15f, 1.1f);
            if (CheckPosition())
            {
                ShowWaterAnim();
            }
        }
        
        private void ShowWaterAnim()
        {
            if(cd <= 0)
            {
                GameObject wave = WavePool.instance.GetWavePool();
                if(wave != null )
                {
                    wave.transform.position = pos;
                    wave.SetActive(true);
                    Debug.Log("on");
                    cd = 0.8f;
                }
            }
            else
            {
                cd -= Time.deltaTime;
            }
        }
        private bool CheckPosition()
        {
            if (pos.x > -1.2 && pos.x < 1.4)
            {
                if (pos.y > -1.8 && pos.y < 3.2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
