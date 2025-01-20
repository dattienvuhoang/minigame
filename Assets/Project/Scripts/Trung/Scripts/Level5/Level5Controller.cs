using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Level5Controller : MonoBehaviour
    {
        public int status { get; private set; }
        [SerializeField] List<TakenOutObject> status0obj;
        [SerializeField] private GameObject status0Stove;
        [SerializeField] private GameObject wasingLiquid2;
        [SerializeField] private GameObject wasingLiquid3;
        [SerializeField] private GameObject status2Pan;
        [SerializeField] private GameObject status3Pan;
        [SerializeField] private GameObject status4Obj;
        [SerializeField] private GameObject billet4;
        [SerializeField] private GameObject status5Obj;
        [SerializeField] private GameObject billet5;
        [SerializeField] private List<GameObject> cleanObjects;
        [SerializeField] private List<Transform> truePos;
        [SerializeField] private GameObject task6;

        private bool canStatus2PanGo = false;
        private bool canStatus3PanGo = false;
        private bool canStatus4Go = false;
        private bool canStatus5Go = false;
        private bool stoveCanGo = false;
        private bool canStatus6Go = false;

        public static Level5Controller instance;
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
            status = 0;
            wasingLiquid2.SetActive(false);
            status2Pan.SetActive(false);
            status3Pan.SetActive(false);
            status4Obj.SetActive(false);    
            task6.SetActive(false);

            Application.targetFrameRate = 60;
            PopupManager.Open(PopupPath.MainPopUpTrung, LayerPopup.Main);
        }

        private void Update()
        {
            if(status == 0)
            {
                CheckCollider0();
            }
            else if(status == 1)
            {
                StoveGoUp();
            }
            else if(status == 2)
            {
                status2Pan.SetActive(true);
                if (canStatus2PanGo)
                {
                    Status2PanDown();
                }
            }
            else if(status == 3)
            {
                status3Pan.SetActive(true);
                if (canStatus3PanGo)
                {
                    Status3PanDown();
                }
            }
            else if(status == 4)
            {
                status4Obj.SetActive(true);
                if (canStatus4Go)
                {
                    Status4ObjectDown();
                }
            }
            else if(status == 5)
            {
                status5Obj.SetActive(true);
                if (canStatus5Go)
                {
                    Status5ObjectDown();
                }
            }
            else if (status == 6)
            {
                task6.SetActive(true);
                if (canStatus6Go)
                {
                    Status6ObjectDown();
                }
                if (cleanObjects[0].GetComponent<ArrangeObject>().isOnTruePos)
                {
                    truePos[1].gameObject.SetActive(true);
                }
                if (cleanObjects[1].GetComponent<ArrangeObject>().isOnTruePos)
                {
                    truePos[2].gameObject.SetActive(true);
                }
                if (cleanObjects[2].GetComponent<ArrangeObject>().isOnTruePos)
                {
                    truePos[3].gameObject.SetActive(true);
                }
                if (cleanObjects[3].GetComponent<ArrangeObject>().isOnTruePos)
                {
                    if (AllArangeObjects.instance.isWon)
                    {
                        AllArangeObjects.instance.ShowWinToast();
                    }
                }
            }
        }
        private void CheckCollider0()
        {
            if (status0obj[0].isOut)
            {
                status0obj[1].SetCollider(true);
            }
        }
        public bool CheckStatus0()
        {
            foreach(TakenOutObject obj in status0obj)
            {
                if (!obj.isOut)
                {
                    return false;
                }
            }
            stoveCanGo = true;
            GoNextStatus();
            return true;
        }
        public void GoNextStatus()
        {
            status++;
        }

        public void SetStatus3()
        {
            if ((status == 3))
            {
                canStatus3PanGo = true;
            }
            else if ((status == 4))
            {
                canStatus4Go = true;
            }
            else if (status == 5)
            {
                canStatus5Go = true;
            }
        }
        private void Status2PanDown()
        {
            status2Pan.transform.position = Vector3.MoveTowards(status2Pan.transform.position, status2Pan.transform.position - new Vector3(0, 10, 0), 15 * Time.deltaTime);
            if(status2Pan.transform.position.y <= 0)
            {
                canStatus2PanGo = false;
                wasingLiquid2.SetActive(true);
            }
        }
        private void Status3PanDown()
        {
            status3Pan.transform.position = Vector3.MoveTowards(status3Pan.transform.position, status3Pan.transform.position - new Vector3(0, 10, 0), 15 * Time.deltaTime);
            if (status3Pan.transform.position.y <= 0)
            {
                canStatus3PanGo = false;
                wasingLiquid3.SetActive(true);
            }
        }
        private void Status4ObjectDown()
        {
            status4Obj.transform.position = Vector3.MoveTowards(status4Obj.transform.position, status4Obj.transform.position - new Vector3(0, 10, 0), 15 * Time.deltaTime);
            if (status4Obj.transform.position.y <= 0)
            {
                billet4.SetActive(true);
                canStatus4Go = false;
            }
        }
        private void Status5ObjectDown()
        {
            status5Obj.transform.position = Vector3.MoveTowards(status5Obj.transform.position, status5Obj.transform.position - new Vector3(0, 10, 0), 15 * Time.deltaTime);
            if (status5Obj.transform.position.y <= 0)
            {
                billet5.SetActive(true);
                canStatus5Go = false;
            }
        }
        private void Status6ObjectDown()
        {
            task6.transform.position = Vector3.MoveTowards(task6.transform.position, task6.transform.position - new Vector3(0, 10, 0), 15 * Time.deltaTime);
            if (task6.transform.position.y <= 0)
            {
                canStatus6Go = false;
            }
        }

        public void SetStatus6()
        {
            canStatus6Go = true;
        }

        private void StoveGoUp()
        {
            if (stoveCanGo)
            {
                Debug.Log("go");
                status0Stove.transform.position = Vector3.MoveTowards(status0Stove.transform.position, 
                    status0Stove.transform.position + new Vector3(0, 10, 0), 20f * Time.deltaTime);
                if(status0Stove.transform.position.y > 10)
                {
                    stoveCanGo = false;
                    status0Stove.SetActive(false);
                    status++;
                    canStatus2PanGo = true;
                }
            }
        }
    }
}
