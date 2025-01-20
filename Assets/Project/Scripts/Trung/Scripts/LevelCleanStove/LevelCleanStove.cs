using Destructible2D;
using SR4BlackDev.UISystem;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class LevelCleanStove : MonoBehaviour
    {
        public int status;
        private float moveSpeed = 15f;
        private float delayTime = 1.5f;
        [Header("Status 0")]
        [SerializeField] List<TakenOutObject> stt0OutObjects;
        [Header("Status 1")]
        [SerializeField] private GameObject stt0Objects;
        [SerializeField] private GameObject stt1Objects;
        [SerializeField] private UseableObjects stt1washingBottle;
        [SerializeField] private UseableObjects stt1Sponge;
        [SerializeField] private D2dDestructibleSprite stt1SoapLayer;
        [SerializeField] private D2dDestructibleSprite stt1DirtLayer;
        [SerializeField] private List<GameObject> stt1Blinks;
        private bool canStt0Out;
        private bool canStt1In;
        [Header("Status 2")]
        [SerializeField] private GameObject stt2Objects;
        [SerializeField] private UseableObjects stt2washingBottle;
        [SerializeField] private UseableObjects stt2Sponge;
        [SerializeField] private D2dDestructibleSprite stt2SoapLayer;
        [SerializeField] private D2dDestructibleSprite stt2DirtLayer;
        [SerializeField] private List<GameObject> stt2Blinks;
        private bool canStt1Out;
        private bool canStt2In;
        [Header("Status 3")]
        [SerializeField] private GameObject stt3Objects;
        [SerializeField] private UseableObjects stt3Scrubber;
        [SerializeField] private UseableObjects stt3Sponge;
        [SerializeField] private D2dDestructibleSprite stt3SoapLayer;
        [SerializeField] private D2dDestructibleSprite stt3DirtLayer;
        [SerializeField] private List<GameObject> stt3Blinks;
        private bool canStt2Out;
        private bool canStt3In;
        [Header("Status 4")]
        [SerializeField] private GameObject stt4Objects;
        [SerializeField] private UseableObjects stt4Scrubber;
        [SerializeField] private UseableObjects stt4Sponge;
        [SerializeField] private D2dDestructibleSprite stt4SoapLayer;
        [SerializeField] private D2dDestructibleSprite stt4DirtLayer;
        [SerializeField] private List<GameObject> stt4Blinks;
        private bool canStt3Out;
        private bool canStt4In;
        [Header("Status 4")]
        [SerializeField] private GameObject stt5Objects;
        [SerializeField] private List<ArrangeObject> stt5ArrangeObjects;
        [SerializeField] private List<PolygonCollider2D> stt5Cols;
        [SerializeField] private List<TruePos> stt5TruePos;
        private bool canStt4Out;
        private bool canStt5In;
        private int curState;
        private void Start()    
        {
            CheckStatus();
            SetStartValue();

            Application.targetFrameRate = 60;
            PopupManager.Open(PopupPath.MainPopUpTrung, LayerPopup.Main);
        }
        private void Update()
        {
            CheckOnUpdate();
        }
        private void SetStartValue()
        {
            status = 0;
            canStt0Out = false;
            canStt1In = false;
            canStt1Out = false;
            canStt2In = false;
            canStt2Out = false;
            canStt3In = false;
            canStt3Out = false;
            canStt4In = false;
            canStt4Out = false;
            canStt5In = false;
            curState = 0;
        }

        private void CheckOnUpdate()
        {
            if(status == 0)
            {
                if (CheckStatus0())
                {
                    GoNextStatus();
                }
            }
            else if(status == 1)
            {
                Stt0MoveOut();
                Stt1MoveIn();
                CheckStatus1();
            }
            else if (status == 2)
            {
                Stt1MoveOut();
                Stt2MoveIn();
                CheckStatus2();
            }
            else if(status == 3)
            {
                Stt2MoveOut();
                Stt3MoveIn();
                CheckStatus3();
            }
            else if(status == 4)
            {
                Stt3MoveOut();
                Stt4MoveIn();
                CheckStatus4();
            }
            else if(status == 5)
            {
                Stt4MoveOut();
                Stt5MoveIn();
                for(int i = 0; i < 3; i++)
                {
                    if (stt5Cols[i].enabled && stt5ArrangeObjects[i].isOnTruePos)
                    {
                        stt5Cols[i].enabled = false;
                        curState++;
                    }
                }
                if(curState == 0 && stt5TruePos.Count == 3)
                {
                    stt5ArrangeObjects[0].SetTruePos(stt5TruePos[0]);
                    stt5TruePos.RemoveAt(0);
                }
                else if (curState == 1 && stt5TruePos.Count == 2)
                {
                    stt5ArrangeObjects[1].SetTruePos(stt5TruePos[0]);
                    stt5TruePos.RemoveAt(0);
                }
                else if (curState == 2 && stt5TruePos.Count == 1)
                {
                    stt5ArrangeObjects[2].SetTruePos(stt5TruePos[0]);
                    stt5TruePos.RemoveAt(0);
                }
                else if (curState == 3)
                {
                    GoNextStatus();
                    Debug.Log("WWINN");
                    PopupManager.ShowToast("WIN");

                }
            }
        }
        private void CheckStatus()
        {
            if (status == 0)
            {
                //0
                stt0Objects.SetActive(true);
                stt0Objects.transform.position = Vector3.zero;
                //1
                stt1Objects.SetActive(false);
                stt1Objects.transform.position = new Vector3(-10, 0, 0);
                stt1washingBottle.GetComponent<BoxCollider2D>().enabled = false;
                stt1Sponge.GetComponent<BoxCollider2D>().enabled = false;
                foreach (var blink in stt1Blinks)
                {
                    blink.SetActive(false);
                }
                //2
                stt2Objects.SetActive(false);
                stt2Objects.transform.position = new Vector3(-10, 0, 0);
                stt2washingBottle.GetComponent<BoxCollider2D>().enabled = false;
                stt2Sponge.GetComponent<BoxCollider2D>().enabled = false;
                foreach (var blink in stt2Blinks)
                {
                    blink.SetActive(false);
                }
                //3
                stt3Objects.SetActive(false);
                stt3Objects.transform.position = new Vector3(-10, 0, 0);
                stt3Scrubber.GetComponent<BoxCollider2D>().enabled = false;
                stt3Sponge.GetComponent<BoxCollider2D>().enabled = false;
                foreach (var blink in stt3Blinks)
                {
                    blink.SetActive(false);
                }
                //4
                stt4Objects.SetActive(false);
                stt4Objects.transform.position = new Vector3(-10, 0, 0);
                stt4Scrubber.GetComponent<BoxCollider2D>().enabled = false;
                stt4Sponge.GetComponent<BoxCollider2D>().enabled = false;
                foreach (var blink in stt4Blinks)
                {
                    blink.SetActive(false);
                }
                //5
                stt5Objects.SetActive(false);
                stt5Objects.transform.position = new Vector3(-10, 0, 0);
            }
            else if (status == 1)
            {
                canStt0Out = true;
            }
            else if (status == 2)
            {
                canStt1Out = true;
            }
            else if(status == 3)
            {
                canStt2Out = true;
            }
            else if(status == 4)
            {
                canStt3Out = true;
            }
            else if(status == 5)
            {
                canStt4Out = true;
            }
        }
        private void GoNextStatus()
        {
            status++;
            CheckStatus();
        }
        private bool CheckStatus0()
        {
            foreach(var stt1OutObject in stt0OutObjects)
            {
                if (!stt1OutObject.isOut)
                {
                    return false;
                }
            }
            return true;
        }

        //stt1 move
        private void Stt0MoveOut()
        {
            if (canStt0Out)
            {
                stt0Objects.transform.position = Vector3.MoveTowards(stt0Objects.transform.position, new Vector3(-10,0,0), moveSpeed * Time.deltaTime);
                if(Vector3.Distance(stt0Objects.transform.position, new Vector3(-10, 0, 0)) < 0.02f)
                {
                    canStt0Out = false;
                    stt0Objects.SetActive(false);
                    stt1Objects.SetActive(true);
                    canStt1In = true;
                }
            }
        }
        private void Stt1MoveIn()
        {
            if (canStt1In)
            {
                stt1Objects.transform.position = Vector3.MoveTowards(stt1Objects.transform.position, new Vector3(0, 0, 0), moveSpeed * Time.deltaTime);
                if (Vector3.Distance(stt1Objects.transform.position, new Vector3(0, 0, 0)) < 0.02f)
                {
                    canStt1In = false;
                    //update ori pos
                    stt1washingBottle.UpdateOriginPos();
                    stt1Sponge.UpdateOriginPos();
                    //on collider
                    stt1washingBottle.GetComponent<BoxCollider2D>().enabled = true;
                    stt1Sponge.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        //stt2 move
        private void Stt1MoveOut()
        {
            if (canStt1Out)
            {
                stt1Objects.transform.position = Vector3.MoveTowards(stt1Objects.transform.position, new Vector3(-10, 0, 0), moveSpeed * Time.deltaTime);
                if (Vector3.Distance(stt1Objects.transform.position, new Vector3(-10, 0, 0)) < 0.02f)
                {
                    canStt1Out = false;
                    stt1Objects.SetActive(false);
                    stt2Objects.SetActive(true);
                    canStt2In = true;
                }
            }
        }
        private void Stt2MoveIn()
        {
            if (canStt2In)
            {
                stt2Objects.transform.position = Vector3.MoveTowards(stt2Objects.transform.position, new Vector3(0, 0, 0), moveSpeed * Time.deltaTime);
                if (Vector3.Distance(stt2Objects.transform.position, new Vector3(0, 0, 0)) < 0.02f)
                {
                    canStt2In = false;
                    //update ori pos
                    stt2washingBottle.UpdateOriginPos();
                    stt2Sponge.UpdateOriginPos();
                    //on collider
                    stt2washingBottle.GetComponent<BoxCollider2D>().enabled = true;
                    stt2Sponge.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        //stt3 move
        private void Stt2MoveOut()
        {
            if (canStt2Out)
            {
                stt2Objects.transform.position = Vector3.MoveTowards(stt2Objects.transform.position, new Vector3(-10, 0, 0), moveSpeed * Time.deltaTime);
                if (Vector3.Distance(stt2Objects.transform.position, new Vector3(-10, 0, 0)) < 0.02f)
                {
                    canStt2Out = false;
                    stt2Objects.SetActive(false);
                    stt3Objects.SetActive(true);
                    canStt3In = true;
                }
            }
        }
        private void Stt3MoveIn()
        {
            if (canStt3In)
            {
                stt3Objects.transform.position = Vector3.MoveTowards(stt3Objects.transform.position, new Vector3(0, 0, 0), moveSpeed * Time.deltaTime);
                if (Vector3.Distance(stt3Objects.transform.position, new Vector3(0, 0, 0)) < 0.02f)
                {
                    canStt3In = false;
                    //update ori pos
                    stt3Scrubber.UpdateOriginPos();
                    stt3Sponge.UpdateOriginPos();
                    //on collider
                    stt3Scrubber.GetComponent<BoxCollider2D>().enabled = true;
                    stt3Sponge.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        //stt4 move
        private void Stt3MoveOut()
        {
            if (canStt3Out)
            {
                stt3Objects.transform.position = Vector3.MoveTowards(stt3Objects.transform.position, new Vector3(-10, 0, 0), moveSpeed * Time.deltaTime);
                if (Vector3.Distance(stt3Objects.transform.position, new Vector3(-10, 0, 0)) < 0.02f)
                {
                    canStt3Out = false;
                    stt3Objects.SetActive(false);
                    stt4Objects.SetActive(true);
                    canStt4In = true;
                }
            }
        }
        private void Stt4MoveIn()
        {
            if (canStt4In)
            {
                stt4Objects.transform.position = Vector3.MoveTowards(stt4Objects.transform.position, new Vector3(0, 0, 0), moveSpeed * Time.deltaTime);
                if (Vector3.Distance(stt4Objects.transform.position, new Vector3(0, 0, 0)) < 0.02f)
                {
                    canStt4In = false;
                    //update ori pos
                    stt4Scrubber.UpdateOriginPos();
                    stt4Sponge.UpdateOriginPos();
                    //on collider
                    stt4Scrubber.GetComponent<BoxCollider2D>().enabled = true;
                    stt4Sponge.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        //stt5 move
        private void Stt4MoveOut()
        {
            if (canStt4Out)
            {
                stt4Objects.transform.position = Vector3.MoveTowards(stt4Objects.transform.position, new Vector3(-10, 0, 0), moveSpeed * Time.deltaTime);
                if (Vector3.Distance(stt4Objects.transform.position, new Vector3(-10, 0, 0)) < 0.02f)
                {
                    canStt4Out = false;
                    stt4Objects.SetActive(false);
                    stt5Objects.SetActive(true);
                    canStt5In = true;
                }
            }
        }
        private void Stt5MoveIn()
        {
            if (canStt5In)
            {
                stt5Objects.transform.position = Vector3.MoveTowards(stt5Objects.transform.position, new Vector3(0, 0, 0), moveSpeed * Time.deltaTime);
                if (Vector3.Distance(stt5Objects.transform.position, new Vector3(0, 0, 0)) < 0.02f)
                {
                    canStt5In = false;
                }
            }
        }
        //check stt1
        private void CheckStatus1()
        {
            if (stt1washingBottle.isClicked)
            {
                stt1DirtLayer.enabled = true;
                stt1SoapLayer.enabled = false;
            }
            else if (stt1Sponge.isClicked)
            {
                stt1DirtLayer.enabled = false;
                if (stt1DirtLayer.AlphaRatio < 0.01)
                {
                    stt1SoapLayer.enabled = true;
                }
                else stt1SoapLayer.enabled = false;
            }
            if(stt1SoapLayer.AlphaRatio < 0.01f && stt1Sponge.isOnTruePos)
            {
                stt1SoapLayer.gameObject.SetActive(false);
                stt1DirtLayer.gameObject.SetActive(false);

                stt1washingBottle.GetComponent<BoxCollider2D>().enabled = false;
                stt1Sponge.GetComponent<BoxCollider2D>().enabled = false;

                foreach (var blink in stt1Blinks)
                {
                    if (!blink.activeSelf)
                    {
                        FeelingTool.instance.FadeInImplement(blink, Random.Range(delayTime / 2, delayTime));
                    }
                }
                DelayGoNextStatus();
            }
        }
        //check stt2
        private void CheckStatus2()
        {
            if (stt2washingBottle.isClicked)
            {
                stt2DirtLayer.enabled = true;
                stt2SoapLayer.enabled = false;
            }
            else if (stt2Sponge.isClicked)
            {
                stt2DirtLayer.enabled = false;
                if (stt2DirtLayer.AlphaRatio < 0.01)
                {
                    stt2SoapLayer.enabled = true;
                }
                else stt2SoapLayer.enabled = false;
            }
            if (stt2SoapLayer.AlphaRatio < 0.01f && stt2Sponge.isOnTruePos)
            {
                stt2SoapLayer.gameObject.SetActive(false);
                stt2DirtLayer.gameObject.SetActive(false);

                stt2washingBottle.GetComponent<BoxCollider2D>().enabled = false;
                stt2Sponge.GetComponent<BoxCollider2D>().enabled = false;

                foreach (var blink in stt2Blinks)
                {
                    if (!blink.activeSelf)
                    {
                        FeelingTool.instance.FadeInImplement(blink, Random.Range(delayTime / 2, delayTime));
                    }
                }
                DelayGoNextStatus();
            }
        }
        //check stt3
        private void CheckStatus3()
        {
            if (stt3Scrubber.isClicked)
            {
                stt3DirtLayer.enabled = true;
                stt3SoapLayer.enabled = false;
            }
            else if (stt3Sponge.isClicked)
            {
                stt3DirtLayer.enabled = false;
                if (stt3DirtLayer.AlphaRatio < 0.01)
                {
                    stt3SoapLayer.enabled = true;
                }
                else stt3SoapLayer.enabled = false;
            }
            if (stt3SoapLayer.AlphaRatio < 0.01f && stt3Sponge.isOnTruePos)
            {
                stt3SoapLayer.gameObject.SetActive(false);
                stt3DirtLayer.gameObject.SetActive(false);

                stt3Scrubber.GetComponent<BoxCollider2D>().enabled = false;
                stt3Sponge.GetComponent<BoxCollider2D>().enabled = false;

                foreach (var blink in stt3Blinks)
                {
                    if (!blink.activeSelf)
                    {
                        FeelingTool.instance.FadeInImplement(blink, Random.Range(delayTime / 2, delayTime));
                    }
                }
                DelayGoNextStatus();
            }
        }
        //check stt4
        private void CheckStatus4()
        {
            if (stt4Scrubber.isClicked)
            {
                stt4DirtLayer.enabled = true;
                stt4SoapLayer.enabled = false;
            }
            else if (stt4Sponge.isClicked)
            {
                stt4DirtLayer.enabled = false;
                if (stt4DirtLayer.AlphaRatio < 0.01)
                {
                    stt4SoapLayer.enabled = true;
                }
                else stt4SoapLayer.enabled = false;
            }
            if (stt4SoapLayer.AlphaRatio < 0.01f && stt4Sponge.isOnTruePos)
            {
                stt4SoapLayer.gameObject.SetActive(false);
                stt4DirtLayer.gameObject.SetActive(false);

                stt4Scrubber.GetComponent<BoxCollider2D>().enabled = false;
                stt4Sponge.GetComponent<BoxCollider2D>().enabled = false;

                foreach (var blink in stt4Blinks)
                {
                    if (!blink.activeSelf)
                    {
                        FeelingTool.instance.FadeInImplement(blink, Random.Range(delayTime/2, delayTime));
                    }
                }
                DelayGoNextStatus();
            }
        }

        //delay
        private void DelayGoNextStatus()
        {
            if (delayTime > 0)
            {
                delayTime -= Time.deltaTime;
            }
            else if(delayTime <=0 && delayTime > -0.5f)
            {
                delayTime = -1;
                GoNextStatus();
                ResetDelayTime();
            }
        }
        private void ResetDelayTime()
        {
            delayTime = 1.5f;
        }
    }
}
