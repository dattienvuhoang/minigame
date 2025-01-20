using UnityEngine;
namespace VuTienDat
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private string nameItem;
        public Sprite item, itemFold;
        public Vector3 rotation;

        
        private void Start()
        {
            transform.eulerAngles = rotation;
        }

        public int getID() { return id; }
    }
}
