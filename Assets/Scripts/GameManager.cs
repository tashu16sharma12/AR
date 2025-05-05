using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
            }

            return instance;
        }
    }

    [SerializeField] Indicator indicator;
    [SerializeField] Transform prefab;
    [SerializeField] GameObject backButton;
    [SerializeField] Transform currentObj;
    [SerializeField] float doubleTapThreshold;

    [SerializeField] AnimationPrefab animationPrefab;
    [SerializeField] Transform animationContent;
    [SerializeField] PaintPrefab paintPrefab;
    [SerializeField] Transform paintContent;

    private void Update()
    {
        if (IsDoubleTap() || Input.GetKeyDown(KeyCode.Space))
        {
            if(currentObj != null)
            {
                Destroy(currentObj.gameObject);
            }

            ClearTab(animationContent);
            ClearTab(paintContent);
            currentObj = Instantiate(prefab, indicator.Pos, indicator.Rot);
            currentObj.GetComponent<RotateObject>().newRotation = indicator.Rot;
            backButton.SetActive(true);
            indicator.Disable();
        }
    }

    float lastTapTime = 0f;
    bool IsDoubleTap()
    {
        bool tap = false;

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            float currentTime = Time.time;

            if (currentTime - lastTapTime <= doubleTapThreshold)
            {
                Debug.Log("Double Tap Detected!");
                tap = true;
            }

            lastTapTime = currentTime;
        }

        return tap;
    }

    public void SetAnimations(AnimationData animationData)
    {
        Instantiate(animationPrefab, animationContent).SetData(animationData);
    }

    public void SetPaints(PaintData paint)
    {
        Instantiate(paintPrefab, paintContent).SetData(paint);
    }

    void ClearTab(Transform trans)
    {
        if (trans.childCount < 1) return;
        foreach(Transform item in trans)
        {
            Destroy(item.gameObject);
        }
    }

    public void OnClickBack()
    {
        backButton.SetActive(false);
        indicator.Enable();

        if(currentObj != null)
        {
            Destroy(currentObj.gameObject);
        }
    }
}
