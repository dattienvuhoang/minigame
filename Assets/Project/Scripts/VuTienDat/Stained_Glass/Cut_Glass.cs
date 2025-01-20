using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class Cut_Glass : MonoBehaviour
    {
        [SerializeField] private GameObject glass;
        [SerializeField] private bool isFall = false;
        [SerializeField] private List<GameObject> listPos;
        [SerializeField] private Rigidbody2D rb;
        private Vector3 posTool; 
        private void Update()
        {
            posTool = DragController_Stained_Glass.instance.getPosTool();
            for (int i = 0; i < listPos.Count; i++)
            {
                if (Vector3.Distance(posTool, listPos[i].transform.position)<0.15f)

                {
                    listPos.Remove(listPos[i]);
                    //Debug.Log("Remove Pos");
                }
            }
            if (!isFall && listPos.Count == 0)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                if (transform.position.y < -10)
                {
                    this.gameObject.SetActive(false);
                    GameManager_StainedGlass.instance.RemoveGlass(this.gameObject);
                    isFall = true;
                }
            }
        }
    }
}
