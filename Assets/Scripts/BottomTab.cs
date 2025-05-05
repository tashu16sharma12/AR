using DG.Tweening;
using UnityEngine;

public class BottomTab : MonoBehaviour
{
    [SerializeField] GameObject closeButton;

    public void CloseTab()
    {
        transform.DOMove(new Vector3(transform.position.x, -240f, 0f), 1f).OnComplete(() =>
        {
            closeButton.SetActive(false);
        });
    }

    public void OpenTab()
    {
        transform.DOMove(new Vector3(transform.position.x, 0f, 0f), 1f).OnComplete(() =>
        {
            closeButton.SetActive(true);
        });
    }
}
