using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// 🧩 Put the helper class outside the main class
[System.Serializable]
public class ScrewGroup
{
    public GameObject boxToUnlock;
    public List<ScrewDriver> screws = new();
    [HideInInspector] public bool isUnlocked = false;
}

public class ScrewManager : MonoBehaviour
{
    public List<ScrewGroup> screwGroups = new();

    public void CheckAllGroups()
    {
        foreach (ScrewGroup group in screwGroups)
        {
            if (group.isUnlocked) continue;

            bool allDetached = true;
            foreach (ScrewDriver screw in group.screws)
            {
                if (!screw.IsDetached())
                {
                    allDetached = false;
                    break;
                }
            }

            if (allDetached)
            {
                group.isUnlocked = true;
                if (group.boxToUnlock.TryGetComponent(out XRGrabInteractable grab))
                {
                    grab.enabled = true;
                    Debug.Log($"{group.boxToUnlock.name} is now unlocked!");
                }
            }
        }
    }
}
