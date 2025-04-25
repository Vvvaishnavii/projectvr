using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NutAndBolt : MonoBehaviour
{
    public Transform boltAnchor;  // Where the nut is attached
    public GameObject bolt;       // The Bolt GameObject
    public int requiredRotations = 3;

    private XRGrabInteractable grabInteractable;
    private int totalTurns = 0;
    private Quaternion lastRotation;
    private bool nutUnscrewed = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        lastRotation = transform.rotation;

        if (bolt != null)
            bolt.GetComponent<Rigidbody>().isKinematic = false;
    }

    void Update()
    {
        if (nutUnscrewed) return;

        if (grabInteractable.isSelected)
        {
            // Lock position to bolt anchor
            transform.position = boltAnchor.position;

            Quaternion currentRotation = transform.rotation;
            Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
            lastRotation = currentRotation;

            // Extract rotation around local Y-axis
            float angle;
            Vector3 axis;
            deltaRotation.ToAngleAxis(out angle, out axis);
            if (angle > 180f) angle -= 360f;

            // Only consider rotation around Y (or adjust axis as needed)
            float yRotation = Vector3.Dot(axis, transform.up) * angle;

            if (Mathf.Abs(yRotation) > 10f) // threshold to ignore jitter
            {
                totalTurns += Mathf.RoundToInt(yRotation / 360f);
                Debug.Log("Total turns: " + totalTurns);
                if (Mathf.Abs(totalTurns) >= requiredRotations)
                {
                    nutUnscrewed = true;
                    UnlockBolt();
                }
            }
        }
        else
        {
            lastRotation = transform.rotation;
        }
    }

    void UnlockBolt()
    {
        Debug.Log("Nut unscrewed! Bolt is free.");
        Rigidbody rb = bolt.GetComponent<Rigidbody>();
        rb.useGravity = true;
    }
}