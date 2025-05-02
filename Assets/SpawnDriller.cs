using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnDriller : MonoBehaviour
{
    public GameObject drillerPrefab;
    public Transform rightHandAttachPoint;
    public string handTag = "Hand";

    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the touching object has the specified hand tag and prefab isn't already spawned
        if (!hasSpawned && other.CompareTag(handTag))
        {
            XRDirectInteractor interactor = other.GetComponent<XRDirectInteractor>();

            if (interactor != null)
            {
                SpawnInHand(interactor);
                hasSpawned = true; // Prevent multiple spawns
            }
        }
    }

    private void SpawnInHand(XRDirectInteractor interactor)
    {
        // Instantiate the prefab at the hand's attach point position and rotation
        GameObject driller = Instantiate(drillerPrefab, rightHandAttachPoint.position, rightHandAttachPoint.rotation);
        XRGrabInteractable interactable = driller.GetComponent<XRGrabInteractable>();

        if (interactable != null)
        {
            // Force select it into the hand immediately
            interactor.interactionManager.SelectEnter(interactor, interactable);
        }

        Debug.Log("Prefab spawned and attached to hand!");
    }
}
