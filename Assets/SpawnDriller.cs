using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnDriller : MonoBehaviour
{
    public GameObject drillerPrefab;
    public Transform rightHandAttachPoint;  // Drag your right-hand controller's attach point here!
    public string handTag = "Hand";         // Tag for your hand/controller

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(handTag))
        {
            XRDirectInteractor interactor = other.GetComponent<XRDirectInteractor>();

            if (interactor != null)
            {
                SpawnInHand(interactor);
            }
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
