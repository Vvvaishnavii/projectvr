using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleVisibilityXR : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;

    private XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnPress);
    }

    private void OnPress(SelectEnterEventArgs args)
    {
        objectA.SetActive(false);
        objectB.SetActive(true);
    }
}
