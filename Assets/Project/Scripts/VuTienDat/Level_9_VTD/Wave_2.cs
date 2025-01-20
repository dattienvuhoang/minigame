using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Wave_2 : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> listWave_2;

        public static Wave_2 instance;
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
            GameObject plate = transform.GetChild(0).gameObject;
            GameObject food = transform.GetChild(1).gameObject;
            for (int i = 0; i < plate.transform.childCount; i++)
            {
                listWave_2.Add(plate.transform.GetChild(i).transform.GetChild(0).GetComponent<SpriteRenderer>());
            }
            for (int i = 0; i < food.transform.childCount; i++)
            {
                listWave_2.Add(food.transform.GetChild(i).transform.GetChild(0).GetComponent<SpriteRenderer>());
            }
        }
        public void FadeWave()
        {
            for (int i = 0; i < listWave_2.Count; i++)
            {
                listWave_2[i].DOFade(1, 1f);
            }
        }
    }
}
