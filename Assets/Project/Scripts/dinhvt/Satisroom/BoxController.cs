using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class BoxController : Mission
    {
        [SerializeField] Vector3 seclectScale;
        [Space(20)]
        [SerializeField] Transform[] items;
        [SerializeField] float moveTime;
        [SerializeField] Transform targetTranform;
        [Space(20)]
        [SerializeField] ArrangeItemWave arrangeItemWave;


        private int _itemIdx;

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            transform.DOScale(seclectScale, 0.3f);
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            transform.DOScale(Vector3.one, 0.3f);
            TakeItem();
        }

        private void TakeItem()
        {
            Transform item = items[_itemIdx];

            arrangeItemWave.AddMissionSpriteRend(item.GetComponent<SpriteRenderer>());

            item.gameObject.SetActive(true);
            item.eulerAngles = Vector3.one;
            item.DOMove(targetTranform.position, moveTime);
            _itemIdx++;

            if (_itemIdx >= items.Length) gameObject.SetActive(false);
        }
    }
}
