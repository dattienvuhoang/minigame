using SR4BlackDev;
using SR4BlackDev.UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace dinhvt
{
    public class HeathBarController : MonoBehaviour
    {
        [SerializeField] private int activeChildCount;
        [SerializeField] LogicDrag logicDrag;

        private void Awake()
        {
            this.RegisterListener(EventID.OnMissionResult, OnMissionResult);
        }
        private void OnDestroy()
        {
            this.RemoveListener(EventID.OnMissionResult, OnMissionResult);
        }
        private void Start()
        {
            activeChildCount = transform.childCount;
        }

        public void OnMissionResult(object sender, object result)
        {
            if ((bool)result) return;

            DecreaseHeart();

            if (activeChildCount == 0)
            {
                this.PostEvent(EventID.Log, "LOSE");
                logicDrag.canDrag = false;

                PopupManager.ShowToast("LOSE");
                return;
            }
        }

        private void DecreaseHeart()
        {
            GameObject heart = transform.GetChild(activeChildCount - 1).gameObject;
            heart.SetActive(false);
            activeChildCount--;
        }

        public void ResetHealth()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            activeChildCount = 5;
        }
    }
}
