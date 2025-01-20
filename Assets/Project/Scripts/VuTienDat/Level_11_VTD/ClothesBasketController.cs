using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuTienDat
{
    public class ClothesBasketController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> listItem;
        [SerializeField] private float posY = -1f;
        [SerializeField] private BoxCollider2D box;
        private float posZ = 0;
        public static ClothesBasketController instance;
        
        private void Awake()
        {
            instance = this;
        }
        private void Update()
        {
            if (listItem.Count == 0)
            {
                box.enabled = false;
                Destroy(gameObject,0.12f);
            }
        }
        public void PushItem()
        {
            int index = Random.Range(0, listItem.Count-1);
            if (listItem[index].GetComponent<Item_Level_10>().idType == 2)
            {
                posY = 3.5f;
            }
            else
            {
                posY = -1;
            }
            posZ -= 0.0001f;
            listItem[index].SetActive(true);
            listItem[index].transform.DOMove(new Vector3(0,posY,posZ), 0.2f);
            listItem.Remove(listItem[index]);
        }
    }
}
