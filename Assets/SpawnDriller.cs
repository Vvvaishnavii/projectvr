using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnDriller : MonoBehaviour
{
    public GameObject drillerPrefab;
    public Transform rightHandAttachPoint;

    private void OnTriggerEnter(Collider other)
    {
        XRDirectInteractor interactor = other.GetComponent<XRDirectInteractor>();
        if (interactor != null)
        {
            Debug.Log("Button touched by: " + other.name);
            SpawnInHand(interactor);
        }
    }

    private void SpawnInHand(XRDirectInteractor interactor)
    {
        // Safety check
        if (interactor.selectTarget != null)
        {
            Debug.Log("Already holding something.");
            return;
        }

        GameObject driller = Instantiate(drillerPrefab, rightHandAttachPoint.position, rightHandAttachPoint.rotation);
        XRGrabInteractable interactable = driller.GetComponent<XRGrabInteractable>();

        if (interactable != null)
        {
            interactor.interactionManager.SelectEnter(interactor, interactable);
            Debug.Log("Prefab spawned and grabbed!");
        }
    }
}
