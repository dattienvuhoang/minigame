using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class KitchenDoor : MonoBehaviour
    {
        private Animator animator;
        private Vector3 oriPos;
        private Vector3 pos;
        [SerializeField] private GameObject openDoor;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        private void Start()
        {
            openDoor.SetActive(false);
            oriPos = new Vector3(0.711f, 2.647f, 0);
            pos = new Vector3(-2.47f, 2.647f, 0f);
        }
        private void OnMouseDown()
        {
            animator.enabled = true;
        }

        public void SetNewPos()
        {
                transform.position = pos;
                openDoor.SetActive(true);
                gameObject.SetActive(false);
        }
    }
}
