using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public class LargePieceOfFlour : MonoBehaviour
    {
        [SerializeField] List<PieceOfFlour> piecesOfFlour = new List<PieceOfFlour>();
        [SerializeField] float moveTime;

        [System.Serializable]
        public struct PieceOfFlour
        {
            public Transform transform;
            public Vector3 position;
            public Sprite RemainingFlour;
        }

        private int _pieceIndex = -1;
        private PieceOfFlour _currentPiece;
        private SpriteRenderer _spriteRend;

        private void Awake()
        {
            _spriteRend = GetComponent<SpriteRenderer>();
        }

        public void DivideIntoPieces()
        {
            if (_pieceIndex > piecesOfFlour.Count - 1) return;

            _pieceIndex++;
            _currentPiece = piecesOfFlour[_pieceIndex];
            _currentPiece.transform.gameObject.SetActive(true);
            _currentPiece.transform.DOMove(_currentPiece.position, moveTime);
            _spriteRend.sprite = _currentPiece.RemainingFlour;
        }

        public bool IsDivideComplete()
        {
            return _pieceIndex == piecesOfFlour.Count - 1;
        }
    }
}
