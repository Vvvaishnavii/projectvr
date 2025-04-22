using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform handAttachPoint;

    public void SpawnObject(XRBaseInteractor interactor)
    {
        GameObject spawned = Instantiate(prefabToSpawn);
        spawned.transform.position = handAttachPoint.position;
        spawned.transform.rotation = handAttachPoint.rotation;

        var grabInteractable = spawned.GetComponent<XRGrabInteractable>();
        interactor.interactionManager.SelectEnter(interactor, grabInteractable);
    }
}
