using UnityEngine;

[CreateAssetMenu(fileName = "SliceData", menuName = "ScriptableObject/SliceData")]
public class SliceData : ScriptableObject
{
    [SerializeField] private int slicePoints;
    public int SlicePoints { get => slicePoints; private set => slicePoints = value; }

    [SerializeField] private Color sliceColor;
    public Color SliceColor { get => sliceColor; private set => sliceColor = value; }

    [SerializeField] private float sliceWeight;
    public float SliceWeight { get => sliceWeight; private set => sliceWeight = value; }
}
