using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Animation))]
public class CustomAnimation : MonoBehaviour
{
    [SerializeField] private AnimationClip forward;
    [SerializeField] private AnimationClip reverse;
    [SerializeField] private Animation animation;

    private void Awake()
    {
        if (forward != null && !animation.GetClip(forward.name))
            animation.AddClip(forward, forward.name);

        if (reverse != null && !animation.GetClip(reverse.name))
            animation.AddClip(reverse, reverse.name);

        animation = GetComponent<Animation>();
    }

    public void ForwardAnimation()
    {
        if (forward != null)
            animation.Play(forward.name);
    }

    public void ReverseAnimation()
    {
        if (reverse != null)
            animation.Play(reverse.name);
    }
}
