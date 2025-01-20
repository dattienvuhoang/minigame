using Destructible2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuTienDat_Game2;

namespace VuTienDat
{
    public class Wave_2_RebuildD2D : MonoBehaviour
    {
        [SerializeField] private List<D2dDestructibleSprite> listD2D;
        [SerializeField] private List<Cleanner> listTool;
        public int indexMisson = 0;
        public int indexWave = 0;
        private bool isNext = false;
        public GameObject lid;
        public Vector3 lastPosLid;
        public bool isNextWave = false;

        public static Wave_2_RebuildD2D Instance;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            lastPosLid = lid.transform.position;
        }
        private void Update()
        {
            if (indexWave == 2)
            {
                if (indexMisson == 0) 
                {
                    CheckAndIncreaseMission(0, 3);
                }
                else if (indexMisson == 1)
                {
                    CheckAndIncreaseMission(3, 7);
                }
                else if (indexMisson == 2)
                {
                    CheckAndIncreaseMission(7, 9);
                }
                if (indexMisson == 3 && lid.transform.position == lastPosLid)
                {
                    isNextWave = true;
                    indexWave++;
                }
                ChangeMisson();
            }
        }
        public void RebuildD2D()
        {
            indexWave = DragController_Level_34.instance.indexWave;

            for (int i = 0; i < listD2D.Count; i++)
            {
                listD2D[i].Rebuild();
              
            }

            StartCoroutine(DelayEnable());
        }
        private void CheckAndIncreaseMission(int startIndex, int endIndex)
        {
            bool isNext = false;

            for (int i = startIndex; i < endIndex; i++)
            {
                if (listD2D[i].AlphaRatio < 0.001f)
                {
                    isNext = true;
                }
                else
                {
                    isNext = false;
                    break;
                }
            }

            if (isNext)
            {
                indexMisson++;
            }
        }

        public void ChangeMisson()
        {
            switch (indexMisson)
            {
                case 0:
                    {
                        for (int  i = 0; i < 3;i++)
                        {
                            listD2D[i].enabled = true;
                        }
                        listTool[0].enabled = true;

                        break;
                    }
                case 1:
                    {
                        for (int i = 3; i < 7; i++)
                        {
                            listD2D[i].enabled = true;
                        }
                        listTool[0].enabled = false;
                        listTool[1].enabled = true;

                        break;
                    }
                case 2:
                    {
                        for (int i = 7; i < 9; i++)
                        {
                            listD2D[i].enabled = true;
                        }
                        listTool[0].enabled = true;
                        listTool[1].enabled = false;
                        break;
                    }
            }
        }
       IEnumerator DelayEnable()
        { 
            yield return new WaitForEndOfFrame();
            {
                for (int i = 0; i < listD2D.Count; i++)
                {
                    listD2D[i].enabled = false;
                }
            }
        }
    }
}
