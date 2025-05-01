using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrewManager : MonoBehaviour
{
    [System.Serializable]
    public class ScrewBoxPair
    {
        public XRGrabInteractable boxToUnlock;
        public int totalScrews = 0;

        [HideInInspector] public int detachedScrews = 0;
    }

    public List<ScrewBoxPair> screwBoxes = new List<ScrewBoxPair>();

    void Start()
    {
        foreach (var pair in screwBoxes)
        {
            if (pair.totalScrews == 0)
                pair.totalScrews = FindObjectsOfType<ScrewDriver>().Length;

            if (pair.boxToUnlock != null)
                pair.boxToUnlock.enabled = false;
        }
    }

    public void ScrewDetached(XRGrabInteractable relatedBox)
    {
        foreach (var pair in screwBoxes)
        {
            if (pair.boxToUnlock == relatedBox)
            {
                pair.detachedScrews++;

                if (pair.detachedScrews >= pair.totalScrews && pair.boxToUnlock != null)
                {
                    Debug.Log($"All screws detached for {relatedBox.name}. You can now remove the box.");
                    pair.boxToUnlock.enabled = true;
                }

                break;
            }
        }
    }
}
