using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class Level20Button : MonoBehaviour
    {
        private RequestPaper requestPaper;
        private RequestPaper RequestPaper
        {
            get
            {
                if(requestPaper == null)
                {
                    requestPaper = GameObject.Find("requestPaper").GetComponent<RequestPaper>();
                }
                return requestPaper;
            }
            set
            {
                requestPaper = value;
            }   
        }
        public void XOnClick()
        {
            RequestPaper.MoveInClick();
        }
    }
}
