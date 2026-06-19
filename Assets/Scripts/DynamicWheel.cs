using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DynamicWheel : MonoBehaviour
{
    [SerializeField] private GameObject slicePrefab;
    [SerializeField] private Transform slicesContainer;

    [SerializeField] private float sliceWidth = 1f;
    [SerializeField] private float sliceHeight = 1f;

    public List<SliceData> wheelSlices = new List<SliceData>();

    public void GenerateWheel()
    {
        foreach (Transform child in slicesContainer)
        {
            Destroy(child.gameObject);
        }

        if (wheelSlices.Count == 0 || slicePrefab == null)
        {
            return;
        }

        float totalWeight = 0f;
        foreach (var sliceData in wheelSlices)
        {
            if (sliceData != null)
            {
                totalWeight += sliceData.SliceWeight;
            }
        }

        float accumulatedZRotation = 0f;

        for (int i = 0; i < wheelSlices.Count; i++)
        {
            SliceData data = wheelSlices[i];
            if (data == null)
            {
                continue;
            }

            GameObject newSliceObj = Instantiate(slicePrefab, slicesContainer);
            SetupRectTransform(newSliceObj);

            SliceBehaviour behaviour = newSliceObj.GetComponent<SliceBehaviour>();

            if (behaviour != null)
            {
                behaviour.sliceData = data;
                behaviour.currentSlicePoints = data.SlicePoints;
                behaviour.currentSliceColor = data.SliceColor;
                behaviour.currentSliceWeight = data.SliceWeight;
            }

            Image image = newSliceObj.GetComponent<Image>();
            if (image != null)
            {
                image.color = data.SliceColor;

                float fillPercentage = data.SliceWeight / totalWeight;
                image.fillAmount = fillPercentage;

                newSliceObj.transform.localRotation = Quaternion.Euler(0, 0, -accumulatedZRotation);
                accumulatedZRotation += fillPercentage * 360f;
            }
        }
    }

    private void SetupRectTransform(GameObject obj)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();

        if (rt != null)
        {
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = Vector2.zero;
            rt.sizeDelta = new Vector2(sliceWidth, sliceHeight);
            rt.localScale = Vector3.one;
        }
    }
}
