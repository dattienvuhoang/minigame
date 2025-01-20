using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trung
{
    public class UIButtons : MonoBehaviour
    {
        public void ReplayButton()
        {
            Debug.Log(SceneManager.GetActiveScene().ToString());
            //SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        }
        public void BackToMenu()
        {
            SceneManager.LoadScene("Menu");
        }
        public void PlayLevelButton()
        {
            if (gameObject != null)
            {
                SceneManager.LoadScene(gameObject.name);
            }
        }
    }
}
