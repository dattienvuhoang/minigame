using UnityEngine;

namespace dinhvt
{
    public class NoseHair : MonoBehaviour
    {
        public int numOfCollision;
        [SerializeField] FoldingTool scissors;
        [SerializeField] float timeThreshold;

        private int _collisionCount = 0;
        private float _timer = 0f;
        private Rigidbody2D _rb;
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!scissors.GetCanMissionComplete()) return;

            if (collision.transform == scissors.transform)
            {
                _collisionCount++;

                CheckAndFoldScissors();
            }
        }


        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!scissors.GetCanMissionComplete()) return;

            if (collision.transform == scissors.transform)
            {
                _timer += Time.deltaTime;

                if (_timer > timeThreshold)
                {
                    _timer = 0f;
                    _collisionCount++;
                }

                CheckAndFoldScissors();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _timer = 0f;
        }

        private void CheckAndFoldScissors()
        {
            if (_collisionCount == numOfCollision)
            {
                scissors.Folding();
                _boxCollider2D.enabled = false;
                _rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
