using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class IceMachine : MonoBehaviour
    {
        [SerializeField] private List<GameObject> ices;
        private float cd;
        private int curIce;
        private bool canDrop;
        void Start()
        {
            canDrop = false;
            curIce = 0;
            cd = 0.1f;
        }

        private void Update()
        {
            if(canDrop)
            {
                StartDrop();
            }
        }
        private void StartDrop()
        {
            cd += Time.deltaTime;
            if(curIce < ices.Count && cd >= 0.1f)
            {
                ices[curIce].SetActive(true);
                curIce++;
                cd = 0;
            }
            else if(curIce >= ices.Count)
            {
                canDrop = false;
            }
        }

        private void OnMouseDown()
        {
            canDrop = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
