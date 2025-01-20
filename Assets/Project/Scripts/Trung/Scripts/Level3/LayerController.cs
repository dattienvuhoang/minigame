using PaintCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trung
{

    public class LayerController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> paintAbleObjects;
        [SerializeField] private GameObject p3DMask;
        [SerializeField] private GameObject fewDirtyObject;
        [SerializeField] private List<UseableOject> useableObjects;
        [SerializeField] private GameObject brushes;
        [SerializeField] private GameObject cleanBrush;

        private CwChannelCounter paintCounter;
        private CwChannelCounter paintCounterDirty;

        private int curBrushIndex;

        public static LayerController instance;
        private int curLayerIndex;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }

            paintCounter = p3DMask.GetComponent<CwChannelCounter>();
            paintCounterDirty = fewDirtyObject.GetComponent<CwChannelCounter>();
        }
        private void Start()
        {
            curLayerIndex = 0;
            curBrushIndex = 0;
            p3DMask.SetActive(false);
            paintAbleObjects[curLayerIndex].SetActive(true);
            for (int i = 1; i < paintAbleObjects.Count; i++)
            {
                paintAbleObjects[i].SetActive(false);
            }
        }

        private void Update()
        {
            CheckPaintCounter();
        }
        public void GoToNextLayer()
        {
            if (curLayerIndex < paintAbleObjects.Count - 1)
            {
                paintAbleObjects[curLayerIndex].SetActive(false);
                paintAbleObjects[curLayerIndex + 1].SetActive(true);
                curLayerIndex++;
            }
        }
        public void SetP3DMask(bool status)
        {
            p3DMask.SetActive(status);
        }

        private void CheckPaintCounter()
        {
            if (p3DMask.activeSelf)
            {
                CheckTypeOfBrush();
                if (curBrushIndex == 0 && paintCounter.RatioG > 0.9f && paintCounter.RatioA > 0.93f && !useableObjects[curBrushIndex].isClicked)
                {
                    curBrushIndex = 1;
                    Button.instance.Brush2On();
                }
                if (curBrushIndex == 1 && paintCounter.RatioG < 0.004f && paintCounter.RatioA > 0.93f && !useableObjects[curBrushIndex].isClicked)
                {
                    curBrushIndex = 2;
                    Button.instance.Brush3On();
                }
                if (curBrushIndex == 2 && paintCounter.RatioG < 0.004f && paintCounter.RatioA < 0.004f && !useableObjects[curBrushIndex].isClicked)
                {
                    curBrushIndex = 0;
                    Button.instance.Brush1On();
                }
            }
            CheckFewDirtyObject();
        }
        private void CheckFewDirtyObject()
        {

            if (!p3DMask.activeSelf && useableObjects[2].isClicked)
            {
                cleanBrush.SetActive(true);
            }
            else
            {
                cleanBrush.SetActive(false);
            }

            if (paintCounterDirty.RatioA > 0.98 && !useableObjects[2].isClicked)
            {
                cleanBrush.SetActive(false);
                p3DMask.SetActive(true);
                brushes.SetActive(true);
            }

        }
        private void CheckTypeOfBrush()
        {
            if (useableObjects[curBrushIndex].isClicked)
            {
                brushes.SetActive(true);
            }
            else
            {
                brushes.SetActive(false);
            }
        }
    }
}