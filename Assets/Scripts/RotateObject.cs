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
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isRotating = true;
                rotateStartPosition = touch.position;
            }
            else if (isRotating && touch.phase == TouchPhase.Moved)
            {
                rotateCurrentPosition = touch.position;

                Vector3 difference = rotateStartPosition - rotateCurrentPosition;
                rotateStartPosition = rotateCurrentPosition;

                Vector3 rotationEulerAngles = newRotation.eulerAngles;
                rotationEulerAngles += Vector3.up * (difference.x / 5f) * rotationAmount;

                newRotation = Quaternion.Euler(rotationEulerAngles);
            }
            else if (isRotating && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
            {
                isRotating = false;
            }
        }
    }
}