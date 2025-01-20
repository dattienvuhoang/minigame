
using UnityEngine;

namespace dinhvt
{
    public interface ISelectableObject 
    {
        public void Select(Vector3 touchPosition);
        public void Deselect(Vector3 touchPosition);
    }
}
