using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class DrawerContent : FunitureController
    {
        private DrawerController _drawerController;

        [SerializeField] LayerMask targetLayer;
        [SerializeField] float scaleTime;
         
        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            RaycastHit2D hitInfo = Physics2D.CircleCast(transform.position, 0.1f, Vector2.zero, Mathf.Infinity, targetLayer);

            if (hitInfo && hitInfo.transform != null)
            {
                if (hitInfo.transform.TryGetComponent<DrawerController>(out _drawerController)) 
                {
                    if (_drawerController.id == id && _drawerController._isOpen)
                    {
                        transform.DOScale(Vector3.zero, scaleTime).OnComplete(() =>
                        {
                            _drawerController.IncreaseCount();
                        });
                    }
                }
            }
        }
    }
}
