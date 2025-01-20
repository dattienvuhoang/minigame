using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class LevelGardenController : MonoBehaviour
    {
        public static LevelGardenController instance;
        public int status { get; private set; }
        //1
        private int status1Condition;
        private bool addedTrash;

        [Header("Status 0")]
        [SerializeField] private ArrangeObject binFall;

        [Header("Status 1")]
        [SerializeField] private List<ArrangeObject> trash;
        [SerializeField] private BoxCollider2D tool1;

        [Header("Status 2")]
        [SerializeField] private UseableObjects tool2;
        [SerializeField] private List<SpriteRenderer> soil1;

        [Header("Status 3")]
        [SerializeField] private UseableObjects tool3;
        [SerializeField] private List<SpriteRenderer> soil2;

        [Header("Status 4")]
        [SerializeField] private List<ArrangeObject> seedPacks;

        [Header("Status 5")]
        [SerializeField] private UseableObjects water;
        [SerializeField] private List<Soil2> soil2s;
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
            CheckStatus();
            //1
            status1Condition = 0;
            addedTrash = false;

            Application.targetFrameRate = 60;
            PopupManager.Open(PopupPath.MainPopUpTrung, LayerPopup.Main);

        }
        private void Update()
        {
            if (status == 0)
            {
                if (binFall.isOnTruePos)
                {
                    GoNextStatus();
                }
            }
            else if (status == 1)
            {
                if(!addedTrash)
                {
                    if (CheckTrash())
                    {
                        addedTrash = true;
                        status1Condition++;
                    }
                }
                if(status1Condition == 2)
                {
                    GoNextStatus();
                }
            }
            else if(status == 2)
            {
                if (Status2Check() && !tool2.isClicked)
                {
                    GoNextStatus();
                }
            }
            else if (status == 3)
            {
                if(Status3Check() && !tool3.isClicked)
                {
                    GoNextStatus();
                }
            }
            else if(status == 4)
            {
                if (Status4Check())
                {
                    GoNextStatus() ;
                }
            }
            else if(status == 5)
            {
                if (Status5Check())
                {
                    GoNextStatus();
                }
            }
            else if (status == 6)
            {
                if (water.isOnTruePos)
                {
                    PopupManager.ShowToast("WIN");
                    GoNextStatus();
                }
            }
        }
        private void CheckStatus()
        {
            if (status == 0)
            {
                Debug.Log("Check");
                binFall.GetComponent<BoxCollider2D>().enabled = true;

                foreach (var t in trash)
                {
                    t.GetComponent<BoxCollider2D>().enabled = false;
                }
                foreach (var s in soil2)
                {
                    s.gameObject.SetActive(false);
                }
                foreach (var s in soil1)
                {
                    s.gameObject.SetActive(false);
                }
                foreach(var s in seedPacks)
                {
                    s.GetComponent<BoxCollider2D>().enabled = false;
                }
                tool1.enabled = false;
                tool2.GetComponent<BoxCollider2D>().enabled = false;
                tool3.GetComponent<BoxCollider2D>().enabled = false;
                water.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (status == 1)
            {
                foreach (var t in trash)
                {
                    t.GetComponent<BoxCollider2D>().enabled = true;
                }
                foreach(var s in soil1)
                {
                    s.gameObject.SetActive(false);
                }

                tool1.enabled = true;
            }
            else if(status == 2)
            {
                tool1.enabled = false;

                tool2.GetComponent<BoxCollider2D>().enabled = true;
                foreach (var s in soil1)
                {
                    s.gameObject.SetActive(true);
                    Color color = s.color;
                    color.a = 0;
                    s.color = color;
                }
            }
            else if(status == 3)
            {
                tool2.GetComponent<BoxCollider2D>().enabled = false;

                tool3.GetComponent<BoxCollider2D>().enabled = true;
                foreach (var s in soil2)
                {
                    s.gameObject.SetActive(true);
                    Color color = s.color;
                    color.a = 0;
                    s.color = color;
                }
            }
            else if (status == 4)
            {
                foreach (var s in seedPacks)
                {
                    s.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            else if(status == 5)
            {
                water.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        private void GoNextStatus()
        {
            status++;
            CheckStatus();
            Debug.Log("go");
        }
        private bool CheckTrash()
        {
            foreach (var t in trash)
            {
                if (!t.isOnTruePos)
                {
                    return false;
                }
            }
            return true;
        }
        public void SetStatus1Condition()
        {
            status1Condition++;
        }

        private bool Status2Check()
        {
            foreach(var s in soil1)
            {
                if(s.color.a != 1)
                {
                    return false;
                }
            }
            return true;
        }

        private bool Status3Check()
        {
            foreach (var s in soil2)
            {
                if (s.color.a != 1)
                {
                    return false;
                }
            }
            return true;
        }
        private bool Status4Check()
        {
            foreach (var s in seedPacks)
            {
                if (!s.isOnTruePos)
                {
                    return false;
                }
            }
            return true;
        }
        private bool Status5Check()
        {
            foreach (var s in soil2s)
            {
                Debug.Log($"{s}: {s.isPlanted}");
                if (!s.isPlanted)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
