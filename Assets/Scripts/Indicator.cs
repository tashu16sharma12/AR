using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem.EnhancedTouch;

public class Indicator : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] GameObject indicator;
    [SerializeField] float doubleTapThreshold;
    [SerializeField] Transform prefab;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        EnhancedTouchSupport.Enable();
        indicator.SetActive(false);
    }

    void Update()
    {
        var ray = new Vector2(Screen.width / 2, Screen.height / 2);

        if(raycastManager.Raycast(ray, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;
            indicator.transform.position = hitPose.position;

            if(!indicator.activeSelf)
            {
                indicator.SetActive(true);
            }
        }

        else
        {
            if (indicator.activeSelf)
            {
                indicator.SetActive(false);
            }
        }

        if (IsDoubleTap())
        {
            Transform Obj = Instantiate(prefab, indicator.transform.position, indicator.transform.rotation);
            Obj.GetComponent<RotateObject>().newRotation = indicator.transform.rotation;
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
}
