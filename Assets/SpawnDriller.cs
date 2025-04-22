using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnDriller : MonoBehaviour
{
    public GameObject drillerPrefab;
    public Transform rightHandAttachPoint;  // Drag your right-hand controller here!

    public void SpawnInHand()
    {
        GameObject driller = Instantiate(drillerPrefab, rightHandAttachPoint.position, rightHandAttachPoint.rotation);
        XRGrabInteractable interactable = driller.GetComponent<XRGrabInteractable>();

        if (interactable != null)
        {
            XRDirectInteractor interactor = rightHandAttachPoint.GetComponentInParent<XRDirectInteractor>();
            if (interactor != null)
            {
                interactor.interactionManager.SelectEnter(interactor, interactable);  // Attach to hand immediately
            }
        }
    }
}