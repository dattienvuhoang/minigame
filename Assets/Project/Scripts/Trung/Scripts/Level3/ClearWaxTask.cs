using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class ClearWaxTask : MonoBehaviour
    {
        public static ClearWaxTask instance;
        public int done;
        private List<SpriteRenderer> papers; 
        private void Awake()
        {
            if(instance != null && instance != this) 
            {
                Destroy(this);
            }
            else { instance = this; }

            papers = new List<SpriteRenderer>();
            for(int i = 0; i < transform.childCount; i++)
            {
                papers.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
            }
        }
        private void Start()
        {
            done = 0;
        }
        public void CheckWaxPaper(float y, float x)
        {
            if(LayerController2D.instance.curLeg == "left")
            {
                if (x > -1.2f && x < -0.3f)
                {
                    if (y > -1.6 && y <= -0.4 && papers[0] != null)
                    {
                        //1
                        if (!papers[0].enabled)
                        {
                            papers[0].enabled = true;
                            done++;
                        }
                    }
                    else if (y > -0.4 && y <= 0.9 && papers[1] != null)
                    {
                        if (!papers[1].enabled)
                        {
                            papers[1].enabled = true;
                            done++;
                        }
                    }
                    else if (y > 0.9 && y < 1.9 && papers[2] != null)
                    {
                        if (!papers[2].enabled)
                        {
                            papers[2].enabled = true;
                            done++;
                        }
                    }
                    else if (y >= 1.9 && y <= 3 && papers[3] != null)
                    {
                        if (!papers[3].enabled)
                        {
                            papers[3].enabled = true;
                            done++;
                        }
                    }
                    else if (y > 3 && y < 4.2 && papers[4] != null)
                    {
                        if (!papers[4].enabled)
                        {
                            papers[4].enabled = true;
                            done++;
                        }
                    }
                }            
            }
        }
    }
}
