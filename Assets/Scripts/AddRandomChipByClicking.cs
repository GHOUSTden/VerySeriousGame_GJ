using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class AddRandomChipByClicking : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private HandManager handManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        List<string> keysList = new List<string>(GameDataRegistry.chipID.Keys);

        int randomIndex = Random.Range(0, keysList.Count);

        handManager.AddChipToHand(GameDataRegistry.chipID[keysList[randomIndex]]);
    }
}
