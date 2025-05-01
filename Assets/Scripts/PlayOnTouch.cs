using UnityEngine;

public class PlayOnTouch : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand")) // Hand ya controller ko tag karo
        {
            animator.SetTrigger("PlayOnTouch");
        }
    }
}