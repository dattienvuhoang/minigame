using UnityEngine;

namespace dinhvt
{
    public class Mission : MonoBehaviour, DraggableObject, IRotatableObject, ISelectableObject
    {
        protected Wave wave;
        protected bool isComplete = false;
        protected bool canComplete = false;


        public virtual void Init(Wave parentWave)
        {
            wave = parentWave;
        }

        public virtual void CompleteMission()
        {
            canComplete = false;
            wave.ToggleCollider(this, false);
            wave.ValidateSuccess();
        }

        public virtual void ResetMission()
        {
            isComplete = false;
            canComplete = true;
            wave.DecreaseMissionIndex();
        }

        public virtual void SetCanMissionComplete(bool isCan)
        {
            canComplete = isCan;

            //StackTrace stackTrace = new StackTrace();
            //string caller = stackTrace.GetFrame(1).GetMethod().DeclaringType.Name;

            //UnityEngine.Debug.Log($"Method called by: {caller}");
        }

        public virtual bool GetCanMissionComplete()
        {
            return canComplete;
        }

        public virtual bool GetMissionComplete()
        {
            return isComplete;
        }

        public virtual SpriteRenderer GetSpriteRenderer() { return null; }

        public virtual void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            
        }

        public virtual void StartMission(float duration)
        {

        }

        public virtual void EndMission(float duration)
        {
            
        }

        public virtual void Blink() { }

        public virtual void Select(Vector3 touchPosition)
        {

        }

        public virtual void Deselect(Vector3 touchPosition)
        {

        }

        public virtual void UpdateRotation(Vector3 touchPosition, float offsetAngle)
        {

        }

        public virtual bool CompareTagValue(Collider2D collision, string tag)
        {
            GameTag gameTag = collision.transform.GetComponent<GameTag>();
            if (gameTag != null)
            {
                if (gameTag.tagValue == tag)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
