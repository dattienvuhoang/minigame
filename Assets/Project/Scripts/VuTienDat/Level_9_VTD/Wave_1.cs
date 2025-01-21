using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Wave_1 : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> listWave_1;
        public static Wave_1 instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            SetList();
        }
        private void SetList()
        {
            GameObject tool = transform.GetChild(6).gameObject;
           
           
            for (int i = 0; i < tool.transform.childCount; i++)
            {
                listWave_1.Add(tool.transform.GetChild(i).transform.GetChild(0).GetComponent<SpriteRenderer>());
            }
        }
        public void FadeWave()
        {
            for (int i = 0; i < listWave_1.Count; i++)
            {
                listWave_1[i].DOFade(0, 2f);
            }
        }
    }
}
