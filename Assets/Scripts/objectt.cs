using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class obj : MonoBehaviour
{
    public TMP_Text infoText;  // Link this from Canvas
    public XRGrabInteractable[] objectsToMonitor;  // Drag your grab objects here

    private void Start()
    {
        foreach (var obj in objectsToMonitor)
        {
            obj.selectEntered.AddListener((args) => ShowInfo(obj));
            obj.selectExited.AddListener((args) => ClearInfo());
        }
    }

    void ShowInfo(XRGrabInteractable obj)
    {
        if (infoText != null)
        {
            infoText.text = obj.name + " grabbed!";  // You can customize more here.
        }
    }

    void ClearInfo()
    {
        if (infoText != null)
        {
            infoText.text = "";
        }
    }
}
