using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Soil2 : MonoBehaviour
    {
        public const float originalRGB = 1;
        public const float waterRGB = 0.7f;
        public bool isPlanted { get; private set; }
        [SerializeField] private GameObject plant;
        [SerializeField] private GameObject seed;

        private float curRGB;
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D col;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            col = GetComponent<BoxCollider2D>();
        }
        private void Start()
        {
            isPlanted = false;
            plant.SetActive(false);
            curRGB = originalRGB;
        }
        private void Update()
        {
            spriteRenderer.color = new Color(curRGB, curRGB, curRGB);
            if (curRGB <= waterRGB)
            {
                col.enabled = false;
                plant.SetActive(true);
                FeelingTool.instance.FadeOutImplement(seed, 0.3f);
                isPlanted = true;
            }
            //if (Input.GetKey(KeyCode.Escape))
            //{
            //    spriteRenderer.color = new Color(curRGB, curRGB, curRGB);
            //}
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            TagController tag = collision.gameObject.GetComponent<TagController>();
            if(tag != null)
            {
                if (tag.tag == "water")
                {
                    Debug.Log("tuoi nuoc");
                    curRGB -= Time.deltaTime / 4;
                }
            }
        }
    }
}
