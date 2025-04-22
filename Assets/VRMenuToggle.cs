using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VRMenuToggle : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty rightTrigger;

    private void Update()
    {
        if (rightTrigger.action.ReadValue<float>() > 0.5f) 
        {
            menu.SetActive(true);  // Show menu
        }
        else
        {
            menu.SetActive(false);  // Hide menu
        }
    }
}
