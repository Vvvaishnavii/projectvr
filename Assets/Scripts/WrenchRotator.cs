using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WrenchRotator : MonoBehaviour
{
    private XRGrabInteractable grab;
    private Quaternion lastRotation;
    private bool isHeld = false;
    private ScrewDriver activeScrew;

    public float rotationThreshold = 1f;
    public LayerMask screwLayer;

    [Header("Contact Zone")]
    public Transform contactPoint; // 👈 Assign this in the Inspector (the small BoxCollider object)

    public float detectionRadius = 0.03f;

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
        activeScrew = null;
    }

    void Update()
    {
        if (!isHeld || contactPoint == null) return;

        if (activeScrew == null)
        {
            Collider[] hits = Physics.OverlapSphere(contactPoint.position, detectionRadius, screwLayer);

            foreach (Collider col in hits)
            {
                ScrewDriver found = col.GetComponent<ScrewDriver>();
                if (found != null)
                {
                    activeScrew = found;
                    break;
                }
            }
        }

        if (activeScrew == null) return;

        Quaternion currentRotation = transform.rotation;
        Quaternion delta = currentRotation * Quaternion.Inverse(lastRotation);

        delta.ToAngleAxis(out float angle, out Vector3 axis);

        if (angle > rotationThreshold && Vector3.Dot(axis, activeScrew.transform.up) > 0.7f)
        {
            activeScrew.RotateScrew(angle);
        }

        lastRotation = currentRotation;
    }

    // Optional: Draw sphere in editor for debug
    private void OnDrawGizmosSelected()
    {
        if (contactPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(contactPoint.position, detectionRadius);
        }
    }
}