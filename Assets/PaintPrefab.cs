using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PaintPrefab : MonoBehaviour
{
    [SerializeField] RawImage icon;
    [SerializeField] TMP_Text animName;
    [SerializeField] GameObject selectedIcon;
    [SerializeField] PaintData paintData;

    public void SetData(PaintData paintData)
    {
        this.icon.texture = paintData.PaintIcon;
        this.animName.text = paintData.PaintName;
        this.paintData = paintData;
    }

    public void OnClick()
    {
        foreach(Material item in paintData.Materials)
        {
            item.color = paintData.PaintColor;
        }
    }
}
