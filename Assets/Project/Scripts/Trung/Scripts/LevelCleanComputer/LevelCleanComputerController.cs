using Destructible2D;
using SR4BlackDev.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class LevelCleanComputerController : MonoBehaviour
    {
        public int status;
        private float moveSpeed = 15f;
        public static LevelCleanComputerController instance;
        [Header("Status 0")]
        [SerializeField] private D2dDestructibleSprite waterDropMask;
        [SerializeField] private UseableObjects bottleSpray;
        [Header("Status 1")]
        [SerializeField] private D2dDestructibleSprite desktopDustMask;
        [SerializeField] private D2dDestructibleSprite waterDrop;
        [SerializeField] private UseableObjects towel;

        [Header("Status 2")]
        [SerializeField] private BoxCollider2D keyBoardCol;
        [SerializeField] private GameObject scene1Objects;
        [SerializeField] private GameObject scene2Objects;
        private bool canMove1;
        private bool canMove2;
        [Header("Status 3")]
        [SerializeField] private UseableObjects brush1;
        [SerializeField] private D2dDestructibleSprite dirt1;
        [Header("Status 4")]
        [SerializeField] private UseableObjects tool;
        [SerializeField] private GameObject status4UselessObj;
        [SerializeField] private KeyBox keyBox;
        [SerializeField] private List<KeyCaps> keyCaps;
        private bool canMove4_1;
        private bool canMove4_2;
        private bool canMove4_3;
        [Header("Status 5")]
        [SerializeField] private UseableObjects brush2;
        [SerializeField] private D2dDestructibleSprite dirt2;
        [Header("Status 6")]
        [SerializeField] private UseableObjects towel2;
        [SerializeField] private D2dDestructibleSprite dirt3;
        [Header("Status 7")]
        [SerializeField] private GameObject scene2Tools;
        [SerializeField] private Transform keyBoard_AllCaps;
        [SerializeField] private List<ArrangeObject> keyCapsArrange; 
        private bool canMove7_1;
        private bool canMove7_2;
        [Header("Status 8")]
        [SerializeField] private GameObject scene3Objects;
        private bool canScene3Go;
        private bool canScene3Go_2;
        [Header("Status 9")]
        [SerializeField] private UseableObjects bottleSpray2;
        [SerializeField] private D2dDestructibleSprite waterDropMaskCase;
        [Header("Status 10")]
        [SerializeField] private UseableObjects towel3;
        [SerializeField] private D2dDestructibleSprite dirt4;
        [SerializeField] private D2dDestructibleSprite waterDrop2;
        [Header("Status 11")]
        [SerializeField] private BoxCollider2D screwDriver;
        [SerializeField] private GameObject caseGlass;
        [SerializeField] private List<Screws> screws;
        private bool canCaseGlassMove;
        [Header("Status 12")]
        [SerializeField] private GameObject toolSet1;
        [SerializeField] private GameObject toolSet2;
        private bool canToolSet1Move;
        private bool canToolSet2Move;
        [Header("Status 13")]
        [SerializeField] private D2dDestructibleSprite dirt5;
        [SerializeField] private UseableObjects brush3;
        [Header("Status 14")]
        [SerializeField] private UseableObjects vaccum;
        [SerializeField] private D2dDestructibleSprite dirt6;
        [Header("Status 15")]
        private bool canToolSet2MoveOut;
        private bool canGlassMoveIn;
        [Header("Status 16")]
        [SerializeField] private GameObject screws2;
        [SerializeField] private List<UseArrangeableObjects> screws3;
        [Header("Status 17")]
        [SerializeField] private GameObject cleanSet;
        private bool scene3MoveOut;
        private bool cleanCaseMoveIn;
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
            SetStartValue();
            CheckStatus();

            Application.targetFrameRate = 60;
            PopupManager.Open(PopupPath.MainPopUpTrung, LayerPopup.Main);

        }
        private void SetStartValue()
        {
            canMove1 = false;
            canMove2 = false;
            canMove4_1 = false;
            canMove4_2 = false;
            canMove4_3 = false;
            canMove7_1 = false;
            canMove7_2 = false;
            canScene3Go = false;
            canScene3Go_2 = false;
            canCaseGlassMove = false;
            canToolSet1Move = false;
            canToolSet2Move = false;
            canGlassMoveIn = false;
            canToolSet2MoveOut = false;
            scene3MoveOut = false;
            cleanCaseMoveIn = false;
            status = 0;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GoNextStatus();
            }
            if (status == 0)
            {
                CheckStatus0();
            }
            else if (status == 1)
            {
                CheckStatus1();
            }
            else if (status == 2)
            {
                if (canMove1)
                {
                    Move1();
                }
                else if (canMove2)
                {
                    Move2();
                }
            }
            else if (status == 3)
            {
                CheckStatus3();
            }
            else if (status == 4)
            {
                Status4Move();
                Status4Move2();
                Status4Move3();
                if (CheckStatus4())
                {
                    keyBox.SetOut();
                    canMove4_3 = true;
                }
            }
            else if(status == 5)
            {
                CheckStatus5();
            }
            else if(status == 6)
            {
                CheckStatus6();
            }
            else if(status == 7)
            {
                Status7Move();
                if (CheckStatus7())
                {
                    GoNextStatus();
                }
            }
            else if(status == 8)
            {
                GoToScene3();
            }
            else if(status == 9)
            {
                CheckStatus9();
            }
            else if(status == 10)
            {
                CheckStatus10();
            }
            else if(status == 11)
            {
                CheckStatus11();
                MoveCaseGlass();
            }
            else if(status == 12)
            {
                ToolSet1Move();
                ToolSet2Move();
            }
            else if(status == 13)
            {
                CheckStatus13();
            }
            else if(status == 14)
            {
                CheckStatus14();
                ToolSet2MoveOut();
            }
            else if (status == 15)
            {
                MoveInGlass();
            }
            else if(status == 16)
            {
                if (CheckStatus16())
                {
                    GoNextStatus();
                }
            }
            else if (status == 17)
            {
                Scene3MoveOut();
                CleanCaseMoveIn();
            }
        }

        private void CheckStatus()
        {
            if (status == 0)
            {
                waterDropMask.enabled = true;
                bottleSpray.transform.GetChild(0).gameObject.SetActive(true);
                scene1Objects.SetActive(true);
                //turn off other task
                scene2Objects.transform.position = new Vector3(-10, 0, 0);
                keyBox.transform.position = new Vector3(-10, 10, 0);
                keyBox.gameObject.SetActive(false);
                scene2Objects.SetActive(false);
                towel.transform.GetChild(0).gameObject.SetActive(false);
                keyBoardCol.enabled = false;
                brush1.transform.GetChild(0).gameObject.SetActive(false);
                tool.transform.GetChild(0).gameObject.SetActive(false);
                //5&6
                brush2.transform.GetChild(0).gameObject.SetActive(false);
                towel2.transform.GetChild(0).gameObject.SetActive(false);
                //7
                TurnOffKeyCapArrange();
                //8
                scene3Objects.SetActive(false);
                scene3Objects.transform.position = new Vector3(10,0,0);
                //9
                bottleSpray2.transform.GetChild(0).gameObject.SetActive(false);
                //10
                towel3.transform.GetChild(0).gameObject.SetActive(false);
                //11
                screwDriver.enabled = false;
                //16
                screws2.gameObject.SetActive(false);
                //17
                cleanSet.SetActive(false);
                cleanSet.transform.position = new Vector3(-10, 0, 0);

            }
            else if (status == 1)
            {
                desktopDustMask.enabled = true;
                towel.transform.GetChild(0).gameObject.SetActive(true);
                waterDrop.enabled = true;
                //turn off other task
                bottleSpray2.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (status == 2)
            {
                keyBoardCol.enabled = true;
                //turn off other task
                towel.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (status == 3)
            {
                brush1.transform.GetChild(0).gameObject.SetActive(true);
                dirt1.enabled = true;
            }
            else if (status == 4)
            {
                canMove4_1 = true;
                keyBox.gameObject.SetActive(true);
                tool.transform.GetChild(0).gameObject.SetActive(true);
                //turn off other task
                brush1.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (status == 5)
            {
                brush2.transform.GetChild(0).gameObject.SetActive(true);
                
            }
            else if(status == 6)
            {
                towel2.transform.GetChild(0).gameObject.SetActive(true);
                brush2.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if(status == 7)
            {
                towel2.transform.GetChild(0).gameObject.SetActive(false);
                TurnOnKeyCapArrange();
                canMove7_1 = true;
            }
            else if(status == 8)
            {
                scene3Objects.SetActive(true);
                toolSet2.SetActive(false);
                canScene3Go = true;
            }
            else if(status == 9)
            {
                bottleSpray2.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (status == 10)
            {
                bottleSpray2.transform.GetChild(0).gameObject.SetActive(false);

                towel3.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (status == 11)
            {
                towel3.transform.GetChild(0).gameObject.SetActive(false);

                screwDriver.enabled = true;
            }
            else if (status == 12)
            {
                toolSet2.transform.position = new Vector3(10, 0, 0);
                canToolSet1Move = true;
                brush3.transform.GetChild(0).gameObject.SetActive(false);
                vaccum.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if(status == 13)
            {
                brush3.transform.GetChild(0).gameObject.SetActive(true);
                
            }
            else if (status == 14)
            {
                vaccum.transform.GetChild(0).gameObject.SetActive(true);
                brush3.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (status == 15)
            {
                vaccum.transform.GetChild(0).gameObject.SetActive(false);
                screws2.gameObject.SetActive(true);
                canGlassMoveIn = true;
                caseGlass.gameObject.SetActive(true);
            }
            else if(status == 17)
            {
                scene3MoveOut = true;
            }
            else if (status == 18)
            {
                Debug.Log("WIN");
            }
        }

        private void CheckStatus0()
        {
            if (bottleSpray.isClicked)
            {
                desktopDustMask.enabled = false;
                waterDrop.enabled = false;
            }
            if (waterDropMask.AlphaRatio <= 0.01 && bottleSpray.isOnTruePos)
            {
                waterDropMask.gameObject.SetActive(false);
                GoNextStatus();
            }
        }
        private void CheckStatus1()
        {
            if (desktopDustMask.AlphaRatio <= 0.01 && towel.isOnTruePos)
            {
                desktopDustMask.gameObject.SetActive(false);
                GoNextStatus();
            }
        }
        private void CheckStatus3()
        {
            if (brush1.isClicked)
            {
                dirt1.enabled = true;
                dirt2.enabled = false;
                dirt3.enabled = false;
            }
            if (dirt1.AlphaRatio <= 0.01 && brush1.isOnTruePos)
            {
                dirt1.gameObject.SetActive(false);
                GoNextStatus();
            }
        }
        public void GoNextStatus()
        {
            status++;
            CheckStatus();
        }

        private void Move1()
        {
            scene1Objects.transform.position = Vector3.MoveTowards(scene1Objects.transform.position,
                new Vector3(-7.1f, 0, 0), moveSpeed * Time.deltaTime);
            if (scene1Objects.transform.position.x <= -7f)
            {
                scene1Objects.SetActive(false);
                canMove1 = false;
                canMove2 = true;
            }
        }
        private void Move2()
        {
            if (!scene2Objects.activeSelf)
            {
                scene2Objects.SetActive(true);
            }
            scene2Objects.transform.position = Vector3.MoveTowards(scene2Objects.transform.position,
                new Vector3(0, 0, 0), moveSpeed * Time.deltaTime);
            if (Vector3.Distance(scene2Objects.transform.position, new Vector3(0, 0, 0)) < 0.02f)
            {
                canMove2 = false;
                brush1.UpdateOriginPos();
                brush2.UpdateOriginPos();
                tool.UpdateOriginPos();
                towel2.UpdateOriginPos();
                GoNextStatus();
            }
        }
        public void SetMove1()
        {
            canMove1 = true;
        }

        private void Status4Move()
        {
            if (canMove4_1)
            {
                status4UselessObj.transform.position = Vector3.MoveTowards(status4UselessObj.transform.position, new Vector3(0, 7, 0), 15f * Time.deltaTime);
                if (Vector3.Distance(status4UselessObj.transform.position, new Vector3(0, 7, 0)) < 0.02f)
                {
                    canMove4_1 = false;
                    canMove4_2 = true;
                }
            }
        }
        private void Status4Move2()
        {
            if (canMove4_2)
            {
                keyBox.transform.position = Vector3.MoveTowards(keyBox.transform.position, new Vector3(0, 2.5f, 0), 15f * Time.deltaTime);
                if (Vector3.Distance(keyBox.transform.position, new Vector3(0, 2.5f, 0)) < 0.02f)
                {
                    canMove4_2 = false;
                }
            }
        }
        private void Status4Move3()
        {
            if (canMove4_3)
            {
                status4UselessObj.transform.position = Vector3.MoveTowards(status4UselessObj.transform.position, new Vector3(0, 0, 0), 15f * Time.deltaTime);
                if (Vector3.Distance(status4UselessObj.transform.position, new Vector3(0, 0, 0)) < 0.02f)
                {
                    canMove4_3 = false;
                    GoNextStatus();
                }
            }
        }
        private bool CheckStatus4()
        {
            foreach (var keyCap in keyCaps)
            {
                if (!keyCap.isOnTruePos || !tool.isOnTruePos)
                {
                    return false;
                }
            }
            return true;
        }

        private void CheckStatus5()
        {
            if (brush2.isClicked)
            {
                dirt2.enabled = true;
                dirt1.enabled = false;
                dirt3.enabled = false;
            }
            if(dirt2.AlphaRatio < 0.01 && brush2.isOnTruePos)
            {
                dirt2.gameObject.SetActive(false);
                GoNextStatus();
            }
        }
        private void CheckStatus6()
        {
            if (towel2.isClicked)
            {
                dirt2.enabled = false;
                dirt1.enabled = false;
                dirt3.enabled = true;
            }
            if (dirt3.AlphaRatio < 0.01 && towel2.isOnTruePos)
            {
                dirt3.gameObject.SetActive(false);
                GoNextStatus();
            }
        }
        private void TurnOffKeyCapArrange()
        {
            foreach(var keyCap in keyCapsArrange)
            {
                keyCap.enabled = false;
            }
        }
        private void TurnOnKeyCapArrange()
        {
            foreach (var keyCap in keyCapsArrange)
            {
                keyCap.enabled = true;
                keyCap.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        private void Status7Move()
        {
            if (canMove7_1)
            {
                scene2Tools.transform.position = Vector3.MoveTowards(scene2Tools.transform.position,
                    new Vector3(-10, 0, 0), 15f * Time.deltaTime);
                if(Vector3.Distance(scene2Tools.transform.position,new Vector3(-10, 0, 0)) < 0.02f)
                {
                    canMove7_1 = false;
                    canMove7_2 = true;
                }
            }
            else if (canMove7_2)
            {
                keyBox.SetIn();
                canMove7_2 = false;
            }
        }
        private bool CheckStatus7()
        {
            foreach(ArrangeObject keyCap in keyCapsArrange)
            {
                if (!keyCap.isOnTruePos)
                {
                    return false;
                }
            }
            SetKeyCapsParent();
            return true;
        }
        private void SetKeyCapsParent()
        {
            foreach(var keyCap in keyCapsArrange)
            {
                keyCap.gameObject.transform.SetParent(keyBoard_AllCaps);
            }
        }
        private void GoToScene3()
        {
            if (canScene3Go)
            {
                scene2Objects.transform.position = Vector3.MoveTowards(scene2Objects.transform.position,
                new Vector3(-10, 0, 0), 15f * Time.deltaTime);
                if (Vector3.Distance(scene2Objects.transform.position, new Vector3(-10, 0, 0)) < 0.02f)
                {
                    canScene3Go = false;
                    canScene3Go_2 = true;
                    scene2Objects.SetActive(false);
                }
            }
            else if (canScene3Go_2)
            {
                scene3Objects.transform.position = Vector3.MoveTowards(scene3Objects.transform.position,
                new Vector3(0, 0, 0), 15f * Time.deltaTime);
                if (Vector3.Distance(scene3Objects.transform.position, new Vector3(0, 0, 0)) < 0.02f)
                {
                    canScene3Go_2 = false;               
                    GoNextStatus();
                }
            }
        }
        private void CheckStatus9()
        {
            if (bottleSpray2.isClicked)
            {
                waterDropMaskCase.enabled = true;
                dirt4.enabled = false;
                waterDrop2.enabled = false;
                dirt5.enabled = false;
                dirt6.enabled = false;
            }
            if(waterDropMaskCase.AlphaRatio < 0.02f && bottleSpray2.isOnTruePos)
            {
                waterDropMaskCase.gameObject.SetActive(false);
                GoNextStatus();
            }
        }
        private void CheckStatus10()
        {
            if (towel3.isClicked)
            {
                waterDropMaskCase.enabled = false;
                dirt4.enabled = true;
                waterDrop2.enabled = true;
                dirt5.enabled = false;
                dirt6.enabled = false;
            }
            if (dirt4.AlphaRatio < 0.02f && towel3.isOnTruePos)
            {
                dirt4.gameObject.SetActive(false);
                GoNextStatus();
            }
        }
        private void CheckStatus11()
        {
            foreach (var screw in screws)
            {
                if (!screw.isOut)
                {
                    return;
                }
            }
            canCaseGlassMove = true;
        }
        private void MoveCaseGlass()
        {
            if (canCaseGlassMove)
            {
                caseGlass.transform.position = Vector3.MoveTowards(caseGlass.transform.position, 
                    new Vector3(-10, caseGlass.transform.position.y, caseGlass.transform.position.z), 15f * Time.deltaTime);
                if(Vector3.Distance(caseGlass.transform.position, new Vector3(-10, caseGlass.transform.position.y, caseGlass.transform.position.z)) < 0.02f)
                {
                    canCaseGlassMove=false;
                    GoNextStatus();
                }
            }
        }
        private void ToolSet1Move()
        {
            if (canToolSet1Move)
            {
                toolSet1.transform.position = Vector3.MoveTowards(toolSet1.transform.position, new Vector3(-8, 0, 0), 15f * Time.deltaTime);
                if(Vector3.Distance(toolSet1.transform.position, new Vector3(-8, 0, 0)) < 0.02f)
                {
                    canToolSet1Move=false;
                    toolSet2.SetActive(true);
                    canToolSet2Move = true;
                    toolSet1.SetActive(false);
                }
            }
        }
        private void ToolSet2Move()
        {
            if (canToolSet2Move)
            {
                toolSet2.transform.position = Vector3.MoveTowards(toolSet2.transform.position, new Vector3(0, 0, 0), 15f * Time.deltaTime);
                if (Vector3.Distance(toolSet2.transform.position, new Vector3(0, 0, 0)) < 0.02f)
                {
                    canToolSet2Move = false;
                }
            }
        }
        private void CheckStatus13()
        {
            if (!brush3.isOnTruePos)
            {
                dirt5.enabled = true;
                dirt6.enabled = false;
            }
            if(dirt5.AlphaRatio<0.05 && brush3.isOnTruePos)
            {
                dirt5.gameObject.SetActive(false);
                GoNextStatus();
            }
        }
        private void CheckStatus14()
        {
            if (!vaccum.isOnTruePos)
            {
                dirt5.enabled = false;
                dirt6.enabled = true;
            }
            if (dirt6.AlphaRatio < 0.05 && vaccum.isOnTruePos)
            {
                dirt6.gameObject.SetActive(false);
                canToolSet2MoveOut = true;
            }
        }
        private void ToolSet2MoveOut()
        {
            if (canToolSet2MoveOut)
            {
                toolSet2.transform.position = Vector3.MoveTowards(toolSet2.transform.position, new Vector3(-8, 0, 0), 15f * Time.deltaTime);
                if(Vector3.Distance(toolSet2.transform.position, new Vector3(-8, 0, 0)) < 0.02f)
                {
                    canToolSet2MoveOut = false;
                    toolSet2.SetActive(false);
                    GoNextStatus();
                }
            }
        }
        private void MoveInGlass()
        {
            if (canGlassMoveIn)
            {
                caseGlass.transform.position = Vector3.MoveTowards(caseGlass.transform.position,
                    new Vector3(0, caseGlass.transform.position.y, caseGlass.transform.position.z), 15f * Time.deltaTime);
                if(Vector3.Distance(caseGlass.transform.position,
                    new Vector3(0, caseGlass.transform.position.y, caseGlass.transform.position.z)) < 0.02f)
                {
                    canGlassMoveIn = false;
                    GoNextStatus();
                }
            }
        }

        private bool CheckStatus16()
        {
            foreach(var screw in screws3)
            {
                if (!screw.isOnTruePos)
                {
                    return false;
                }
            }
            return true;
        }
        private void Scene3MoveOut()
        {
            if (scene3MoveOut)
            {
                scene3Objects.transform.position = Vector3.MoveTowards(scene3Objects.transform.position, new Vector3(-10, 0, 0), 15f * Time.deltaTime);
                if(Vector3.Distance(scene3Objects.transform.position, new Vector3(-10, 0, 0)) < 0.02f)
                {
                    scene3MoveOut = false;
                    cleanCaseMoveIn = true;
                    cleanSet.SetActive(true);
                }
            }
        }

        private void CleanCaseMoveIn()
        {
            if (cleanCaseMoveIn)
            {
                cleanSet.transform.position = Vector3.MoveTowards(cleanSet.transform.position, Vector3.zero, 15f * Time.deltaTime);
                if (Vector3.Distance(cleanSet.transform.position, Vector3.zero) < 0.02f)
                {
                    cleanCaseMoveIn = false;
                    GoNextStatus();
                }
            }
        }
    }
}
