using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager instance;
    public GameManager Instance
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

    private void Update()
    {
        if (IsDoubleTap())
        {
            if(currentObj != null)
            {
                Destroy(currentObj.gameObject);
            }

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
