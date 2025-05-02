using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class EstimateLight : MonoBehaviour
{
    [SerializeField] ARCameraManager cameraManager;
    [SerializeField] Light dirLight;

    private void OnEnable()
    {
        cameraManager.frameReceived += GetLight;
    }

    private void OnDisable()
    {
        cameraManager.frameReceived -= GetLight;
    }

    void GetLight(ARCameraFrameEventArgs args)
    {
        if(args.lightEstimation.mainLightColor.HasValue)
        {
            dirLight.color = args.lightEstimation.mainLightColor.Value;
            // float averageBrightness = 0.2126f * dirLight.color.r + 0.7152f * dirLight.color.g + 0.0722f * dirLight.color.b;
        }
    }
}
