using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Trung
{
    public class Level14Controller : MonoBehaviour
    {
        [SerializeField] private List<TruePos> airPods;
        [SerializeField] private List<BoxCollider2D> airPodObjects;
        [SerializeField] private SpriteRenderer airpodCaseTop;
        [SerializeField] private Sprite closeAirpodCase;
        [SerializeField] private ArrangeObject pad;
        [SerializeField] private ArrangeObject gold;
        [SerializeField] private TruePos goldPos;
        public static Level14Controller instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }
        private void Start()
        {
            Application.targetFrameRate = 60;
            PopupManager.Open(PopupPath.MainPopUpTrung, LayerPopup.Main);
        }

        private void Update()
        {
            if (AllArangeObjects.instance.isWon)
            {
                AllArangeObjects.instance.ShowWinToast();
            }
        }
        private bool CheckAirPod()
        {
            foreach(var airPod in airPods)
            {
                if (!airPod.isHavingObject) return false;
            }
            return true;
        }

        public void CheckGold()
        {
            if (gold.isOnTruePos)
            {
                gold.transform.position += new Vector3(0, 0, 0.5f);
                gold.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                if (pad.isOnTruePos)
                {
                    pad.transform.position += new Vector3(0, 0, 0.2f);
                    pad.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
        public void CheckPadOut()
        {
            if (!pad.isOnTruePos && gold.truePos.Count == 0)
            {
                gold.SetTruePos(goldPos);
            }
            else if (pad.isOnTruePos)
            {
                gold.RemoveTruePos();
            }
        }
        public void CloseAirPod()
        {
            if (CheckAirPod())
            {
                airpodCaseTop.sprite = closeAirpodCase;
                airpodCaseTop.sortingOrder = 2;
                foreach(var airPod in airPodObjects)
                {
                    airPod.enabled = false;
                }
            }
        }

    }
}
