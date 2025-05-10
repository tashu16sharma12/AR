using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class EstimateLight : MonoBehaviour
{
    [SerializeField] ARCameraManager cameraManager;
    [SerializeField] Light[] dirLight;

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
        foreach(Light item in dirLight)
        {
            // Set light intensity
            if (args.lightEstimation.averageMainLightBrightness.HasValue) {
                item.intensity = args.lightEstimation.averageMainLightBrightness.Value;
            }

            // Set light direction
            if (args.lightEstimation.mainLightDirection.HasValue) {
                item.transform.rotation = Quaternion.LookRotation(args.lightEstimation.mainLightDirection.Value);
            }

            // Optional: Set light color
            if (args.lightEstimation.mainLightColor.HasValue) {
                item.color = args.lightEstimation.mainLightColor.Value;
            }
        }
    }
}
