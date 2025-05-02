using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem.EnhancedTouch;

public class Indicator : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] GameObject indicator;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public Vector3 Pos
    {
        get => indicator.transform.position;
    }

    public Quaternion Rot
    {
        get => indicator.transform.rotation;
    }

    void Start()
    {
        EnhancedTouchSupport.Enable();
        indicator.SetActive(false);
    }

    void Update()
    {
        PlaceIndicator();
    }

    void PlaceIndicator()
    {
        var ray = new Vector2(Screen.width / 2, Screen.height / 2);

        if (raycastManager.Raycast(ray, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;
            indicator.transform.position = hitPose.position;

            if (!indicator.activeSelf)
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
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
}