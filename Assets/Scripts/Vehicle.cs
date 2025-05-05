using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    public AnimationData[] animations;
    public PaintData[] paints;

    void Start()
    {
        foreach(AnimationData item in animations)
        {
            GameManager.Instance.SetAnimations(item);
        }

        foreach (PaintData item in paints)
        {
            GameManager.Instance.SetPaints(item);
        }
    }
}

[System.Serializable]
public class AnimationData
{
    public string AnimName;
    public Texture AnimIcon;
    public List<CustomAnimation> Animations;
}

[System.Serializable]
public class PaintData
{
    public string PaintName;
    public Texture PaintIcon;
    public Color PaintColor;
    public List<Material> Materials;
}