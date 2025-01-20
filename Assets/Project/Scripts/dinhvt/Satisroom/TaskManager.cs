using SR4BlackDev.UISystem;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class TaskManager : MonoBehaviour
    {       
        public List<SubTask> subTasks = new List<SubTask>();
        public int _completeTaskCount;

        private void Start()
        {
            InitTask();
        }

        public void InitTask()
        {
            foreach (SubTask subTask in subTasks)
            {
                subTask?.Init(this);

                if (subTask.GetIsComplete()) _completeTaskCount++;
            }
        }

        public void CheckComplete()
        {
            if (_completeTaskCount == subTasks.Count)
            {
                Debug.Log("WIN");
                PopupManager.ShowToast("WIN");
            }
        }

        public void UpdateTaskCount(bool _isComplete)
        {   
            if (_isComplete)
            {
                _completeTaskCount++;
                CheckComplete();
            }
            else
            {
                _completeTaskCount--;
            }
        }
    }
}
