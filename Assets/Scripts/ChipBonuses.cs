using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using DG.Tweening;

public class ChipBonuses : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform buttonTransform;

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

    }

    private void PointsToTheLowestSlice(int points)
    {

    }

    private void PointsToRandomSlice(int points)
    {

    }

    public void OnDisable()
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }
    }
}
