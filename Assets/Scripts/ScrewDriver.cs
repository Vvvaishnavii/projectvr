using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrewDriver : MonoBehaviour
{
    public float rotationSpeed = 1f;         // Sensitivity of rotation
    public float risePerDegree = 0.001f;     // How much screw rises per degree
    public float activationHeight = 0.1f;    // Height after which screw becomes grabbable
    public XRGrabInteractable screwGrab;     // Assign in Inspector

    private float currentRotation = 0f;
    private Vector3 initialLocalPosition;
    private bool isActivated = false;

    void Start()
    {
        initialLocalPosition = transform.localPosition;

        // Disable grabbing at start
        if (screwGrab != null)
        {
            screwGrab.enabled = false;
        }
    }

    public void RotateScrew(float angle)
    {
        currentRotation += angle * rotationSpeed;

        // Rotate around local Y-axis
        transform.Rotate(0f, angle * rotationSpeed, 0f, Space.Self);

        // Lift along local up direction
        float rise = currentRotation * risePerDegree;
        transform.localPosition = initialLocalPosition - transform.up * rise;

        // Enable grabbing once screw is sufficiently raised
        if (!isActivated && Vector3.Distance(transform.localPosition, initialLocalPosition) > activationHeight)
        {
            isActivated = true;

            if (screwGrab != null)
            {
                screwGrab.enabled = true;
            }
        }
    }
}



//     transform.Rotate(0f, angle * rotationSpeed, 0f, Space.Self);