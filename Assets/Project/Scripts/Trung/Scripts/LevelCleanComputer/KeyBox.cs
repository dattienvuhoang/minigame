using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class KeyBox : MonoBehaviour
    {
        private bool canOut;
        private bool canIn;
        [SerializeField] private GameObject glass;

        private void Start()
        {
            canOut = false;
            canIn = false;
            glass.SetActive(false);
        }
        private void Update()
        {
            GoOut();
            GoInt();
        }
        public void SetOut()
        {
            glass.SetActive(true);
            canOut = true;
        }
        public void SetIn()
        {
            gameObject.SetActive(true);
            canIn = true;
        }
        private void GoOut()
        {
            if(canOut)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 10, 0), 15f * Time.deltaTime);
                if (Vector3.Distance(transform.position, new Vector3(0, 10, 0)) < 0.02f)
                {
                    canOut = false;
                }
            }
        }
        private void GoInt()
        {
            if (canIn)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 2.5f, 0), 15f * Time.deltaTime);
                if (Vector3.Distance(transform.position, new Vector3(0, 2.5f, 0)) < 0.02f)
                {
                    canIn = false;
                    glass.SetActive(false);
                }
            }
        }
    }
}
