using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrewDriver : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float risePerDegree = 0.001f;
    public float activationHeight = 0.1f;
    public XRGrabInteractable screwGrab;

    private float currentRotation = 0f;
    private Vector3 initialLocalPosition;
    private bool isActivated = false;

    public ScrewManager manager;               // 👈 Still named ScrewManager
    public XRGrabInteractable relatedBox;      // 👈 Box this screw is linked to

    public AudioSource screwAudioSource;       // 🎵 Add this line

   void Start()
{
    initialLocalPosition = transform.localPosition;

    if (screwGrab != null)
        screwGrab.enabled = false;

    if (screwAudioSource != null)
    {
        Debug.Log("AudioSource is assigned.");
        Debug.Log("Assigned clip: " + screwAudioSource.clip?.name);
        screwAudioSource.Stop();
        screwAudioSource.playOnAwake = false;
    }
    else
    {
        Debug.LogWarning("No AudioSource assigned to screwAudioSource!");
    }
}


   public void RotateScrew(float angle)
{
    currentRotation += angle * rotationSpeed;

    transform.Rotate(0f, angle * rotationSpeed, 0f, Space.Self);

    float rise = currentRotation * risePerDegree;
    transform.localPosition = initialLocalPosition - transform.up * rise;

    // ✅ Play the sound when rotated
    if (screwAudioSource != null && !screwAudioSource.isPlaying)
    {
        screwAudioSource.Play();
        Debug.Log("Playing screw audio");
    }

    // ✅ When screw rises beyond activationHeight, hide it and call manager
    if (!isActivated && Mathf.Abs(Vector3.Dot(transform.localPosition - initialLocalPosition, transform.up)) > activationHeight)
    {
        isActivated = true;

        // ❌ Removed: screwGrab.enabled = true;

        // ✅ Hide the screw
        gameObject.SetActive(false);

        if (manager != null && relatedBox != null)
            manager.ScrewDetached(relatedBox);
    }
}

}
