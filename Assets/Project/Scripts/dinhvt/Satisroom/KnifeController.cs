using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace dinhvt
{
    public class KnifeController : ToolController
    {
        [SerializeField] Sprite selectSprite;
        [SerializeField] Sprite sharpenedSprite;
        [SerializeField] Sprite unsharpenedSprite;
        [Space(20)]
        [SerializeField] SpriteRenderer knifeGrindstone;
        [SerializeField] SpriteRenderer board;

        private Animator _animator;
        [SerializeField]private VegetablesController _vegetableInBoard;
        private bool _isSharpened;
        private Sprite _deselectSprite;

        public override void Awake()
        {
            base.Awake();

            _animator = GetComponent<Animator>();
        }

        public override void Start()
        {
            base.Start();

            _deselectSprite = unsharpenedSprite;
        }

        public override void Select(Vector3 touchPosition)
        {
            base.Select(touchPosition);

            _animator.enabled = false;
            _spriteRenderer.sprite = selectSprite;
            _spriteRenderer.sortingOrder += 2;
            transform.position = touchPosition;
        }

        public override void Deselect(Vector3 touchPosition)
        {
            base.Deselect(touchPosition);

            if (board.bounds.Contains(touchPosition) && _vegetableInBoard != null && _isSharpened)
            {
                CutVegetable();
            }
            else if (knifeGrindstone.bounds.Contains(touchPosition) && !_isSharpened)
            {
                Sharpened();
            }
            else
            {
                ResetTransform();
            }
        }

        
        public override void UpdatePosition(Vector3 touchPosition, Vector3 offset)
        {
            base.UpdatePosition(touchPosition, offset);

            transform.position = touchPosition + offset;
        }

        public void UpdateVegetableInBoard(VegetablesController vegetable)
        {       
            _vegetableInBoard = vegetable;
        }

        private void Sharpened()
        {
            _isSharpened = true;
            float minY = knifeGrindstone.transform.position.y - 0.5f;
            float maxY = knifeGrindstone.transform.position.y + 0.5f;

            StartCoroutine(PlaySharpenedAnim(minY, maxY));
        }

        private void CutVegetable()
        {
            float minX = _vegetableInBoard.GetSpriteRenderer().bounds.min.x;
            float maxX = _vegetableInBoard.GetSpriteRenderer().bounds.max.x;
            float minY = _vegetableInBoard.GetSpriteRenderer().bounds.min.y;
            float maxY = _vegetableInBoard.GetSpriteRenderer().bounds.max.y;

            Vector3 startPos = new Vector3(minX, (minY + maxY) / 2f, 0f);
            Vector3 endPos = new Vector3(maxX, (minY + maxY) / 2f, 0f);

            StartCoroutine(PlayCutAnim(startPos, endPos));
        }


        private void ResetTransform()
        {
            _spriteRenderer.sprite = _deselectSprite;
            _spriteRenderer.sortingOrder -= 2;
            transform.DOMove(_initialPosition, moveTime);
        }

        IEnumerator PlayCutAnim(Vector3 startPos, Vector3 endPos)
        {
            _collider.enabled = false;
            _vegetableInBoard.DisableCollider();
            transform.DOMove(startPos, 0.2f);
            yield return new WaitForSeconds(0.2f);

            _animator.enabled = true;
            _animator.SetBool("IsCut", true);
            transform.DOMove(endPos, 2f);
            yield return new WaitForSeconds(2f);

            _animator.SetBool("IsCut", false);
            _animator.enabled = false;

            _vegetableInBoard.OnCut();
            _vegetableInBoard = null;
            _collider.enabled = true;

            ResetTransform();
        }

        IEnumerator PlaySharpenedAnim(float minY, float maxY)
        {
            _collider.enabled = false;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOMove(knifeGrindstone.transform.position, 0.25f)); 
            sequence.Append(transform.DOMoveY(minY, 0.25f)); 
            sequence.Append(transform.DOMoveY(maxY, 0.5f)); 
            sequence.Append(transform.DOMoveY(minY, 0.5f)); 
            sequence.Append(transform.DOMoveY(maxY, 0.5f)); 
            sequence.Append(transform.DOMoveY(minY, 0.5f));
            sequence.Append(transform.DOMove(knifeGrindstone.transform.position, 0.25f));

            sequence.Play();

            yield return new WaitForSeconds(sequence.Duration());
            _deselectSprite = sharpenedSprite;
            _collider.enabled = true;
            ResetTransform();
        }
    }
}
