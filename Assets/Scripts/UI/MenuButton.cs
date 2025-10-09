using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class MenuButton : MonoBehaviour
{
    [SerializeField] private bool locked;
    [SerializeField] private Canvas[] hideCanvas;
    [SerializeField] private Canvas showCanvas;

    private AudioSource audioSource;
    [SerializeField] private AudioClip press;
    [SerializeField] private AudioClip error;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (locked)
        {
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        else
        {
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    virtual public void OnClick()
    {
        if (!locked)
        {
            for (int i = 0; i < hideCanvas.Length; i++)
            {
                hideCanvas[i].enabled = false;
            }
            showCanvas.enabled = true;
            audioSource.PlayOneShot(press);
        }
        else
        {
            audioSource.PlayOneShot(error);
        }

    }
}