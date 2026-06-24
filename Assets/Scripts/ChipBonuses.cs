using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class ChipBonuses : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform buttonTransform;
    [SerializeField] private DynamicWheel playerWheel;
    [SerializeField] private EnemyWheel1 enemyWheel1;

    private ChipInspectorBehaviour chipInspector;
    private ChipBonus currentChipBonus;
    private GameObject currentChipGO;

    private DG.Tweening.Sequence activeSequence;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (chipInspector == null)
        {
            chipInspector = GetComponentInParent<ChipInspectorBehaviour>();
        }

        currentChipBonus = chipInspector._chipBonus;
        currentChipGO = chipInspector.chipGO;

        switch (currentChipBonus)
        {
            case ChipBonus.DroolingCat:
                Debug.Log("Drooling Cat Bonus Used");
                DroolingCatBonus();
                break;

            case ChipBonus.PlusFiveToTheHighestSlice:
                Debug.Log("Plus Five To The Highest Slice Bonus Used");
                PointsToTheHighestSlice(5);
                break;

            case ChipBonus.PlusFiveToRandomSlice:
                Debug.Log("Plus Five To Random Slice Bonus Used");
                PointsToRandomSlice(5);
                break;

            case ChipBonus.PlusFiveToTheLowestSlice:
                Debug.Log("Plus Five To The Lowest Slice Bonus Used");
                PointsToTheLowestSlice(5);
                break;
        }

        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }

        activeSequence = DOTween.Sequence();

        activeSequence
            .Append(buttonTransform.DOPunchScale(new Vector3(-0.05f, -0.05f, 0f), 0.15f, 10, 1f));

        chipInspector.CloseInspector();

        Destroy(currentChipGO);
    }

    private void DroolingCatBonus()
    {

    }

    private void PointsToTheHighestSlice(int points)
    {
        int highestIndex = 0;
        int highestPoints = int.MinValue;

        for (int i = 0; i < playerWheel.activeWheelSlices.Count; i++)
        {
            if (playerWheel.activeWheelSlices[i].currentSlicePoints > highestPoints)
            {
                highestPoints = playerWheel.activeWheelSlices[i].currentSlicePoints;
                highestIndex = i;
            }
        }

        SliceBehaviour landedSlice = playerWheel.activeWheelSlices[highestIndex];
        landedSlice.currentSlicePoints += points;

        TextMeshProUGUI sliceText = landedSlice.GetComponentInChildren<TextMeshProUGUI>();
        if (sliceText != null)
        {
            sliceText.text = landedSlice.currentSlicePoints.ToString();
        }
    }

    private void PointsToTheLowestSlice(int points)
    {
        int lowestIndex = 0;
        int lowestPoints = int.MaxValue;

        for (int i = 0; i < playerWheel.activeWheelSlices.Count; i++)
        {
            if (playerWheel.activeWheelSlices[i].currentSlicePoints < lowestPoints)
            {
                lowestPoints = playerWheel.activeWheelSlices[i].currentSlicePoints;
                lowestIndex = i;
            }
        }

        SliceBehaviour landedSlice = playerWheel.activeWheelSlices[lowestIndex];
        landedSlice.currentSlicePoints += points;

        TextMeshProUGUI sliceText = landedSlice.GetComponentInChildren<TextMeshProUGUI>();
        if (sliceText != null)
        {
            sliceText.text = landedSlice.currentSlicePoints.ToString();
        }
    }

    private void PointsToRandomSlice(int points)
    {
        int index = playerWheel.GetRandomPieceIndex();
        SliceBehaviour landedSlice = playerWheel.activeWheelSlices[index];
        landedSlice.currentSlicePoints += points;

        TextMeshProUGUI sliceText = landedSlice.GetComponentInChildren<TextMeshProUGUI>();
        if (sliceText != null)
        {
            sliceText.text = landedSlice.currentSlicePoints.ToString();
        }
    }

    public void OnDisable()
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }
    }
}
