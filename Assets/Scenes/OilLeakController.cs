using UnityEngine;

public class OilLeakController : MonoBehaviour
{
    public ParticleSystem oilDripParticles;
    public GameObject oilPool;
    public float poolDelay = 3f;

    void Start()
    {
        oilDripParticles.gameObject.SetActive(false);
        oilPool.SetActive(false);
    }

    public void TriggerLeak()
    {
        oilDripParticles.gameObject.SetActive(true);
        oilDripParticles.Play();

        // Start pooling effect after delay
        Invoke(nameof(ShowOilPool), poolDelay);
    }

    void ShowOilPool()
    {
        oilPool.SetActive(true);
    }
}