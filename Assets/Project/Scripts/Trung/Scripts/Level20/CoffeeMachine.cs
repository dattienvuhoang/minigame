using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class CoffeeMachine : MonoBehaviour
    {
        [SerializeField] private GameObject coffeeMachine;
        [SerializeField] private GameObject coffeeTinyCup;

        private void Start()
        {
        }
        private void OnMouseDown()
        {
            coffeeMachine.SetActive(true);
            coffeeTinyCup.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
