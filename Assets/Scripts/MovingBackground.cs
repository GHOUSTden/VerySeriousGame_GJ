using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MovingBackground : MonoBehaviour
{
    // !!! YOU NEED TO HAVE DOTWEEN INSTALLED !!!
    // For those who are interested in how that 'Moving Background' work and wanted to make same in your own game.
    // You need to put this script directly into the Moving Background GameObject.
    // Image of your background (Needed to be Repeatable) and in Image Component change Image Type to Tiled.
    private Image uiImage;
    // You just need to create material elsewhere and then change material shader to UI/Default and that's it, you now have pretty looking background in your game :)
    private Material backgroundMaterial;

    [Header("Scrolling Settings")]
    // Direction where it going to move and speed
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
