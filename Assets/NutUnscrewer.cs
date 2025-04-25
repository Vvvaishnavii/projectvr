using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NutUnscrewer : MonoBehaviour
{
    public XRBaseInteractor wrenchInteractor;      // The hand/controller holding the wrench
    public Transform nutTransform;                 // Assign your nut object in inspector
    public Transform boltTransform;                // Assign your bolt object in inspector
    public float rotationThreshold = 360f * 5f;    // Total degrees needed to unscrew
    public float unscrewTolerance = 0.5f;          // Angle alignment check

    private bool isGrippingNut = false;
    private Quaternion previousWrenchRotation;
    private float totalRotation = 0f;
    private Rigidbody nutRigidbody;

    void Start()
    {
        if (nutTransform != null)
            nutRigidbody = nutTransform.GetComponent<Rigidbody>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform == nutTransform && wrenchInteractor != null && wrenchInteractor.hasSelection)
        {
            // Check the wrench's alignment to the bolt's axis (optional realism)
            Vector3 toNut = nutTransform.position - transform.position;
            Vector3 wrenchForward = transform.forward;
            float alignment = Vector3.Dot(toNut.normalized, wrenchForward.normalized);

            if (alignment > unscrewTolerance)  // If aligned, grip the nut
            {
                if (!isGrippingNut)
                {
                    isGrippingNut = true;
                    previousWrenchRotation = transform.rotation;
                    Debug.Log("Nut Grip Locked");
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == nutTransform)
        {
            isGrippingNut = false;
            Debug.Log("Nut Released");
        }
    }

    void Update()
    {
        if (isGrippingNut && nutTransform != null)
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion deltaRotation = currentRotation * Quaternion.Inverse(previousWrenchRotation);
            deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

            // Make sure rotation is around the bolt's up axis
            float signedAngle = Vector3.Dot(axis, boltTransform.up) > 0 ? angle : -angle;

            totalRotation += signedAngle;

            // Apply the rotation to the nut
            nutTransform.RotateAround(boltTransform.position, boltTransform.up, signedAngle);

            previousWrenchRotation = currentRotation;

            Debug.Log($"Total Rotation: {totalRotation}");

            // When the nut is fully unscrewed
            if (Mathf.Abs(totalRotation) >= rotationThreshold)
            {
                Debug.Log("Nut Fully Unscrewed!");
                DetachNut();
                isGrippingNut = false;
            }
        }
    }

    private void DetachNut()
    {
        if (nutRigidbody != null)
        {
            nutRigidbody.isKinematic = false;  // Let physics take over
            nutRigidbody.AddForce(transform.up * 1f, ForceMode.Impulse);  // Little pop
        }
    }
}
