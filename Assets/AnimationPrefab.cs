using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AnimationPrefab : MonoBehaviour
{
    [SerializeField] RawImage icon;
    [SerializeField] TMP_Text animName;
    [SerializeField] GameObject selectedIcon;
    [SerializeField] bool isSelected;
    [SerializeField] AnimationData animationData;

    public bool IsSelected
    {
        get => isSelected;
        set
        {
            isSelected = value;

            if(isSelected)
            {
                selectedIcon.SetActive(true);

                foreach(CustomAnimation item in animationData.Animations)
                {
                    item.ForwardAnimation();
                }
            }
            else
            {
                selectedIcon.SetActive(false);

                foreach (CustomAnimation item in animationData.Animations)
                {
                    item.ReverseAnimation();
                }
            }
        }
    }

    public void SetData(AnimationData animationData)
    {
        this.icon.texture = animationData.AnimIcon;
        this.animName.text = animationData.AnimName;
        this.animationData = animationData;
    }

    public void OnClick()
    {
        IsSelected = !isSelected;
    }
}
