using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class Dispenser : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] private AudioClip dispenseSound;
    [SerializeField] private AudioClip enterSound;
    [SerializeField] private AudioClip exitSound;

    [SerializeField] private GameObject[] leftAnimatedItems;

    [SerializeField] private GameObject[] rightAnimatedItems;
    private bool enterDirectionLeft;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 entryDirection = (transform.position - other.transform.position).normalized;
        if (Vector3.Dot(entryDirection, Vector3.left) > 0)
        {
            Debug.Log("Player entered from the left");
            AnimateIn(leftAnimatedItems);
            enterDirectionLeft = true;
        }
        else
        {
            Debug.Log("Player entered from the right");
            AnimateIn(rightAnimatedItems);
            enterDirectionLeft = false;
        }

        audioSource.PlayOneShot(enterSound);
        Debug.Log("Player entered dispenser zone");
    }

    void AnimateIn(GameObject[] animatedItems)
    {
        for (int i = 0; i < animatedItems.Length; i++)
        {
            EaseMovement em = animatedItems[i].GetComponent<EaseMovement>();
            em.MoveToSecondPos();
        }
    }

    void AnimateOut(GameObject[] animatedItems)
    {
        for (int i = 0; i < animatedItems.Length; i++)
        {
            EaseMovement em = animatedItems[i].GetComponent<EaseMovement>();
            em.MoveToFirstPos();
        }
    }


    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if(enterDirectionLeft)
        {
            AnimateOut(leftAnimatedItems);
        }
        else
        {
            AnimateOut(rightAnimatedItems);
        }

        audioSource.PlayOneShot(exitSound);
        Debug.Log("Player exited dispenser zone");
    }
}
