using Destructible2D;
using DG.Tweening;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class GameManager_StainedGlass : MonoBehaviour
    {

        [SerializeField] private float time;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip musicClip;

        [Header("Manager")]
        public int indexWave = 1;

        [Header("Tool")]
        [SerializeField] private List<GameObject> listTool;

        [Header("Wave 1")]
        [SerializeField] private GameObject wave_1;
        [SerializeField] private List<GameObject> listGlassWave1;

        [Header("Wave 2")]
        [SerializeField] private GameObject wave_2;
        [SerializeField] private List<GameObject> listGlassWave2;

        [Header("Wave 3")]
        [SerializeField] private GameObject wave_3;
        [SerializeField] private List<GameObject> listGlassWave3;

        [Header("Wave 4")]
        [SerializeField] private GameObject wave_4;
        [SerializeField] private List<GameObject> listGlassWave4;

        [Header("Wave 5")]
        [SerializeField] private GameObject wave_5;
        [SerializeField] private List<GameObject> listGlassWave5;

        [Header("Wave_6")]
        [SerializeField] private GameObject wave_6;
        [SerializeField] private List<D2dDestructibleSprite> listD2D_Wave6;

        [Header("Wave_7")]
        [SerializeField] private int indexMisson = 1;
        [SerializeField] private GameObject wave_7;
        [SerializeField] private List<GameObject> listFail;
        [SerializeField] private List<D2dDestructibleSprite> listD2D_Wave7;
        [SerializeField] private GameObject frame_1, frame_2, line, fail;

        private bool isGamePause = false;

        public static GameManager_StainedGlass instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            PopupManager.Open(PopupPath.POPUPUI_StainedGlass, LayerPopup.Main);
            UIController_StainedGlass.instance.InitTime();
        }
        private void Update()
        {
            if (listGlassWave1.Count == 0 && indexWave == 1)
            {
                indexWave++;
                DragController_Stained_Glass.instance.ShowDone();
                wave_1.transform.DOMoveX(-10, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    wave_2.transform.DOMoveX(0, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                    {
                        wave_1.SetActive(false);
                        for (int i = 0; i < listGlassWave2.Count; i++)
                        {
                            listGlassWave2[i].GetComponent<Cut_Glass>().enabled = true;
                        }
                    });
                });
            }
            if (listGlassWave2.Count == 0 && indexWave == 2)
            {
                indexWave++;
                DragController_Stained_Glass.instance.ShowDone();
                wave_2.transform.DOMoveX(10, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    wave_3.transform.DOMoveX(0, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                    {
                        wave_2.SetActive(false);
                        for (int i = 0; i < listGlassWave3.Count; i++)
                        {
                            listGlassWave3[i].GetComponent<Cut_Glass>().enabled = true;
                        }
                    });
                });
            }
            if (listGlassWave3.Count == 0 && indexWave == 3)
            {
                indexWave++;
                DragController_Stained_Glass.instance.ShowDone();
                wave_3.transform.DOMoveX(-10, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    wave_4.transform.DOMoveX(0, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                    {
                        wave_3.SetActive(false);
                        for (int i = 0; i < listGlassWave4.Count; i++)
                        {
                            listGlassWave4[i].GetComponent<Cut_Glass>().enabled = true;
                        }
                    });
                });
            }
            if (listGlassWave4.Count == 0 && indexWave == 4)
            {
                indexWave++;
                DragController_Stained_Glass.instance.ShowDone();
                wave_4.transform.DOMoveX(10, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    wave_5.transform.DOMoveX(0, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                    {
                        wave_4.SetActive(false);
                        for (int i = 0; i < listGlassWave5.Count; i++)
                        {
                            listGlassWave5[i].GetComponent<Cut_Glass>().enabled = true;
                        }
                    });
                });
            }
            if (listGlassWave5.Count == 0 && indexWave == 5)
            {
                indexWave++;
                DragController_Stained_Glass.instance.ShowDone();
                wave_5.transform.DOMoveY(10, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    wave_6.transform.DOMoveY(0, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                    {
                        wave_5.SetActive(false);
                    });
                });
            }
            RemoveD2DWave6();
            
            if (listFail.Count == 0 && indexMisson == 1)
            {
                indexMisson++;
                DragController_Stained_Glass.instance.ShowDone();
                listTool[4].transform.DOMoveY(-2f, 0.2f);
                listTool[5].transform.DOMoveY(-2f, 0.2f);
            }
            RemoveD2DWave7();

        }
        public float getTime()
        {
            return time;
        }
        public bool IsGamePause()
        {
            return isGamePause;
        }
        public void setIsGamePause(bool isPause)
        {
            this.isGamePause = isPause;
        }
        public void RemoveGlass(GameObject glass)
        {
            switch (indexWave)
            {
                case 1:
                    {
                        listGlassWave1.Remove(glass);
                        break;
                    }
                case 2:
                    {
                        listGlassWave2.Remove(glass);
                        break;
                    }
                case 3:
                    {
                        listGlassWave3.Remove(glass);
                        break;
                    }
                case 4:
                    {
                        listGlassWave4.Remove(glass);
                        break;
                    }
                case 5:
                    {
                        listGlassWave5.Remove(glass);
                        break;
                    }
            }
        }
        public void RemoveD2DWave6()
        {
            for (int i = 0; i < listD2D_Wave6.Count; i++)
            {
                if (listD2D_Wave6[i].AlphaRatio < 0.3f)
                {
                    listD2D_Wave6[i].gameObject.SetActive(false);
                    listD2D_Wave6.Remove(listD2D_Wave6[i]);
                   
                }
            }
        }
        public void RemoveFail(GameObject fail)
        {
            listFail.Remove(fail);
        }
        public void RemoveD2DWave7()
        {
            for (int i = 0;i<listD2D_Wave7.Count;i++)
            {
                if (listD2D_Wave7[i].AlphaRatio < 0.3f)
                {
                    listD2D_Wave7[i].gameObject.SetActive(false);
                    listD2D_Wave7.Remove(listD2D_Wave7[i]);
                }
            }
        }
        public void CheckWave_6()
        {
            if (listD2D_Wave6.Count == 0 && indexWave == 6)
            {
                indexWave++;
                wave_6.transform.DOMoveY(-10, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    listTool[0].transform.DOMoveX(-10, 0.2f);
                    listTool[1].transform.DOMoveX(-10, 0.2f);
                    listTool[2].transform.DOMoveX(10, 0.2f);
                    listTool[3].transform.DOMoveX(10, 0.2f);
                    wave_7.transform.DOMoveY(0, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                    {
                        wave_6.SetActive(false);
                    });
                });
            }
        }
        public void EndGame()
        {
            if (listD2D_Wave7.Count == 0 && indexMisson == 2)
            {
                listTool[4].transform.DOMoveY(-10f, 0.2f);
                listTool[5].transform.DOMoveY(-10f, 0.2f);
                line.transform.DOMoveY(0, 0.25f);
                fail.transform.DOMoveY(-1.57f, 0.25f);
                frame_2.transform.DOMoveX(0, 0.3f).SetDelay(0.3f) .OnComplete(() =>
                {
                    frame_1.transform.DOMoveX(0, 0.3f);
                });
            }
        }
    }
}
