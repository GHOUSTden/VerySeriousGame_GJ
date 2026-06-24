using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using NUnit.Framework.Constraints;
using UnityEngine.UI;

public class GamblingChipBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GamblingChipsSO chipData;

    public string currentChipName;
    public Sprite currentChipIcon;
    public string currentDescription;
    public Sprite currentChipColorImage;
    public GameObject currentChipPrefab;
    public ChipRarity currentRarity;
    public ChipBonus currentBonus;

    public GameObject chipBody;
    [SerializeField] private Transform chipTransform;
    [SerializeField] private ChipInspectorBehaviour chipInspectorBehaviour;

    private Vector2 originalChipScale;

    private Sequence activeSequence;

    public void Initialize(GamblingChipsSO data)
    {
        chipData = data;

        currentChipName = chipData.ChipName;
        currentChipIcon = chipData.ChipIcon;
        currentDescription = chipData.Description;
        currentChipColorImage = chipData.ChipColorImage;
        currentChipPrefab = chipData.ChipPrefab;
        currentRarity = chipData.Rarity;
        currentBonus = chipData.Bonus;

        chipTransform = chipBody.GetComponent<Transform>();
        chipInspectorBehaviour = FindAnyObjectByType<ChipInspectorBehaviour>();

        chipBody.transform.Find("Icon").GetComponent<Image>().sprite = currentChipIcon;

        originalChipScale = chipTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }

        activeSequence = DOTween.Sequence();

        activeSequence
            .Append(chipTransform.DOScale(Vector3.one * 1.015f, 0.25f))
            .Join(chipTransform.DOLocalMoveY(30f, 0.25f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }

        activeSequence = DOTween.Sequence();

        activeSequence
            .Append(chipTransform.DOScale(originalChipScale, 0.25f))
            .Join(chipTransform.DOLocalMoveY(0f, 0.25f));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentChipName == null || currentDescription == null || currentChipColorImage == null || currentChipIcon == null)
        {
            Initialize(chipData);
        }

        chipTransform.DOPunchScale(new Vector3(-0.05f, -0.05f, 0f), 0.15f, 10, 1f);

        chipInspectorBehaviour.GetDataFromChip(chipData, this.gameObject);
    }

    private void OnDisable()
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }
    }
}
