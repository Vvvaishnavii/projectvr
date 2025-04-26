using UnityEngine;

public class HingeLiftControl : MonoBehaviour
{
    [Header("Lever Settings")]
    public Transform lever; // Lever that rotates on local X
    public float minLeverAngleX = 0f; // Minimum rotation (degrees)
    public float maxLeverAngleX = 90f; // Maximum rotation (degrees)

    [Header("Lift Platform Settings")]
    public Transform liftPlatform; // Platform that moves in local Y
    public float minPlatformY = 0f;
    public float maxPlatformY = 2f;
    public float liftSpeed = 2f;

    private void Start()
    {
        // Make sure lift platform starts at bottom position
        Vector3 localPos = liftPlatform.localPosition;
        liftPlatform.localPosition = new Vector3(localPos.x, minPlatformY, localPos.z);
    }

    void Update()
    {
        // Read lever's local X rotation
        float leverX = lever.localEulerAngles.x;

        // Because rotations can go 0-360, we may need to fix it
        if (leverX > 180f)
            leverX -= 360f;

        // Clamp lever rotation between min and max
        leverX = Mathf.Clamp(leverX, minLeverAngleX, maxLeverAngleX);

        // Map lever X rotation to 0-1 range
        float t = Mathf.InverseLerp(minLeverAngleX, maxLeverAngleX, leverX);

        // Calculate target platform Y
        float targetY = Mathf.Lerp(minPlatformY, maxPlatformY, t);

        // Smoothly move lift platform
        Vector3 currentLocalPos = liftPlatform.localPosition;
        Vector3 targetLocalPos = new Vector3(currentLocalPos.x, targetY, currentLocalPos.z);
        liftPlatform.localPosition = Vector3.Lerp(currentLocalPos, targetLocalPos, Time.deltaTime * liftSpeed);
    }
}