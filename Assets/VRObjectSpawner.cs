using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform handAttachPoint;

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
        if (interactor != null)
        {
            SpawnObject(interactor);
        }
    }

public void SpawnObject(XRBaseInteractor interactor)
{
    GameObject spawned = Instantiate(prefabToSpawn);
    spawned.transform.position = handAttachPoint.position;
    spawned.transform.rotation = handAttachPoint.rotation;

    var grabInteractable = spawned.GetComponent<XRGrabInteractable>();
    if (grabInteractable != null)
    {
        StartCoroutine(WaitAndGrab(interactor, grabInteractable));
    }
}

private IEnumerator WaitAndGrab(XRBaseInteractor interactor, XRGrabInteractable grabInteractable)
{
    yield return null; // wait one frame
    interactor.interactionManager.SelectEnter(interactor, grabInteractable);
}

}