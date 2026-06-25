using DG.Tweening;
using UnityEngine;

public class Endings : MonoBehaviour
{
    [SerializeField] private GameObject winningScreen;
    [SerializeField] private GameObject loseScreen;

    [SerializeField] private GameObject playerWheel;
    [SerializeField] private GameObject pointsUI;
    [SerializeField] private GameObject turnsUI;
    [SerializeField] private GameObject bonusesUI;
    [SerializeField] private GameObject inspectorUI;
    [SerializeField] private GameObject randomChipButton;

    private Sequence activeSequence;

    public void ActivateLoseScreen()
    {
        inspectorUI.SetActive(false);

        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }

        activeSequence = DOTween.Sequence();

        activeSequence
            .Append(playerWheel.transform.DOLocalMoveX(-1500, 2.5f))
            .Join(pointsUI.transform.DOLocalMoveY(700, 2.5f))
            .Join(turnsUI.transform.DOLocalMoveY(-750, 2.5f))
            .Join(bonusesUI.transform.DOLocalMoveY(-740, 2.5f))
            .Join(randomChipButton.transform.DOLocalMoveX(1500, 2.5f))
            .OnComplete(() => { loseScreen.SetActive(true); });
    }

    public void ActivateWinningScreen()
    {
        inspectorUI.SetActive(false);

        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }

        activeSequence = DOTween.Sequence();

        activeSequence
            .Append(playerWheel.transform.DOLocalMoveX(-1500, 2.5f))
            .Join(pointsUI.transform.DOLocalMoveY(700, 2.5f))
            .Join(turnsUI.transform.DOLocalMoveY(-750, 2.5f))
            .Join(bonusesUI.transform.DOLocalMoveY(-740, 2.5f))
            .Join(randomChipButton.transform.DOLocalMoveX(1500, 2.5f))
            .OnComplete(() => { winningScreen.SetActive(true); });
    }

    private void OnDisable()
    {
        if (activeSequence != null && activeSequence.IsActive())
        {
            activeSequence.Kill();
        }
    }
}
