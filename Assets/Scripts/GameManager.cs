using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
    [SerializeField] GameObject backButton;
    [SerializeField] Transform currentObj;
    [SerializeField] float doubleTapThreshold;

    [SerializeField] AnimationPrefab animationPrefab;
    [SerializeField] Transform animationContent;
    [SerializeField] PaintPrefab paintPrefab;
    [SerializeField] Transform paintContent;
    [SerializeField] AssetReferenceGameObject[] carsPrefabs;
    int carInd;

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

            LoadByIndex(0);

            backButton.SetActive(true);
            indicator.Disable();
        }
    }

    void LoadByIndex(int ind)
    {
        carsPrefabs[ind].LoadAssetAsync().Completed += OnAddressableLoaded;
    }

    void OnAddressableLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            currentObj = Instantiate(handle.Result, indicator.Pos, indicator.Rot).transform;
            currentObj.GetComponent<RotateObject>().newRotation = indicator.Rot;
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
