using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuTienDat_Game2;

namespace VuTienDat
{
    public class CleanWave : MonoBehaviour
    {
        [SerializeField] private List<D2dDestructibleSprite> listD2D;
        public Cleanner clean_1, clean_2;
        public bool isDone = false;
        public int count = 0;
        public bool isShowDone = true;

        private void Start()
        {
            isShowDone = true;
            count = listD2D.Count;
            Debug.Log(listD2D.Count);
            Debug.Log(count);
        }
        private void Update()
        {
            for (int i = 0; i < listD2D.Count/2; i++)
            {
                if (listD2D[i].AlphaRatio > 0.95f)
                {
                    Debug.Log("Checkkk");
                    //listD2D[i].Clear();
                    //listD2D[i].gameObject.SetActive(false);
                    listD2D.Remove(listD2D[i]);
                }
            }
            for (int i = listD2D.Count/2; i < listD2D.Count; i++)
            {
                if (listD2D[i].AlphaRatio < 0.01f)
                {
                    Debug.Log("Checkkk2222");

                    //listD2D[i].Clear();
                    listD2D[i].gameObject.SetActive(false);
                    listD2D.Remove(listD2D[i]);
                }
            }
            if (listD2D.Count == count/2)
            {
                /* if (isShowDone)
                 {
                     clean_1.enabled = false;
                     clean_2.enabled = true;
                     DragController_Level_28.ins.ShowDone();
                     isShowDone = false;
                 }
                 else
                 {

                 }*/
                if (isShowDone)
                {
                    Debug.Log("Check Done ...........");
                    isShowDone = false;
                    DragController_Level_28.ins.ShowDone();
                }
                clean_1.enabled = false;
                clean_2.enabled = true;

            }
            if (!isDone && listD2D.Count == 0)
            {
                clean_2.enabled = false;
                isDone = true;
                isShowDone = true;
                //DragController_Level_28.ins.ShowDone();
            }

        }
    }
}
