using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ChipInspectorBehaviour : MonoBehaviour
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

    public void GetDataFromChip(GamblingChipsSO chipData)
    {
        _chipName = chipData.ChipName;
        _chipDescription = chipData.Description;
        _chipBody = chipData.ChipColorImage;
        _chipIcon = chipData.ChipIcon;

        CallChipInspectorUI();
    }

    private void CallChipInspectorUI()
    {
        chipName.text = _chipName;
        chipDescription.text = _chipDescription;
        chipIcon.sprite = _chipIcon;
        chipBody.sprite = _chipBody;

        mainBody.SetActive(true);
    }
}
