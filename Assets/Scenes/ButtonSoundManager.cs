using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonSoundManager : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip hoverClip;
    public AudioClip clickClip;
    //public Scene scene;

    // When the mouse hovers over the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(hoverClip);
    }

    // When the button is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.PlayOneShot(clickClip);
        // Load new scene
        SceneManager.LoadScene("SampleScene");
    }
}
