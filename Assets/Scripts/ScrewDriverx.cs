using UnityEngine;

public class ScrewDriverx : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float risePerDegree = 0.001f;

    private float currentRotation = 0f;
    private Vector3 localStartPos;

    void Start()
    {
        localStartPos = transform.localPosition;
    }

    public void RotateScrew(float angle)
    {
        currentRotation += angle * rotationSpeed;

        // Rotate around its own axis
        transform.localRotation *= Quaternion.AngleAxis(angle * rotationSpeed, Vector3.up);

        // Move outward along its local "up"
        float rise = currentRotation * risePerDegree;
        transform.localPosition = localStartPos + transform.up * rise;
    }
}
