using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private Transform handContainer;
    [SerializeField] private int maxChipsInHand = 6;

    public void AddChipToHand(GamblingChipsSO chipData)
    {
        if (handContainer.childCount >= maxChipsInHand)
        {
            Debug.Log("Hand is full!");

            return;
        }

        GameObject spawnedChip = Instantiate(chipData.ChipPrefab, handContainer);

        var chipBehaviour = spawnedChip.GetComponent<GamblingChipBehaviour>();
        if (chipBehaviour != null)
        {
            chipBehaviour.chipBody = spawnedChip;
            chipBehaviour.Initialize(chipData);
        }
    }
}
