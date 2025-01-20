using Cysharp.Threading.Tasks.Triggers;
using Destructible2D;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using TopDownShooter;
using Unity.VisualScripting;
using UnityEngine;

namespace Trung
{
    public class Level2Controller : MonoBehaviour
    {
        public int status {  get; private set; }
        public int curIndex { get; private set; }
        private bool destroy;
        public int mossCleaned {  get; private set; }

        [SerializeField] private List<ClickableObject> useableObjects;
        [SerializeField] private GameObject superDirtyFloor;
        [SerializeField] private List<BoxCollider2D> lotuses;
        private Manhole manhole;
        private D2dDestructibleSprite d2dDirtyFloor;
        
        private void Awake()
        {
            d2dDirtyFloor = superDirtyFloor.GetComponent<D2dDestructibleSprite>();
            manhole = GameObject.Find("Manhole").GetComponent<Manhole>();
        }
        private void Start()
        {
            status = 0;
            curIndex = 0;
            mossCleaned = 0;
            destroy = false;
            d2dDirtyFloor.enabled = false;
            Application.targetFrameRate = 60;
            PopupManager.Open(PopupPath.MainPopUpTrung, LayerPopup.Main);
        }
        private void Update()
        {
            if (!CheckIsClicked() && curIndex != status)
            {
                destroy = true;
            }
            if(curIndex == 3)
            {
                d2dDirtyFloor.enabled = true;
            }
            if(status == 4)
            {
                TurnOnLotus();
                if (CheckStatus4())
                {
                    TurnOffLotus();
                    GoNextStatus();
                }
            }
        }

        public void TurnOffDestroy()
        {
            destroy = false;
        }
        public void CleanMoss()
        {
            if (mossCleaned < 9) 
            {
                mossCleaned++;
            }
            else
            {
                GoNextStatus();
                Debug.Log("2");

            }
        }
        public void TurnOnLotus()
        {
            foreach(var lotus in lotuses)
            {
                lotus.enabled = true;
            }
        }
        public void TurnOffLotus()
        {
            foreach (var lotus in lotuses)
            {
                lotus.enabled = false;
                lotus.gameObject.GetComponent<Lotus>().enabled = false;
            }
        }
        public void GoNextStatus()
        {
            status++;
            Debug.Log("Next");
        }

        private bool CheckIsClicked()
        {
            foreach (ClickableObject obj in useableObjects)
            {
                if (obj.isClicked)
                {
                    return true;
                }
            }
            return false;
        }

        public void GoOut()
        {
            GoOutAnim(useableObjects[curIndex].gameObject);
        }
        private void GoOutAnim(GameObject obj)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, new Vector3(100, obj.transform.position.y, obj.transform.position.z), 20f * Time.deltaTime);
        }
        public void ShowNextObject()
        {
            curIndex++;
            useableObjects[curIndex].gameObject.SetActive(true);
        }
        public void ShowBroomAndBrush()
        {
            useableObjects[2].gameObject.SetActive(true);
            curIndex++;
        }
        private bool CheckStatus4()
        {
            foreach(var lotus in lotuses)
            {
                if (!lotus.gameObject.GetComponent<Lotus>().isDone)
                {
                    return false;
                }
            }
            if (!manhole.isOnTruePos)
            {
                return false;
            }
            return true;
        }
    }
}
