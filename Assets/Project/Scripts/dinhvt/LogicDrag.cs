using SR4BlackDev;
using SR4BlackDev.UISystem;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace dinhvt
{
    public class LogicDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public static event Action<Vector3> OnEventTouch;
        public static event Action<Vector3> OnEventDrag;
        public static event Action<Vector3> OnEventRelease;

        public bool isPopup = true;
        public bool popupHealth = false;
        public bool canDrag = true;

        [SerializeField] protected LayerMask dragLayer;
        [SerializeField] protected float radius = 0.3f;
        protected Camera mainCamera;
        protected Vector3 touchPosition;
        protected Vector3 offset;
        protected float angleOffset;
        protected RaycastHit2D[] hitInfo;
        protected Transform hitTransform;
        protected DraggableObject draggableObject;
        protected IRotatableObject rotatableObject;
        protected ISelectableObject selectableObject;

        public virtual void Awake()
        {
            this.RegisterListener(EventID.OnToggleDragAbility, ToggleDragAbility);
        }

        protected virtual void Start()
        {   
            Application.targetFrameRate = 60;
            mainCamera = Camera.main;

            if (isPopup)
            {
                if (popupHealth)
                {
                    PopupManager.Open(PopupPath.POPUP_LEVEL_3, LayerPopup.Main);
                }
                else
                {
                    PopupManager.Open(PopupPath.POPUP_NO_HEALTH, LayerPopup.Main);

                }
            }
        }

        private void OnDestroy()
        {
            this.RemoveListener(EventID.OnToggleDragAbility, ToggleDragAbility);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (!canDrag) return;
            UpdateTouchPosition(ref touchPosition);
            OnEventTouch?.Invoke(touchPosition);
            SelectObject();
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (!canDrag) return;
            UpdateTouchPosition(ref touchPosition);
            OnEventDrag?.Invoke(touchPosition);
            if (hitTransform != null) MoveObject();
        }
        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (hitTransform != null) ReleaseObject();
            if (!canDrag) return;
            UpdateTouchPosition(ref touchPosition);
            OnEventRelease?.Invoke(touchPosition);
        }

        protected virtual void SelectObject()
        {
            UpdateTouchPosition(ref touchPosition);
            hitInfo = Physics2D.CircleCastAll(touchPosition, radius, Vector2.zero, Mathf.Infinity, dragLayer);

            int hitSortingOrder, maxSortingOrder = 0;
            SpriteRenderer spriteRenderer;
            if (hitInfo.Length != 0) hitTransform = hitInfo[0].transform;

            foreach (var hit in hitInfo)
            {
                if (hit.transform.TryGetComponent<SpriteRenderer>(out spriteRenderer))
                {
                    hitSortingOrder = spriteRenderer.sortingOrder;
                    if (hitSortingOrder >= maxSortingOrder)
                    {
                        hitTransform = hit.transform;
                        maxSortingOrder = hitSortingOrder;
                    }
                }
            }

            if (hitTransform != null)
            {
                //Debug.Log("Hit Transform Name: " + hitTransform.name);
                if (hitTransform.TryGetComponent<DraggableObject>(out draggableObject))
                {
                    offset = hitTransform.position - touchPosition;
                }

                if (hitTransform.TryGetComponent<IRotatableObject>(out rotatableObject))
                {
                    offset = hitTransform.position - touchPosition;
                    angleOffset = (Mathf.Atan2(hitTransform.right.y, hitTransform.right.x) - Mathf.Atan2(offset.y, offset.x)) * Mathf.Rad2Deg;
                }

                if (hitTransform.TryGetComponent<ISelectableObject>(out selectableObject))
                {
                    selectableObject.Select(touchPosition);
                }
            }
        }
        protected virtual void MoveObject()
        {
            UpdateTouchPosition(ref touchPosition);
            //Vector3 targetPos = touchPosition + offset;
            draggableObject?.UpdatePosition(touchPosition, offset);
            rotatableObject?.UpdateRotation(touchPosition, angleOffset);
        }

        protected virtual void ReleaseObject()
        {
            hitTransform = null;

            if (selectableObject != null)
            {
                UpdateTouchPosition(ref touchPosition);
                selectableObject?.Deselect(touchPosition);
                selectableObject = null;
            }

            if (draggableObject != null) draggableObject = null;
            if (rotatableObject != null) rotatableObject = null;
        }

        protected virtual void ToggleDragAbility(object sender, object result)
        {
            canDrag = (bool)result;
        }

        protected void UpdateTouchPosition(ref Vector3 touchPosition)
        {
            if (
#if UNITY_EDITOR
                true
#else
                Input.touchCount >= 1
#endif
                )
            {
#if UNITY_EDITOR
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#else                  
                touchPosition =  Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
            }

            touchPosition.z = 0f;
        }
    }
}

