using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrewDriver : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float risePerDegree = 0.001f;
    public float activationHeight = 0.1f;
    public XRGrabInteractable screwGrab;

    private float currentRotation = 0f;
    private Vector3 initialLocalPosition;
    private bool isActivated = false;

    public ScrewManager manager; // 👈 Assign in inspector

    void Start()
    {
        initialLocalPosition = transform.localPosition;

        if (screwGrab != null)
            screwGrab.enabled = false;
    }

    public void RotateScrew(float angle)
    {
        currentRotation += angle * rotationSpeed;

        transform.Rotate(0f, angle * rotationSpeed, 0f, Space.Self);

        float rise = currentRotation * risePerDegree;
        transform.localPosition = initialLocalPosition - transform.up * rise;

        if (!isActivated && Mathf.Abs(Vector3.Dot(transform.localPosition - initialLocalPosition, transform.up)) > activationHeight)

        {
            isActivated = true;

            if (screwGrab != null)
                screwGrab.enabled = true;

            if (manager != null)
                manager.ScrewDetached(); // 👈 Notify manager
        }
    }
}
