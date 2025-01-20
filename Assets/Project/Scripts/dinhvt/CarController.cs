using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class CarController : MonoBehaviour
    {
        private Vector3 _zoomOutScale;
        private Vector3 _zoomOutPosition;
        public float zoomInTime;
        [SerializeField] ZoomInData[] zoomInDatas;
        [SerializeField] Wave[] waves;
        [SerializeField] Collider2D frameCollider;
         
        [System.Serializable]
        public struct ZoomInData
        {
            public Mission transform;
            public Vector3 zoomInScale;
            public Vector3 zoomInPosition;
        }

        private void Start()
        {
            _zoomOutScale = transform.localScale;
            _zoomOutPosition = transform.position;
        }

        public void ZoomIn(Mission collisionTool)
        {
            frameCollider.enabled = false;

            foreach (ZoomInData zoomInData in zoomInDatas)
            {
                if (zoomInData.transform == collisionTool)
                {
                    transform.DOScale(zoomInData.zoomInScale, zoomInTime);
                    transform.DOMove(zoomInData.zoomInPosition, zoomInTime);
                }
            }

            foreach (Wave wave in waves)
            {   
                foreach (Mission mission in wave.missions)
                {
                    if (mission != collisionTool) mission.EndMission(zoomInTime);
                }  
            }
        }

        public void ZoomOut(Mission collisionTool)
        {
            transform.DOScale(_zoomOutScale, zoomInTime);
            transform.DOMove(_zoomOutPosition, zoomInTime).OnComplete(() =>
            {
                frameCollider.enabled = true;
            });

            foreach (Wave wave in waves)
            {
                foreach (Mission mission in wave.missions)
                {
                    if (mission != collisionTool) mission.StartMission(zoomInTime);
                }
            }
        }
    }
}
