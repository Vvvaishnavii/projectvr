using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapZone : MonoBehaviour
{
    public GameObject correctPart;
    public Transform snapPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == correctPart)
        {
            other.transform.position = snapPosition.position;
            other.transform.rotation = snapPosition.rotation;

            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null) grab.enabled = false;  // lock it!

            Debug.Log("Part snapped!");
        }
    }
}
