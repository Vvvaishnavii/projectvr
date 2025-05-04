using UnityEngine;
using UnityEngine.XR; // ✅ This is the XR namespace we want

public class PlayMachineAnimation : MonoBehaviour
{
    public Animator pulley1Animator;
    public Animator pulley2Animator;
    public Animator engineAnimator;

    public AudioSource machineAudio;

    private bool hasPlayed = false;

    void Update()
    {
        if (!hasPlayed && IsRightTriggerPressed())
        {
            pulley1Animator.SetTrigger("Play");
            pulley2Animator.SetTrigger("Play");
            engineAnimator.SetTrigger("Play");

            if (machineAudio != null)
                machineAudio.Play();

            hasPlayed = true;
        }
    }

    bool IsRightTriggerPressed()
    {
        UnityEngine.XR.InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed) && isPressed)
        {
            return true;
        }

        return false;
    }
}
