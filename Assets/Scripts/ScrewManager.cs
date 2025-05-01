using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrewManager : MonoBehaviour
{
    public int totalScrews = 2; // 👈 or leave 0 to auto-detect
    private int detachedScrews = 0;

    public XRGrabInteractable boxToUnlock; // 👈 Drag the box here

    void Start()
    {
        // Optional auto-detect all screws in scene
        if (totalScrews == 0)
        {
            totalScrews = FindObjectsOfType<ScrewDriver>().Length;
        }

        if (boxToUnlock != null)
        {
            boxToUnlock.enabled = false; // Disable grabbing until all screws detached
        }
    }

    public void ScrewDetached()
    {
        detachedScrews++;

        if (detachedScrews >= totalScrews && boxToUnlock != null)
        {
            Debug.Log("All screws detached! Box can be removed.");
            boxToUnlock.enabled = true;
        }
    }
}
