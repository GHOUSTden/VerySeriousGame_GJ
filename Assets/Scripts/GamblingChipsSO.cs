using UnityEngine;
using UnityEngine.UI;

public enum ChipRarity
{
    Common, // Red
    Uncommon, // Green
    Rare, // Black
}

[CreateAssetMenu(fileName = "GamblingChip", menuName = "ScriptableObject/GamblingChip")]
public class GamblingChipsSO : ScriptableObject
{
    [SerializeField] private string chipName;
    public string ChipName { get => chipName; private set => chipName = value; }

    [SerializeField] private Sprite chipIcon;
    public Sprite ChipIcon { get => chipIcon; private set => chipIcon = value; }

    [TextArea(3, 10)]
    [SerializeField] private string description;
    public string Description { get => description; private set => description = value; }

    [SerializeField] private Sprite chipColorImage;
    public Sprite ChipColorImage { get => chipColorImage; private set => chipColorImage = value; }

    [SerializeField] private GameObject chipPrefab;
    public GameObject ChipPrefab { get => chipPrefab; private set => chipPrefab = value; }

    [SerializeField] private ChipRarity rarity;
    public ChipRarity Rarity { get => rarity; private set => rarity = value; }
}
