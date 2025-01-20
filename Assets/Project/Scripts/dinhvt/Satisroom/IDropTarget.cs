using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dinhvt
{
    public interface IDropTarget
    {
        public void Dragged(Transform dragger);
        public void Dropped(Transform dropper);  
    }
}
