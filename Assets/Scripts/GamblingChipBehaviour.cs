using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GamblingChipBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GamblingChipsSO chipData;

    public string currentChipName;
    public Sprite currentChipIcon;
    public string currentDescription;
    public Sprite currentChipColorImage;
    public GameObject currentChipPrefab;
    public ChipRarity currentRarity;

    [SerializeField] private GameObject chipParent;
    [SerializeField] private Transform chipTransform;

    private Vector2 originalChipScale;
    private float chipPosY;

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

    }

    private void Start()
    {
        originalChipScale = chipTransform.localScale;
        chipPosY = chipTransform.position.y;
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
            .Join(chipTransform.DOLocalMoveY(chipTransform.position.y + 30f, 0.25f));
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
            .Join(chipTransform.DOLocalMoveY(chipPosY, 0.25f));
    }

    private void OnDisable()
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }
    }
}
