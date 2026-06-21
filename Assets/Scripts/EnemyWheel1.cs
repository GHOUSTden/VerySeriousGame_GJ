using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWheel1 : MonoBehaviour
{
    [Header("Prefabs, etc.")]
    [SerializeField] private GameObject slicePrefab;
    [SerializeField] private Transform slicesContainer;
    [SerializeField] private Transform slicesParent;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private Transform linesTransform;
    [SerializeField] private TextMeshProUGUI enemyPointsCounter;

    [Header("Slice Configs")]
    [SerializeField] private float sliceWidth = 1f;
    [SerializeField] private float sliceHeight = 1f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tickAudioClip;
    [SerializeField][Range(0f, 1f)] private float volume = 0.5f;
    [SerializeField][Range(-3f, 3f)] private float pitch = 1f;

    [Range(1, 20)] public int spinDuration = 5;

    public List<SliceData> wheelSlices = new List<SliceData>();

    [HideInInspector] public List<SliceBehaviour> activeWheelSlices = new List<SliceBehaviour>();

    private bool isSpinning = false;

    private Vector2 originalContainerScale;

    [Header("IDK")]
    [SerializeField] private DynamicWheel playerWheel;
    private int enemyPoints;
    private float sliceAngle;
    private float halfSliceAngle;
    private float halfSliceAngleWithPaddings;
    private double accumulatedWeight;
    private List<int> nonZeroChancesIndices = new List<int>();
    private System.Random rand = new System.Random();

    private DG.Tweening.Sequence activeSequence;

    public void GenerateWheel()
    {
        originalContainerScale = slicesContainer.localScale;

        activeWheelSlices.Clear();
        nonZeroChancesIndices.Clear();
        accumulatedWeight = 0;

        if (linesTransform != null)
        {
            foreach (Transform child in linesTransform)
            {
                Destroy(child.gameObject);
            }
        }

        if (wheelSlices.Count == 0 || slicePrefab == null)
        {
            return;
        }

        sliceAngle = 360f / wheelSlices.Count;
        halfSliceAngle = sliceAngle / 2f;
        halfSliceAngleWithPaddings = halfSliceAngle - (halfSliceAngle / 4f);

        CalculateWeightsAndIndices();
        SetupAudio();

        for (int i = 0; i < wheelSlices.Count; i++)
        {
            SliceData data = wheelSlices[i];
            if (data == null)
            {
                continue;
            }

            GameObject newSliceObj = Instantiate(slicePrefab, slicesParent);
            newSliceObj.AddComponent<SliceBehaviour>();
            SetupRectTransform(newSliceObj);

            SliceBehaviour behaviour = newSliceObj.GetComponent<SliceBehaviour>();
            if (behaviour != null)
            {
                behaviour.sliceData = data;
                behaviour.currentSlicePoints = data.SlicePoints;
                behaviour.currentSliceColor = data.SliceColor;
                behaviour.currentSliceWeight = data.SliceWeight;
                activeWheelSlices.Add(behaviour);
            }

            TextMeshProUGUI sliceText = newSliceObj.GetComponentInChildren<TextMeshProUGUI>();
            if (sliceText != null)
            {
                sliceText.text = behaviour.currentSlicePoints.ToString();
            }

            Image image = newSliceObj.GetComponent<Image>();
            if (image != null)
            {
                image.color = data.SliceColor;
                image.fillAmount = sliceAngle / 360f;
            }

            newSliceObj.transform.RotateAround(slicesContainer.position, Vector3.back, sliceAngle * i);

            if (linePrefab != null && linesTransform != null)
            {
                Transform lineTrns = Instantiate(linePrefab, linesTransform.position, Quaternion.identity, linesTransform).transform;
                lineTrns.RotateAround(slicesContainer.position, Vector3.back, sliceAngle * i);
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
            rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y, 0f);
        }
    }

    private void SetupAudio()
    {
        if (audioSource == null)
        {
            return;
        }

        audioSource.clip = tickAudioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
    }

    private void CalculateWeightsAndIndices()
    {
        for (int i = 0; i < wheelSlices.Count; i++)
        {
            SliceData data = wheelSlices[i];
            if (data == null)
            {
                continue;
            }

            accumulatedWeight += data.SliceWeight;

            if (data.SliceWeight > 0)
            {
                nonZeroChancesIndices.Add(i);
            }
        }
    }

    public void Spin()
    {
        if (isSpinning || wheelSlices.Count == 0)
        {
            return;
        }

        isSpinning = true;

        int index = GetRandomPieceIndex();

        float angle = -(sliceAngle * index);
        float rightOffset = (angle - halfSliceAngleWithPaddings) % 360;
        float leftOffset = (angle + halfSliceAngleWithPaddings) % 360;
        float randomAngle = Random.Range(leftOffset, rightOffset);

        Vector3 targetRotation = Vector3.back * (randomAngle + 2 * 360 * spinDuration);

        float prevAngle, currentAngle;
        prevAngle = currentAngle = slicesContainer.eulerAngles.z;
        bool isIndicatorOnTheLine = false;

        slicesContainer.DOPunchScale(new Vector3(-0.05f, -0.05f, 0f), 0.15f, 10, 1f);

        slicesContainer.DOLocalRotate(targetRotation, spinDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutQuart)
            .OnUpdate(() => {
                float diff = Mathf.Abs(prevAngle - currentAngle);
                if (diff >= halfSliceAngle)
                {
                    if (isIndicatorOnTheLine && audioSource != null && audioSource.clip != null)
                    {
                        audioSource.PlayOneShot(audioSource.clip);
                    }
                    prevAngle = currentAngle;
                    isIndicatorOnTheLine = !isIndicatorOnTheLine;
                }
                currentAngle = slicesContainer.eulerAngles.z;
            })
            .OnComplete(() => {
                isSpinning = false;

                if (activeWheelSlices == null || activeWheelSlices.Count == 0)
                {
                    Debug.LogError("ActiveWheelSlices is empty");
                }

                if (index < activeWheelSlices.Count)
                {
                    SliceBehaviour landedSlice = activeWheelSlices[index];
                    enemyPoints += landedSlice.currentSlicePoints;
                    enemyPointsCounter.text = $"{enemyPoints}";

                    Debug.Log($"Slice index: {index}, Points: {landedSlice.currentSlicePoints}");
                }

                playerWheel.EndEnemyTurn();

            });
    }

    private int GetRandomPieceIndex()
    {
        double r = rand.NextDouble() * accumulatedWeight;
        float currentWeightCounter = 0f;

        for (int i = 0; i < wheelSlices.Count; i++)
        {
            currentWeightCounter += wheelSlices[i].SliceWeight;
            if (currentWeightCounter >= r)
            {
                return i;
            }
        }
        return 0;
    }

    private void OnDisable()
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }
    }
}
