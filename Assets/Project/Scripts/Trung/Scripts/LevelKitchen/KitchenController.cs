using Destructible2D;
using PaintIn3D;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class KitchenController : MonoBehaviour
    {
        public int status { get; private set; }
        private bool status5Check;
        [Header("Status 0")]
        [SerializeField] private GameObject leftSpray;
        [SerializeField] private GameObject rightSpray;
        [SerializeField] private SpriteRenderer leftWaterSprite;
        [SerializeField] private SpriteRenderer rightWaterSprite;

        [Header("Status 1")]
        [SerializeField] private List<ArrangeObject> status1Objects;

        [Header("Status 2")]
        [SerializeField] private BoxCollider2D soap;

        [Header("Status 3")]
        [SerializeField] private UseableObjects sponge;
        [SerializeField] private List<D2dDestructibleSprite> dirts;

        [Header("Status 4")]
        [SerializeField] private GameObject leftFaucet;
        [SerializeField] private GameObject rightFaucet;
        [SerializeField] private SpriteRenderer soapLayer;

        [Header("Status 5")]
        [SerializeField] private List<TruePos> status5TruePos;

        [Header("Status 6")]
        [SerializeField] private List<TruePos> status6TruePos;
        [SerializeField] private BoxCollider2D door;
        [SerializeField] private List<GameObject> setObjects;
        [SerializeField] private Animator closeDoorAnim;

        [Header("Status 7")]
        [SerializeField] private List<D2dDestructibleSprite> status7Dirts;
        [SerializeField] private List<GameObject> originalDirts;
        [SerializeField] private List<GameObject> d2dDirts;
        [SerializeField] private BoxCollider2D towel;

        public static KitchenController instance;

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
        void Start()
        {
            status = 0;
            status5Check = false;
            CheckStatus();

            Application.targetFrameRate = 60;
            PopupManager.Open(PopupPath.MainPopUpTrung, LayerPopup.Main);
        }

        void Update()
        {
            if (status == 1)
            {
                if (CheckAllArangeObject())
                {
                    GoNextStatus();
                }
            }
            if (status == 3)
            {
                if (Status3Check() && !sponge.isClicked)
                {
                    GoNextStatus();
                }
            }
            if (status == 5)
            {
                if (CheckAllArangeObject())
                {
                    StartCoroutine(FadeOut(rightWaterSprite));
                    GoNextStatus();
                }
            }
            if(status == 6)
            {
                CheckStatus6();
            }
            if(status == 7)
            {
                if (CheckWinCondition() && !towel.GetComponent<UseableObjects>().isClicked)
                {
                    Debug.Log("Win!!!");
                    PopupManager.ShowToast("WIN");
                }
            }
        }

        private void CheckStatus6()
        {
            if (status1Objects[0].curTruePos == status6TruePos[0])
            {
                status1Objects[0].gameObject.SetActive(false);
                status1Objects[0].SetIsOnTruePos(true);
                setObjects[0].SetActive(true);
            }
            if (status1Objects[0].curTruePos == status6TruePos[1])
            {
                status1Objects[0].gameObject.SetActive(false);
                status1Objects[0].SetIsOnTruePos(true);
                setObjects[1].SetActive(true);
            }
            if (status1Objects[1].curTruePos == status6TruePos[0])
            {
                status1Objects[1].gameObject.SetActive(false);
                status1Objects[1].SetIsOnTruePos(true);
                setObjects[0].SetActive(true);
            }
            if (status1Objects[1].curTruePos == status6TruePos[1])
            {
                status1Objects[1].gameObject.SetActive(false);
                status1Objects[1].SetIsOnTruePos(true);
                setObjects[1].SetActive(true);
            }
            if (status1Objects[2].curTruePos == status6TruePos[2])
            {
                status1Objects[2].gameObject.SetActive(false);
                status1Objects[2].SetIsOnTruePos(true);
                setObjects[2].SetActive(true);
            }
            if (status1Objects[2].curTruePos == status6TruePos[3])
            {
                status1Objects[2].gameObject.SetActive(false);
                status1Objects[2].SetIsOnTruePos(true);
                setObjects[3].SetActive(true);
            }
            if (status1Objects[3].curTruePos == status6TruePos[2])
            {
                status1Objects[3].gameObject.SetActive(false);
                status1Objects[3].SetIsOnTruePos(true);
                setObjects[4].SetActive(true);
            }
            if (status1Objects[3].curTruePos == status6TruePos[3])
            {
                status1Objects[3].gameObject.SetActive(false);
                status1Objects[3].SetIsOnTruePos(true);
                setObjects[5].SetActive(true);
            }
            if (status1Objects[4].curTruePos == status6TruePos[4])
            {
                status1Objects[4].gameObject.SetActive(false);
                status1Objects[4].SetIsOnTruePos(true);
                setObjects[6].SetActive(true);
            }
            if (CheckAllArangeObject())
            {
                closeDoorAnim.enabled = true;
                GoNextStatus();
            }
        }
        public void LeftOnButton()
        {
            if (status == 0)
            {
                leftSpray.SetActive(true);
                StartCoroutine(FadeInWater(leftWaterSprite));
            }
            else if (status == 4)
            {
                rightSpray.SetActive(true);
                StartCoroutine(FadeInWater(rightWaterSprite));
                StartCoroutine(FadeOut(soapLayer));
            }
        }
        public void RightOnButton()
        {
            rightSpray.SetActive(true);
            StartCoroutine(FadeInWater(rightWaterSprite));
        }
        private IEnumerator FadeInWater(SpriteRenderer water, float duration = 1f)
        {
            water.gameObject.SetActive(true);
            Color color = water.color;
            color.a = 0f;
            water.color = color;
            float timeElapsed = 0;

            while (timeElapsed < duration)
            {
                color.a = Mathf.Lerp(0f, 1f, timeElapsed / duration);
                water.color = color;
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            color.a = 1f;
            water.color = color;
            leftSpray.SetActive(false);
            rightSpray.SetActive(false);
            GoNextStatus();
        }

        private IEnumerator FadeOut(SpriteRenderer soapLayer, float duration = 1f)
        {
            Color color = soapLayer.color;
            float timeElapsed = 0;
            while(timeElapsed < duration)
            {
                color.a = Mathf.Lerp(1f,0f, timeElapsed / duration);
                soapLayer.color = color;
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            color.a = 0f;
            soapLayer.color = color;
            soapLayer.gameObject.SetActive(false);
        }
        
        public void GoNextStatus()
        {
            status++;
            CheckStatus();
        }
        private void CheckStatus()
        {
            if(status == 0)
            {
                foreach(var obj in status1Objects)
                {
                    obj.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                }
                for(int i = 0; i < 2; i++)
                {
                    originalDirts[i].SetActive(true);
                    d2dDirts[i].SetActive(false);
                }
                soap.enabled = false;
                sponge.GetComponent<PolygonCollider2D>().enabled = false;
                leftFaucet.SetActive(true);
                rightFaucet.SetActive(false);
                door.enabled = false;
                towel.enabled = false;
                //TurnDirt(false);
            }
            else if (status == 1)
            {
                foreach (var obj in status1Objects)
                {
                    obj.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                }
            }
            else if (status == 2)
            {
                foreach (var obj in status1Objects)
                {
                    obj.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                }
                soap.enabled = true;
            }
            else if (status == 3)
            {
                soap.enabled = false;
                sponge.GetComponent<PolygonCollider2D>().enabled = true;
            }
            else if(status == 4)
            {
                SetOriginalZ();
                leftFaucet.SetActive(false);
                rightFaucet.SetActive(true);
                sponge.GetComponent<PolygonCollider2D>().enabled = false;
            }
            else if( status == 5)
            {
                for (int i = 0; i < status1Objects.Count; i++)
                {
                    status1Objects[i].gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                    status1Objects[i].ClearTruePos();
                    status1Objects[i].SetTruePos(status5TruePos[i]);
                }
            }
            else if(status == 6)
            {
                for (int i = 0; i < status1Objects.Count; i++)
                {
                    status1Objects[i].gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                    status1Objects[i].ClearTruePos();
                }
                status1Objects[0].SetTruePos(status6TruePos[0]);
                status1Objects[0].SetTruePos(status6TruePos[1]);
                status1Objects[1].SetTruePos(status6TruePos[0]);
                status1Objects[1].SetTruePos(status6TruePos[1]);
                status1Objects[2].SetTruePos(status6TruePos[2]);
                status1Objects[2].SetTruePos(status6TruePos[3]);
                status1Objects[3].SetTruePos(status6TruePos[2]);
                status1Objects[3].SetTruePos(status6TruePos[3]);
                status1Objects[4].SetTruePos(status6TruePos[4]);
                door.enabled = true;
            }
            else if(status == 7)
            {
                for (int i = 0; i < 2; i++)
                {
                    originalDirts[i].SetActive(false);
                    d2dDirts[i].SetActive(true);
                }
                towel.enabled = true;
            }
        }
        
        private bool CheckAllArangeObject()
        {
            foreach (var obj in status1Objects)
            {
                if (!obj.isOnTruePos)
                {
                    return false;
                }
            }
            return true;
        }
        private bool Status3Check()
        {
            foreach (var dirt in dirts)
            {
                if (dirt.AlphaRatio > 0.05)
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckWinCondition()
        {
            foreach(var dirt in status7Dirts)
            {
                if (dirt.AlphaRatio > 0.08)
                {
                    return false;
                }
            }
            return true;
        }

        private void SetOriginalZ()
        {
            foreach(var obj in status1Objects)
            {
                obj.transform.position -= new Vector3 (0f, 0f, obj.transform.position.z);
            }
        }

        //private void TurnDirt(bool status)
        //{
        //    foreach(var dirt in dirts)
        //    {
        //        if(dirt != null)
        //        {
        //            dirt.enabled = status;
        //        }
        //    }
        //}
    }
}
