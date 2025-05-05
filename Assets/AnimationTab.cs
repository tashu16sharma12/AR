using UnityEngine;
using DG.Tweening;

public class AnimationTab : Tab
{
    [SerializeField] GameObject closeButton;

    public override void CloseTab()
    {
        transform.DOMove(new Vector3(transform.position.x, -360f, 0f), 1f).OnComplete(() =>
        {
            closeButton.SetActive(false);
        });
    }

    public override void OpenTab()
    {
        transform.DOMove(new Vector3(transform.position.x, 0f, 0f), 1f).OnComplete(() =>
        {
            closeButton.SetActive(true);
        });
    }
}
