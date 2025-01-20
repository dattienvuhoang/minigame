using DG.Tweening;
using SR4BlackDev;
using UnityEngine;

namespace dinhvt
{   
    public class SuctionTool : CleaningTool
    {
        private int count;

        [Header("Suction Tool Settings")]
        [SerializeField] int numOfDusty;
        [SerializeField] float suctionTime;
        [SerializeField] Transform cleanHolder;
        [SerializeField] GameObject suctionObject;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (canComplete)
            {
                if (CompareTagValue(collision, "Debris"))
                {
                    count++;
                    collision.GetComponent<Collider2D>().enabled = false;
                    collision.transform.DOMove(cleanHolder.position, suctionTime).OnComplete(() =>
                    {
                        collision.gameObject.SetActive(false);
                    });

                    if (count == numOfDusty) isComplete = true;
                }
            }
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            suctionObject.SetActive(true);
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            suctionObject.SetActive(false);
        }
    }
}
