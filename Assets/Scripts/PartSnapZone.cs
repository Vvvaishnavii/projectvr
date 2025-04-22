using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSnapZone : MonoBehaviour
{
  [System.Serializable]
    public class PartInfo
    {
        public string expectedTag;
        public GameObject assembledPart; // The fixed version (initially inactive)
    }

    public List<PartInfo> partsList;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);

        foreach (PartInfo part in partsList)
        {
            if (other.CompareTag(part.expectedTag))
            {
                Debug.Log("Correct part dropped!");

                other.gameObject.SetActive(false); // Hide disassembled
                part.assembledPart.SetActive(true); // Show assembled
                return; // Exit after first match
            }
        }

        Debug.Log("Wrong part. No matching expectedTag found for: " + other.tag);
    }
}
