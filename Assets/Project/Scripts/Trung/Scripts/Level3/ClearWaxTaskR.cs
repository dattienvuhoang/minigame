using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{
    public class ClearWaxTaskR : MonoBehaviour
    {
        public static ClearWaxTaskR instance;
        public int done;
        private List<SpriteRenderer> papers;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else { instance = this; }

            papers = new List<SpriteRenderer>();
            for (int i = 0; i < transform.childCount; i++)
            {
                papers.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
            }
        }
        private void Start()
        {
            done = 0;
        }
        public void CheckWaxPaperR(float y, float x)
        {
            if (LayerController2D.instance.curLeg == "right")
            {
                if (x > 0.2f && x < 1f)
                {
                    if (y > -1.4 && y <= -0.12 && papers[0] != null)
                    {
                        //1
                        if (!papers[0].enabled)
                        {
                            papers[0].enabled = true;
                            done++;
                        }
                    }
                    else if (y > -0.12 && y <= 0.9 && papers[1] != null)
                    {
                        if (!papers[1].enabled)
                        {
                            papers[1].enabled = true;
                            done++;
                        }
                    }
                    else if (y > 0.9 && y < 1.85 && papers[2] != null)
                    {
                        if (!papers[2].enabled)
                        {
                            papers[2].enabled = true;
                            done++;
                        }
                    }
                    else if (y >= 1.85 && y <= 3.15 && papers[3] != null)
                    {
                        if (!papers[3].enabled)
                        {
                            papers[3].enabled = true;
                            done++;
                        }
                    }
                    else if (y > 3.15 && y < 4.4 && papers[4] != null)
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
