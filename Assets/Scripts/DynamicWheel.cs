using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class DynamicWheel : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject slicePrefab;
    [SerializeField] private Transform slicesContainer;

    [SerializeField] private float sliceWidth = 1f;
    [SerializeField] private float sliceHeight = 1f;

    public List<SliceData> wheelSlices = new List<SliceData>();

    private bool isSpinning = false;

    private Vector2 originalContainerScale;

    private Sequence activeSequence;

    public void GenerateWheel()
    {
        originalContainerScale = slicesContainer.localScale;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }

        activeSequence = DOTween.Sequence();

        if (!isSpinning)
        {
            activeSequence.Append(slicesContainer.DOScale(Vector3.one * 1.015f, 0.25f));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }

        activeSequence = DOTween.Sequence();
        
        activeSequence.Append(slicesContainer.DOScale(originalContainerScale, 0.25f));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSpinning)
        {
            isSpinning = true;
            int randomAngle = Random.Range(0, 361);

            slicesContainer.DOPunchScale(new Vector3(-0.1f, -0.1f, 0f), 0.15f, 10, 1f);
            slicesContainer.DOLocalRotate(new Vector3(0f, 0f, -((360 * 5) + randomAngle)), 5f, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => isSpinning = false);
        }
    }

    private void OnDisable()
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }
    }
}
