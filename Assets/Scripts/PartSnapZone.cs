using System.Collections;
using UnityEngine;

public class PartSnapZone : MonoBehaviour
{
   public string expectedTag;
    public GameObject assembledPart; // The fixed version (initially inactive)

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);

        if (other.CompareTag(expectedTag))
        {
            Debug.Log("Correct part dropped!");

            other.gameObject.SetActive(false); // Hide disassembled
            assembledPart.SetActive(true);     // Show assembled
        }
        else
        {
            Debug.Log("Wrong part. Expected: " + expectedTag + ", Got: " + other.tag);
        }
    }
}
