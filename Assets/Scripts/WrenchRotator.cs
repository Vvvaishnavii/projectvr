using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WrenchRotator : MonoBehaviour
{
    public ScrewDriver screw;                 // Reference to the screw
    public Transform screwCenter;            // Pivot point for rotation (usually screw head)

    private XRGrabInteractable grab;
    private Quaternion lastRotation;
    private bool isHeld = false;
    private float rotationThreshold = 1f;    // Minimum angle to consider a valid rotation

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        lastRotation = transform.rotation;
        isHeld = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
    }

    void Update()
    {
        if (!isHeld || screw == null || screwCenter == null) return;

        Quaternion currentRotation = transform.rotation;
        Quaternion delta = currentRotation * Quaternion.Inverse(lastRotation);

        delta.ToAngleAxis(out float angle, out Vector3 axis);

        // ✅ Ignore tiny rotations (controller jitter or grab initiation)
        if (angle < rotationThreshold) return;

        // ✅ Only apply rotation if mostly aligned with screw's forward axis (usually Z or adjust to Y if needed)
        if (Vector3.Dot(axis, screwCenter.up) > 0.7f) // use .up if screw rotates around Y
        {
            screw.RotateScrew(angle);
        }

        lastRotation = currentRotation;
    }
}
