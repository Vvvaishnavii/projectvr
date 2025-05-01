using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRGrabSpawnButton : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform handAttachPoint;

    private void Awake()
    {
        var grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
        if (interactor != null)
        {
            GameObject spawned = Instantiate(prefabToSpawn, handAttachPoint.position, handAttachPoint.rotation);
            var grabInteractable = spawned.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
                interactor.interactionManager.SelectEnter(interactor, grabInteractable);
        }
    }
}