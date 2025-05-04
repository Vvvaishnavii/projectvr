using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PickableObject : XRGrabInteractable
{
   public InfoDisplayManager infoManager;

    private void OnEnable()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
        }
    }

    private void OnDisable()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (infoManager != null)
        {
            infoManager.SetCurrentObject(gameObject);
        }
    }
}
