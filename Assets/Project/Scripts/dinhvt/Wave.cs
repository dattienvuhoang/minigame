using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace dinhvt
{
    public class Wave : MonoBehaviour
    {
        public int _missionIndex;
        public int numOfInitialCompletableMission;
        public List<Mission> missions = new List<Mission>();
        public List<TargetColliders> colliderOfMission = new List<TargetColliders>();
        public float startWaveTime;
        public float endWaveTime;
        public float transitionTime;

        protected WaveManager waveManager;
        protected bool _isComplete;

        [System.Serializable]
        public struct TargetColliders
        {
            public Collider2D[] collider2Ds;
        }

        [System.Serializable]
        public struct MovableObject
        {
            public Transform transform;
            public Vector3 targetPosition;
        }

        public virtual void Start()
        {
            InitWave();
        }

        public virtual void Init(WaveManager parentWaveManager)
        {
            waveManager = parentWaveManager;
        }

        public virtual void InitWave()
        {   
            for (int i = 0; i < missions.Count; i++)
            {
                missions[i]?.Init(this);
            }  
        }

        public virtual void StartWave(float duration)
        {
            foreach (Mission mission in missions)
            {
                mission.StartMission(duration);
            }

            InitCompletableMissions();
        }

        public virtual void InitCompletableMissions()
        {
            for (int i = 0; i < numOfInitialCompletableMission; i++)
            {
                missions[i].SetCanMissionComplete(true);
                ToggleCollider(missions[i], true);
            }
        }

        public virtual void EndWave(float duration) 
        {
            foreach (Mission mission in missions)
            {
                mission.EndMission(duration);
            }
        }

        public virtual void ValidateSuccess()
        {
            _missionIndex++;
            Debug.Log("Mission index: " + _missionIndex);
            if (_missionIndex > missions.Count - 1)
            {
                CompleteAllMission();
                //Debug.Log("Complete Wave: " + name);
            }
            else
            {
                NextMission();
            }
        }

        public virtual void DecreaseMissionIndex()
        {
            _missionIndex--;
        }

        public virtual void NextMission()
        {
            if (_missionIndex >= numOfInitialCompletableMission)
            {
                missions[_missionIndex].SetCanMissionComplete(true);
                ToggleCollider(missions[_missionIndex], true);
            }
        }

        public virtual void ToggleCollider(Mission mission, bool isEnable)
        {   
            int index = missions.IndexOf(mission);
            if (index >= colliderOfMission.Count) return;

            foreach (Collider2D col in colliderOfMission[index].collider2Ds)
            {
                col.enabled = isEnable;
            }
        }

        public virtual void CompleteAllMission()
        {
            CompleteWave();
            waveManager.NextWave();
        }

        public virtual void CompleteWave()
        {
            _isComplete = true;
        }

        public virtual bool GetWaveComplete()
        {
            return _isComplete;
        }

        public virtual int GetMissionIndex()
        {
            return _missionIndex;
        }

        public virtual WaveManager GetWaveManager()
        {
            return waveManager;
        }

        public virtual void UpdateMissionSortingOrder(SpriteRenderer missionSR) { }

        public virtual void AddMissionSpriteRend(SpriteRenderer missionSP) { }

        public virtual void RemoveMissionSpriteRend(SpriteRenderer missionSP) { }

        public virtual void FadeObject(GameObject gameObject, float endValue, float duration)
        {
            if (endValue > 0f) gameObject.SetActive(true);

            gameObject.GetComponent<SpriteRenderer>().DOFade(endValue, duration).OnComplete(() =>
            {
                if (endValue < 1f) gameObject.SetActive(false);
            });
        }

        public virtual void FadeHierarchy(GameObject parent, float endValue, float duration)
        {
            FadeObject(parent, endValue, duration);

            foreach (Transform child in parent.transform)
            {
                GameObject childObject = child.gameObject;
                FadeObject(childObject, endValue, duration);
                if (child.childCount > 0)
                {
                    FadeHierarchy(childObject, endValue, duration);
                }
            }
        }
    }
}

