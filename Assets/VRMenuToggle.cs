using UnityEngine;
using UnityEngine.InputSystem;

public class VRMenuToggle : MonoBehaviour
{
    public GameObject menu;  // Your Menu Canvas

    public InputActionProperty leftTrigger;  // Assigned to Left Hand Trigger

    private Vector3 targetScale = new Vector3(0.003f, 0.003f, 0.003f);
    private Vector3 hiddenScale = Vector3.zero;

    private void Start()
    {
        if (menu != null)
            menu.transform.localScale = hiddenScale;

        if (leftTrigger.action != null)
            leftTrigger.action.Enable();  // Make sure input is active
    }

    private void Update()
    {
        if (menu == null) return;

        if (leftTrigger.action.ReadValue<float>() > 0.5f)
        {
            menu.transform.localScale = targetScale;
        }
        else
        {
            menu.transform.localScale = hiddenScale;
        }
    }
}