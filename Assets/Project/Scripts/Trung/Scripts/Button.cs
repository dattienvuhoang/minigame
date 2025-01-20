using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trung
{
    public class Button : MonoBehaviour
    {
        [SerializeField] private GameObject brush1;
        [SerializeField] private GameObject brush2;
        [SerializeField] private GameObject brush3;
        public static Button instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }
        void Start()
        {
            brush1.SetActive(true);
            brush2.SetActive(false);
            brush3.SetActive(false);
        }
        private void Update()
        {
            ButtonTest();
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
            }
        }

        public void Brush1On()
        {
            brush1.SetActive(true);
            brush2.SetActive(false);
            brush3.SetActive(false);
        }
        public void Brush2On()
        {
            brush2.SetActive(true);
            brush1.SetActive(false);
            brush3.SetActive(false);
        }
        public void Brush3On()
        {
            LayerController.instance.GoToNextLayer();
            brush3.SetActive(true);
            brush1.SetActive(false);
            brush2.SetActive(false);
        }
        private void ButtonTest()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Brush1On();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Brush2On();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Brush3On();
            }
        }
    }
}
    