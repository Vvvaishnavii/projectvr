using UnityEngine;

public class HingeLiftControl : MonoBehaviour
{
    public Transform lever; // the lever you move in the X direction
    public Rigidbody liftPlatform; // the lift platform that will move up/down
    public float liftSpeed = 2f; // speed at which the platform moves
    public float minLeverPositionX = 0f; // minimum X position of the lever
    public float maxLeverPositionX = 1f; // maximum X position of the lever

    void FixedUpdate()
    {
        // Get the current X position of the lever
        float leverPositionX = lever.localPosition.x;

        // Ensure the lever position stays within the bounds
        leverPositionX = Mathf.Clamp(leverPositionX, minLeverPositionX, maxLeverPositionX);

        // Map the lever's X position to the lift platform's Y position (from 0 to 2 units high)
        float t = Mathf.InverseLerp(minLeverPositionX, maxLeverPositionX, leverPositionX);
        float targetHeight = Mathf.Lerp(0f, 2f, t); // Adjust the platform height based on the lever's X position

        // Move the platform vertically (only modify Y position)
        Vector3 targetPosition = new Vector3(
            liftPlatform.position.x,  // Keep the current X position of the platform
            targetHeight,              // Adjust the Y position of the platform (move up/down)
            liftPlatform.position.z   // Keep the current Z position of the platform
        );

        // Smoothly move the platform up and down based on the lever's X position
        liftPlatform.MovePosition(Vector3.Lerp(liftPlatform.position, targetPosition, Time.fixedDeltaTime * liftSpeed));
    }
}
