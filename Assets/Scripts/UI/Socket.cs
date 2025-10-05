using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class Socket : MonoBehaviour, IDropHandler
{
    /*
    for ICPU:
    -1 = socket closed empty
    0 = opened, cpu can be installed here
    1 = cpu installed
    2 = socket closed with cpu inside
    3 = thermal paste applied
    4 = cooler mounted
    
    */
    
    [SerializeField] private string componentType;
    [SerializeField] private int installIndex; 
    [SerializeField] private int removeIndex;
    [SerializeField] private int startIndex;
    [SerializeField] private BuildHUD buildHUD; // reference to the BuildHUD script to display error messages
    [SerializeField] private string[] errorMessages; // error messages to display when trying to drop an item into a locked socket
    public int currentIndex; //the current status of this socket with 0 being filled 

    [SerializeField] private AudioClip installSound;
    [SerializeField] private AudioClip removeSound;
    [SerializeField] private AudioClip errorSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentIndex = startIndex;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop in Socket");
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragDrop>().itemType == componentType && currentIndex == installIndex)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            currentIndex++;
            audioSource.PlayOneShot(installSound);
        }
        else if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<DragDrop>().itemType == componentType && currentIndex == removeIndex)
        {
            currentIndex--;
            audioSource.PlayOneShot(removeSound);
        }
        else
        {
            // Start highlight coroutine
            StartCoroutine(HighlightCoroutine(Color.red, 0.2f, 0.5f));
            buildHUD.ShowPopup("Error", "Cannot Place Item", errorMessages[currentIndex - startIndex], 3);

        }

    }



    private IEnumerator HighlightCoroutine(Color highlightColor, float fadeDuration, float highlightDuration)
    {
        Image sr = GetComponent<Image>();
        Color originalColor = sr.color;

        // Fade to highlight color
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            sr.color = Color.Lerp(originalColor, highlightColor, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = highlightColor;

        // Hold highlight color
        yield return new WaitForSeconds(highlightDuration);

        // Fade back to original color
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            sr.color = Color.Lerp(highlightColor, originalColor, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = originalColor;
    }
    
    


}
