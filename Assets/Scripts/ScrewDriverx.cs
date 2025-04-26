using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrewDriverx : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float risePerDegree = 0.001f;
    public float activationHeight = 0.1f;
    public XRGrabInteractable screwGrab;

    public ScrewManager manager;

    private float currentRotation = 0f;
    private Vector3 initialLocalPosition;
    private bool isActivated = false;

    void Start()
    {
        initialLocalPosition = transform.localPosition;

        if (screwGrab != null)
            screwGrab.enabled = false;
    }

    public void RotateScrew(float angle)
    {
        currentRotation += angle * rotationSpeed;

        // Rotate and lift
        transform.Rotate(0f, angle * rotationSpeed, 0f, Space.Self);
        float rise = currentRotation * risePerDegree;
        transform.localPosition = initialLocalPosition - transform.up * rise;

        if (!isActivated && Vector3.Distance(transform.localPosition, initialLocalPosition) > activationHeight)
        {
            isActivated = true;
            if (screwGrab != null)
                screwGrab.enabled = true;

            if (manager != null)
                manager.CheckAllGroups();
        }
    }

    public bool IsDetached()
    {
        return isActivated;
    }
}
