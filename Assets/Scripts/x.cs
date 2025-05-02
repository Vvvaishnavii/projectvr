using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class x : MonoBehaviour
{
    public float rotationSpeed = 1f;         // Sensitivity of rotation
    public float risePerDegree = 0.001f;     // How much screw rises per degree
    public float activationHeight = 0.1f;    // Height after which screw becomes grabbable
    public XRGrabInteractable screwGrab;     // Assign in Inspector
    public Transform screwCenter;            // The screw head (this should be assigned in the inspector)

    private float currentRotation = 0f;
    private Vector3 initialLocalPosition;
    private bool isActivated = false;

    void Start()
    {
        initialLocalPosition = transform.localPosition;

        // Disable the grab functionality at the start
        if (screwGrab != null)
        {
            screwGrab.enabled = false;  // Start with XRGrabInteractable disabled
        }
    }

    public void RotateScrew(float angle)
    {
        currentRotation += angle * rotationSpeed;

        // Rotate the screw around its local Y-axis
        transform.Rotate(0f, angle * rotationSpeed, 0f, Space.Self);

        // Move the screw upwards along its local up direction (z-axis in case of screw)
        float rise = currentRotation * risePerDegree;
        transform.localPosition = initialLocalPosition - transform.up * rise;

        // Enable grabbing once the screw is sufficiently raised (activates the collider)
        if (!isActivated && Vector3.Distance(transform.localPosition, initialLocalPosition) > activationHeight)
        {
            isActivated = true; // Mark as activated
            Debug.Log("Screw is now grabbable!"); // Debug log

            if (screwGrab != null)
            {
                screwGrab.enabled = true; // Enable the XRGrabInteractable
            }
        }
    }
}
