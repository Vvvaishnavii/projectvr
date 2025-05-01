using UnityEngine;

public class PulleyHook : MonoBehaviour
{
    public bool isHooked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Engine"))
        {
            isHooked = true;
            Debug.Log("✅ Hooked to engine!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Engine"))
        {
            isHooked = false;
            Debug.Log("❌ Unhooked from engine.");
        }
    }
}

