using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Glass_1 : MonoBehaviour
    {
        public Animator anim;
        public BoxCollider2D box;
        public SpriteRenderer spGlass_2, spGlass_2_Lip;
        public GameObject glass_2; 
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagGameObject tag = collision.gameObject.GetComponent<TagGameObject>();
            if (tag != null)
            {
                if (tag.tagValue == "Glass_2")
                {
                    DragController_Level_40.ins.itemParent = null;
                    this.gameObject.transform.DOScale(1, 0.3f);
                    this.gameObject.transform.DOMove(new Vector3(1.914f, 0.518f, 0), 0.3f).OnComplete(() =>
                    {
                        anim.enabled = true;
                        box.enabled = false;
                        StartCoroutine(ChangeSprite());
                    });
                }
            }
        }
        IEnumerator ChangeSprite()
        {
            yield return new WaitForSeconds(2f);
            spGlass_2.DOFade(0, 1f);
            spGlass_2_Lip.DOFade(1, 1f).OnComplete(() =>
            {
                glass_2.layer = 6;
            });
            
        }
    }
}
