using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

using UnityEngine.XR;


public class InfoDisplayManager : MonoBehaviour
{
    public GameObject infoPanel;
    public Text nameText;
    public Text descriptionText;

    private ObjectInfo currentObjectInfo;

    public void SetCurrentObject(GameObject pickedObject)
    {
        currentObjectInfo = pickedObject.GetComponent<ObjectInfo>();
    }

void Update()
{
    // Find the left controller
    InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

    // Try to get the button press state
    bool isPressed;
    if (leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out isPressed) && isPressed)
    {
        if (currentObjectInfo != null)
        {
            infoPanel.SetActive(true);
            nameText.text = currentObjectInfo.objectName;
            descriptionText.text = currentObjectInfo.objectDescription;
        }
    }
    else
    {
        infoPanel.SetActive(false);
    }
}

}
