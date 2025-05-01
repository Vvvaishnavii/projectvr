using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnDriller : MonoBehaviour
{
    public GameObject drillerPrefab;
    public Transform rightHandAttachPoint;  // Drag your right-hand controller's attach point here!

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the right hand interactor
        if (other.GetComponent<XRDirectInteractor>())
        {
            SpawnInHand(other.GetComponent<XRDirectInteractor>());
        }
    }

    public void SpawnInHand(XRDirectInteractor interactor)
    {
        GameObject driller = Instantiate(drillerPrefab, rightHandAttachPoint.position, rightHandAttachPoint.rotation);
        XRGrabInteractable interactable = driller.GetComponent<XRGrabInteractable>();

        if (interactable != null)
        {
            interactor.interactionManager.SelectEnter(interactor, interactable);  // Attach to hand immediately
        }
    }
}
