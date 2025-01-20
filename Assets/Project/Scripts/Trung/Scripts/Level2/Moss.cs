using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Moss : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private bool canMove;
        private Vector3 pos;
        private bool isClear;
        [SerializeField] private List<Sprite> squareMosses;
        private GameObject racket;
        private BoxCollider2D col;
        private Level2Controller level2Controller;
        private Level2Controller Level2Controller
        {
            get
            {
                if (level2Controller == null)
                {
                    level2Controller = GameObject.Find("GameController").GetComponent<Level2Controller>();
                }
                return level2Controller;
            }
            set
            {
                level2Controller = value;
            }
        }
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            col = GetComponent<BoxCollider2D>();
        }
        private void Start()
        {
            isClear = false;
            canMove = false;
        }
        private void Update()
        {
            MoveToward();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagController tag = collision.gameObject.GetComponent<TagController>();
            if(tag != null)
            {
                if (tag.tag == "racket" && !isClear)
                {
                    racket = collision.gameObject;
                    racket.GetComponent<Animator>().enabled = true;
                    spriteRenderer.sprite = squareMosses[Random.Range(0, squareMosses.Count)];
                    gameObject.transform.localScale = Vector3.one * 0.6f;
                    pos = racket.transform.position + new Vector3(0, 0.1f, 0);
                    col.enabled = false;
                    canMove = true;
                    gameObject.transform.SetParent(racket.transform);
                    isClear = true;
                    Level2Controller.CleanMoss();
                }
            }
        }
        private void MoveToward()
        {
            if (canMove)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, racket.transform.position + new Vector3(0, 1.1f, -2), 10f*Time.deltaTime);
                if(Vector3.Distance(transform.position, racket.transform.position + new Vector3(0, 1.1f, -2)) < 0.1f)
                {
                    canMove = false;
                }
            }
        }
    }
}
