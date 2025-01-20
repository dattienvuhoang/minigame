using UnityEngine;

namespace VuTienDat_Game2
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private string nameItem;
        public Sprite item, itemFold;
        public Vector3 rotation;
        public ItemPosition position;
        public float scale;

        private void Start()
        {
            transform.eulerAngles = rotation;
            scale = transform.GetChild(0).transform.localScale.x;
        }

        public int getID() { return id; }

    }
}
