using UnityEngine;

public class SliceBehaviour : MonoBehaviour
{
    public SliceData sliceData;

    public int currentSlicePoints;
    public Color currentSliceColor;
    public float currentSliceWeight;

    private void Initialize(SliceData data)
    {
        this.sliceData = data;

        currentSlicePoints = data.SlicePoints;
        currentSliceColor = data.SliceColor;
        currentSliceWeight = data.SliceWeight;
    }
}
