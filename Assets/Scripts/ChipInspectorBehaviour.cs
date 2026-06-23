using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
using TMPEffects;

public class ChipInspectorBehaviour : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject mainBody;
    [SerializeField] private TextMeshProUGUI chipName;
    [SerializeField] private TextMeshProUGUI chipDescription;
    [SerializeField] private Image chipIcon;
    [SerializeField] private Image chipBody;

    private string _chipName;
    private string _chipDescription;
    private Sprite _chipIcon;
    private Sprite _chipBody;
    private ChipRarity _chipRarity;

    private Sequence activeSequence;

    public void GetDataFromChip(GamblingChipsSO chipData)
    {
        _chipName = chipData.ChipName;
        _chipDescription = chipData.Description;
        _chipBody = chipData.ChipColorImage;
        _chipIcon = chipData.ChipIcon;
        _chipRarity = chipData.Rarity;

        CallChipInspectorUI();
    }

    private void CallChipInspectorUI()
    {
        switch (_chipRarity)
        {
            case ChipRarity.Common:
                chipName.text = $"<color=#F5E8D1>{_chipName}";
                break;

            case ChipRarity.Uncommon:
                chipName.text = $"<color=green>{_chipName}";
                break;

            case ChipRarity.Rare:
                chipName.text = $"<wave><color=#00FFB9>{_chipName}";
                break;

            default:
                chipName.text = $"<color=#F5E8D1>{_chipName}";
                break;
        }

        chipDescription.text = _chipDescription;
        chipIcon.sprite = _chipIcon;
        chipBody.sprite = _chipBody;

        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }

        activeSequence = DOTween.Sequence();

        activeSequence
            .Append(mainBody.transform.DOLocalMoveY(0, 0.5f)).SetEase(Ease.InOutCubic);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }

        activeSequence = DOTween.Sequence();

        activeSequence
            .Append(mainBody.transform.DOLocalMoveY(1100, 0.5f)).SetEase(Ease.InOutCubic);
    }

    private void OnDisable()
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }
    }
}
