using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MovingBackground : MonoBehaviour
{
    private Image uiImage;
    private Material backgroundMaterial;

    [Header("Scrolling Settings")]
    [SerializeField] private Vector2 scrollSpeed = new Vector2(0.05f, 0.05f);
    [SerializeField] private float loopDuration = 10f;

    private Tween scrollTween;

    private void Start()
    {
        uiImage = GetComponent<Image>();

        if (uiImage != null && uiImage.material != null)
        {
            backgroundMaterial = new Material(uiImage.material);
            uiImage.material = backgroundMaterial;

            StartInfiniteScroll();
        }
    }

    public void StartInfiniteScroll()
    {
        Vector2 targetOffset = scrollSpeed * loopDuration;

        scrollTween = DOTween.To(() => backgroundMaterial.GetTextureOffset("_MainTex"), x => backgroundMaterial.SetTextureOffset("_MainTex", x), targetOffset, loopDuration)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
        if (scrollTween != null)
        {
            scrollTween.Kill();
        }

        if (backgroundMaterial != null)
        {
            Destroy(backgroundMaterial);
        }
    }
}
