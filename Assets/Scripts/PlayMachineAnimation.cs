using UnityEngine;

public class PlayMachineAnimation : MonoBehaviour
{
    public Animator pulley1Animator;
    public Animator pulley2Animator;
    public Animator engineAnimator;

    public AudioSource machineAudio; // 🎵 Drag your audio source here

    private bool hasPlayed = false;

    void Update()
    {
        // Trigger on 'H' key press
        if (Input.GetKeyDown(KeyCode.H) && !hasPlayed)
        {
            // ✅ Play all animations
            pulley1Animator.SetTrigger("Play");
            pulley2Animator.SetTrigger("Play");
            engineAnimator.SetTrigger("Play");

            // ✅ Play audio
            if (machineAudio != null)
            {
                machineAudio.Play();
            }

            hasPlayed = true;
        }
    }
}
