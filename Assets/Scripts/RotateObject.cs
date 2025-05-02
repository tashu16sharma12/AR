using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float movementTime;
    [SerializeField] private float rotationAmount;

    public Quaternion newRotation;

    Vector3 rotateStartPosition;
    Vector3 rotateCurrentPosition;

    bool isRotating;

    private void Update()
    {
        HandleRotationInput();
        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, transform.position, Time.deltaTime * movementTime), Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime));
    }

    private void HandleRotationInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            rotateStartPosition = Input.mousePosition;
        }
        if (isRotating && Input.GetMouseButton(0))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            Vector3 rotationEulerAngles = newRotation.eulerAngles;

            rotationEulerAngles = newRotation.eulerAngles + (Vector3.up * (difference.x / 5f) * rotationAmount);

            newRotation = Quaternion.Euler(rotationEulerAngles);
        }
        if (isRotating && Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }
    }
}