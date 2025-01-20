using SR4BlackDev;
using System.Collections;
using UnityEngine;

namespace dinhvt
{
    public class FoldingTool : CleaningTool
    {
        [Header("Folding Tool")]
        [SerializeField] int numOfTarget;   
        [SerializeField] Sprite foldingSprite;

        private int _targetCount;
        private Sprite _initialSprite;
        private SpriteRenderer _spriteRend;

        private void Awake()
        {   
            _spriteRend = GetComponent<SpriteRenderer>();
            _initialSprite = _spriteRend.sprite;    
        }

        public void Folding()
        {
            _targetCount++;
            StartCoroutine(UpdateFoldingSprite());

            if (_targetCount == numOfTarget)
            {
                isComplete = true;
            }
        }

        IEnumerator UpdateFoldingSprite()
        {
            _spriteRend.sprite = foldingSprite;

            yield return new WaitForSeconds(0.2f);

            _spriteRend.sprite = _initialSprite;
        }
    }
}
