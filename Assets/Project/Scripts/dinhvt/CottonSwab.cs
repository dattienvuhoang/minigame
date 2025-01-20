using DG.Tweening;
using UnityEngine;

namespace dinhvt
{
    public class CottonSwab : DestructionTool
    {
        private SpriteRenderer _spriteRend;
        private int medicineSortingOrder;

        [Header("Medicine Bottle")]
        private Vector2 _medicineInitialPos;
        [SerializeField] Transform medicine;
        [SerializeField] Vector2 medicinePos;

        public override void Start()
        {   
            base.Start();

            _spriteRend = GetComponent<SpriteRenderer>();
            _medicineInitialPos = medicine.position;
            medicineSortingOrder = medicine.GetComponent<SpriteRenderer>().sortingOrder;
        }


        public override void StartMission(float duration)
        {   
            base.StartMission(duration);
            medicine.gameObject.SetActive(true);
            medicine.DOMove(medicinePos, duration);
        }

        public override void EndMission(float duration)
        {   
            base.EndMission(duration);
            medicine.DOMove(_medicineInitialPos, duration).OnComplete(() =>
            {
                medicine.gameObject.SetActive(false);
            });
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            _spriteRend.sortingOrder = medicineSortingOrder + 2;
            transform.DOScale(Vector2.one, 0.3f);
        }


        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);
            transform.DOScale(_initialScale, 0.3f).OnComplete(() => 
            { 
                _spriteRend.sortingOrder = medicineSortingOrder - 2; 
            });
        }

        public override void UpdateRotationBasedOnPosition(Vector3 newPosition)
        {
            
        }
    }
}
