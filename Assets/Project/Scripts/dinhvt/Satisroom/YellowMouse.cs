using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class YellowMouse : AnimalController
    {
        [SerializeField] Animator haystackShook;

        private bool _isFirstTouched;
        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            if (!_isFirstTouched)
            {   
                _isFirstTouched = true;
                _initialSortingOrder = 4;
                _spriteRenderer.sortingOrder = _initialSortingOrder;
                haystackShook.SetTrigger("Stop");
            }

            UpdateSortingOrder(_selectSortingOrder);
            if (_isComplete)
            {
                _isComplete = false;
                _taskManager.UpdateTaskCount(_isComplete);
            }
        }
    }
}
