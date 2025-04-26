using UnityEngine;

public class CrankLiftVR : MonoBehaviour
{
    public Transform crankWheel;    // The rotating wheel
    public Transform engine;        // The engine to lift
    public float liftPerRotation = 0.5f; // Amount to lift per full rotation

    private float previousYRotation;

    void Start()
    {
        previousYRotation = crankWheel.localEulerAngles.y;
    }

    void Update()
    {
        float currentYRotation = crankWheel.localEulerAngles.y;
        float deltaRotation = Mathf.DeltaAngle(previousYRotation, currentYRotation);

        float liftAmount = (deltaRotation / 360f) * liftPerRotation;
        engine.position += new Vector3(0, liftAmount, 0);

        previousYRotation = currentYRotation;
    }
}
