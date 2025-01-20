using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

namespace Trung
{
    public class Tool1 : MonoBehaviour
    {
        [SerializeField] private Transform tool;
        [SerializeField] int leavesAmount = 18;
        private List<GameObject> leaves;
        private void Start()
        {
            leaves = new List<GameObject>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TagController tag = collision.gameObject.GetComponent<TagController>(); 
            if(tag != null)
            {
                if (tag.tag == "leaf")
                {
                    leaves.Add(collision.gameObject);
                    collision.gameObject.transform.SetParent(tool);
                    if (leaves.Count == leavesAmount)
                    {
                        FadeLeaf();
                        if (LevelGardenController.instance != null)
                        {
                            LevelGardenController.instance.SetStatus1Condition();
                        }
                        if (LevelCleanComputerController.instance != null)
                        {
                            LevelCleanComputerController.instance.GoNextStatus();
                        }
                    }
                }
            }
        }
        private void FadeLeaf()
        {
            foreach (GameObject leaf in leaves)
            {
                FeelingTool.instance.FadeOutImplement(leaf);
            }
        }
    }
}
