using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class WavePool : MonoBehaviour
    {
        public static WavePool instance;
        private List<GameObject> wavePool = new List<GameObject>();
        private int amount = 20;

        [SerializeField] private GameObject wavePrefab;
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            for(int i = 0; i < amount; i++)
            {
                GameObject obj = Instantiate(wavePrefab);
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                wavePool.Add(obj);
            }   
        }
        
        public GameObject GetWavePool()
        {
            for (int i = 0; i < wavePool.Count; i++)
            {
                if (!wavePool[i].activeInHierarchy)
                {
                    return wavePool[i];
                }
            }
            return null;
        }
    }
}
