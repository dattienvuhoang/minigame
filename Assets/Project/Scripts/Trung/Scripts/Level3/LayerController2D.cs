using Destructible2D;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class LayerController2D : MonoBehaviour
    {
        [SerializeField] private GameObject hair_L;
        [SerializeField] private GameObject hair_R;
        [SerializeField] private GameObject dirty_L;
        [SerializeField] private GameObject dirty_R;
        [SerializeField] private GameObject soap_L;
        [SerializeField] private GameObject soap_R;
        [SerializeField] private GameObject babyPower_L;
        [SerializeField] private GameObject babyPower_R;
        [SerializeField] private GameObject wax_L;
        [SerializeField] private GameObject wax_R;
        [SerializeField] private List<UseableOject> useableObjects;
        [SerializeField] private Transform allUseObj;
        [SerializeField] private GameObject brush;
        [SerializeField] private GameObject doneTickImage;
        [SerializeField] private TakeOffObject rightSock;

        private GameObject waxPaperL;
        private GameObject waxMaskL;
        private GameObject waxPaperR;
        private GameObject waxMaskR;
        public int paperLeftAmount { get; private set; }
        public int paperRightAmount { get; private set; }

        private D2dDestructibleSprite hairL_d2d;
        private D2dDestructibleSprite hairR_d2d;
        private D2dDestructibleSprite dirtyL_d2d;
        private D2dDestructibleSprite dirtyR_d2d;
        private D2dDestructibleSprite soapL_d2d;
        private D2dDestructibleSprite soapR_d2d;
        private D2dDestructibleSprite babyPowerL_d2d;
        private D2dDestructibleSprite babyPowerR_d2d;
        private D2dDestructibleSprite waxL_d2d;
        private D2dDestructibleSprite waxR_d2d;
        private GameObject fullLeft;

        public int currentState { get; private set; }
        private bool allTurnedOff;
        private bool useObjectOnPos;
        public string curLeg { get; private set; }
        private float doneScale;

        public static LayerController2D instance;
        private void Awake()
        {
            useableObjects = new List<UseableOject>();
            hairL_d2d = hair_L.GetComponent<D2dDestructibleSprite>();
            hairR_d2d = hair_R.GetComponent<D2dDestructibleSprite>();
            dirtyL_d2d = dirty_L.GetComponent<D2dDestructibleSprite>();
            dirtyR_d2d = dirty_R.GetComponent<D2dDestructibleSprite>();
            soapL_d2d = soap_L.GetComponent<D2dDestructibleSprite>();
            soapR_d2d = soap_R.GetComponent<D2dDestructibleSprite>();
            babyPowerL_d2d = babyPower_L.GetComponent<D2dDestructibleSprite>();
            babyPowerR_d2d = babyPower_R.GetComponent<D2dDestructibleSprite>();
            waxL_d2d = wax_L.GetComponent<D2dDestructibleSprite>();
            waxR_d2d = wax_R.GetComponent<D2dDestructibleSprite>();
            waxPaperL = GameObject.Find("WaxPaperL");
            waxMaskL = GameObject.Find("WaxL2DMasksL");
            waxPaperR = GameObject.Find("WaxPaperR");
            waxMaskR = GameObject.Find("WaxR2DMasks");
            fullLeft = GameObject.Find("LeftLeg");

            useableObjects = new List<UseableOject>();
            for (int i = 0; i < 5; i++)
            {
                useableObjects.Add(allUseObj.GetChild(i).GetComponent<UseableOject>());
            }

            if (instance != this && instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }

            Application.targetFrameRate = 60;

        }
        void Start()
        {
            waxMaskL.gameObject.SetActive(false);
            waxPaperL.gameObject.SetActive(false);
            waxPaperR.gameObject.SetActive(false);
            waxMaskR.gameObject.SetActive(false);
            paperLeftAmount = 5;
            paperRightAmount = 5;
            currentState = -1;
            doneScale = 0.25f;
            curLeg = "left";
            brush.gameObject.SetActive(false);
            TurnOffAllLayer();
            PopupManager.Open(PopupPath.MainPopUpTrung, LayerPopup.Main);
            useObjectOnPos = true;
        }
        void Update()
        {
            if (curLeg == "left")
            {
                CheckLayerLeft();
                CheckDoneLeft();
            }
            else if(curLeg == "right")
            {
                CheckLayerRight();
                CheckDoneRight();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                TurnOnSoapLeft();
            }
            UseobjectMoveOn();
        }

        public void TurnOffAllLayer()
        {
            if (!allTurnedOff)
            {
                hairL_d2d.enabled = false;
                hairR_d2d.enabled = false;
                dirtyL_d2d.enabled = false;
                dirtyR_d2d.enabled = false;
                soapL_d2d.enabled = false;
                soapR_d2d.enabled = false;
                babyPowerL_d2d.enabled = false;
                babyPowerR_d2d.enabled = false;
                waxL_d2d.enabled = false;
                waxR_d2d.enabled = false;
                allTurnedOff = true;
            }
        }

        public void TurnOnAllLayer()
        {
            hairL_d2d.enabled = true;
            hairR_d2d.enabled = true;
            dirtyL_d2d.enabled = true;
            dirtyR_d2d.enabled = true;
            soapL_d2d.enabled = true;
            soapR_d2d.enabled = true;
            babyPowerL_d2d.enabled = true;
            babyPowerR_d2d.enabled = true;
            waxL_d2d.enabled = true;
            waxR_d2d.enabled = true;
            allTurnedOff = false;
        }
        public void TurnOnDirtyLeft()
        {
            if (!dirtyL_d2d.enabled)
            {

                TurnOffAllLayer();
                dirtyL_d2d.enabled = true;
                allTurnedOff = false;
            }
        }
        public void TurnOnDirtyRight()
        {
            if (!dirtyR_d2d.enabled)
            {

                TurnOffAllLayer();
                dirtyR_d2d.enabled = true;
                allTurnedOff = false;
            }
        }
        public void TurnOnSoapLeft()
        {
            if (!soapL_d2d.enabled)
            {
                TurnOffAllLayer();
                soapL_d2d.enabled = true;
                allTurnedOff = false;
            }
        }
        public void TurnOnSoapRight()
        {
            if (!soapR_d2d.enabled)
            {
                TurnOffAllLayer();
                soapR_d2d.enabled = true;
                allTurnedOff = false;
            }
        }
        public void TurnOnHairLeft()
        {
            if (!hairL_d2d.enabled)
            {
                TurnOffAllLayer();
                hairL_d2d.enabled = true;
                allTurnedOff = false;
            }
        }
        public void TurnOnHairRight()
        {
            if (!hairR_d2d.enabled)
            {
                TurnOffAllLayer();
                hairR_d2d.enabled = true;
                allTurnedOff = false;
            }
        }
        public void TurnOnBabyPowerLeft()
        {
            if (!babyPowerL_d2d.enabled)
            {
                TurnOffAllLayer();
                babyPowerL_d2d.enabled = true;
                allTurnedOff = false;
            }
        }
        public void TurnOnBabyPowerRight()
        {
            if (!babyPowerR_d2d.enabled)
            {
                TurnOffAllLayer();
                babyPowerR_d2d.enabled = true;
                allTurnedOff = false;
            }
        }
        public void TurnOnWaxLeft()
        {
            if (!waxL_d2d.enabled)
            {
                TurnOffAllLayer();
                waxL_d2d.enabled = true;
                allTurnedOff = false;
            }
        }
        public void TurnOnWaxRight()
        {
            if (!waxR_d2d.enabled)
            {
                TurnOffAllLayer();
                waxR_d2d.enabled = true;
                allTurnedOff = false;
            }
        }

        //check what layer to turn on
        private void CheckLayerLeft()
        {
            if (useableObjects[0].isClicked && currentState == 0)
            {
                TurnOnDirtyLeft();
            }
            else if (useableObjects[1].isClicked && currentState == 1)
            {
                TurnOnSoapLeft();
            }
            else if (useableObjects[2].isClicked && currentState == 2)
            {
                fullLeft.transform.position = new Vector3(fullLeft.transform.position.x, fullLeft.transform.position.y, -1.1f);
                TurnOnHairLeft();
            }
            else if (useableObjects[3].isClicked && currentState == 3)
            {
                TurnOnBabyPowerLeft();
            }
            else if (useableObjects[4].isClicked && currentState == 4)
            {
                TurnOnWaxLeft();
            }
            else
            {
                TurnOffAllLayer();
            }
        }
        private void CheckLayerRight()
        {
            if (useableObjects[0].isClicked && currentState == 0)
            {
                TurnOnDirtyRight();
            }
            else if (useableObjects[1].isClicked && currentState == 1)
            {
                TurnOnSoapRight();
            }
            else if (useableObjects[2].isClicked && currentState == 2)
            {
                TurnOnHairRight();
            }
            else if (useableObjects[3].isClicked && currentState == 3)
            {
                TurnOnBabyPowerRight();
            }
            else if (useableObjects[4].isClicked && currentState == 4)
            {
                TurnOnWaxRight();
            }
            else
            {
                TurnOffAllLayer();
            }
        }

        //check when clear
        private void CheckDoneLeft()
        {
            if (currentState == 0 && !useableObjects[0].isClicked && dirtyL_d2d.AlphaRatio < doneScale)
            {
                currentState = 1;
                doneTickImage.SetActive(true);
                dirty_L.gameObject.SetActive(false);
            }
            else if (currentState == 1 && !useableObjects[1].isClicked && soapL_d2d.AlphaRatio < doneScale)
            {
                currentState = 2;
                doneTickImage.SetActive(true);
                soap_L.gameObject.SetActive(false);
            }
            else if (currentState == 2 && !useableObjects[2].isClicked && hairL_d2d.AlphaRatio < doneScale)
            {
                currentState = 3;
                doneTickImage.SetActive(true);
                hair_L.gameObject.SetActive(false);
            }
            else if (currentState == 3 && !useableObjects[3].isClicked && babyPowerL_d2d.AlphaRatio < doneScale*1.5f)
            {
                currentState = 4;
                doneTickImage.SetActive(true);
                babyPower_L.gameObject.SetActive(false);
                waxMaskL.gameObject.SetActive(true);    
                waxPaperL.gameObject.SetActive(true);
                wax_L.gameObject.SetActive(false) ;
            }
        }
        public void SetDoneWax()
        {
            currentState = 0;
            curLeg = "right";
            rightSock.CheckCanMove();
            doneTickImage.SetActive(true);
            wax_L.gameObject.SetActive(false);
            ClearWaxTask.instance.done = 0;
        }
        private void CheckDoneRight()
        {
            if (currentState == 0 && !useableObjects[0].isClicked && dirtyR_d2d.AlphaRatio < doneScale)
            {
                currentState = 1;
                doneTickImage.SetActive(true);
                Debug.Log("Done state 0");
                dirty_R.gameObject.SetActive(false);
            }
            else if (currentState == 1 && !useableObjects[1].isClicked && soapR_d2d.AlphaRatio < doneScale)
            {
                currentState = 2;
                doneTickImage.SetActive(true);
                soap_R.gameObject.SetActive(false);
            }
            else if (currentState == 2 && !useableObjects[2].isClicked && hairR_d2d.AlphaRatio < doneScale)
            {
                currentState = 3;
                doneTickImage.SetActive(true);
                hair_R.gameObject.SetActive(false);
            }
            else if (currentState == 3 && !useableObjects[3].isClicked && babyPowerR_d2d.AlphaRatio < doneScale*1.5f)
            {
                currentState = 4;
                doneTickImage.SetActive(true);
                babyPower_R.gameObject.SetActive(false);
                waxMaskR.gameObject.SetActive(true);
                waxPaperR.gameObject.SetActive(true);
                wax_R.gameObject.SetActive(false);
            }
        }
        public void SetWin()
        {
            doneTickImage.SetActive(true);
            Debug.Log("win");

            PopupManager.ShowToast("WIN");
        }
        public void TurnOnBrush()
        {
            if (!brush.activeSelf)
            {
                brush.gameObject.SetActive(true);
                currentState = 0;
            }
        }
        public void TurnOffBrush()
        {
            if (brush.activeSelf)
            {
                brush.gameObject.SetActive(false);
            }
        }
        public void CheckLoseHeart()
        {
            if (useableObjects[0].isClicked && currentState != 0)
            {
                LifeController.instance.LoseHeart();
            }
            if (useableObjects[1].isClicked && currentState != 1)
            {
                LifeController.instance.LoseHeart();
            }
            if (useableObjects[2].isClicked && currentState != 2)
            {
                LifeController.instance.LoseHeart();
            }
            if (useableObjects[3].isClicked && currentState != 3)
            {
                LifeController.instance.LoseHeart();
            }
            if (useableObjects[4].isClicked && currentState != 4)
            {
                LifeController.instance.LoseHeart();
            }
        }
        public int GetIntL()
        {
            return waxPaperL.transform.childCount;
        }
        public int GetIntR()
        {
            return waxPaperR.transform.childCount;
        }

        public int DecreaseLeft()
        {
            paperLeftAmount--;
            return paperLeftAmount;
        }
        public int DecreaseRight()
        {
            paperRightAmount--;
            return paperRightAmount;
        }
        private void UseobjectMoveOn()
        {
            if(!useObjectOnPos)
            {
                allUseObj.transform.position = Vector3.MoveTowards(allUseObj.transform.position, new Vector3(0, 0, -5), 20f * Time.deltaTime);
                if (allUseObj.transform.position.y <= 0)
                {
                    useObjectOnPos = true;
                    foreach(UseableOject useableObj in useableObjects)
                    {
                        useableObj.UpdateOriginPos();
                    }
                }
            }
        }
        public void SetUseObjectOnPos()
        {
            useObjectOnPos = false;
        }
    }
    
}
