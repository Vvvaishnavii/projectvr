using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NutAndBolt : MonoBehaviour
{
    public Transform boltAnchor;  // Where the nut is attached
    public GameObject bolt;       // The Bolt GameObject
    public int requiredRotations = 3;

    private XRGrabInteractable grabInteractable;
    private int totalTurns = 0;
    private float lastAngle = 0f;
    private bool nutUnscrewed = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (bolt != null)
            bolt.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        if (nutUnscrewed) return;

        if (grabInteractable.isSelected) // Nut is grabbed
        {
            Vector3 nutDir = transform.position - boltAnchor.position;
            float currentAngle = Mathf.Atan2(nutDir.y, nutDir.x) * Mathf.Rad2Deg;

            float delta = Mathf.DeltaAngle(lastAngle, currentAngle);
            lastAngle = currentAngle;

            if (Mathf.Abs(delta) > 30f)  // Ignore micro shakes
            {
                totalTurns += Mathf.RoundToInt(delta / 360f);
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
            lastAngle = 0f;
        }
    }

    void UnlockBolt()
    {
        Debug.Log("Nut unscrewed! Bolt is free.");
        Rigidbody rb = bolt.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;  // Bolt can now be pulled out.
    }
}
