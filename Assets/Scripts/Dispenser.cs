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

    [SerializeField] private GameObject[] animatedItems;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        AnimateIn();
        audioSource.PlayOneShot(enterSound);
        Debug.Log("Player entered dispenser zone");
    }

    void AnimateIn()
    {
        for (int i = 0; i < animatedItems.Length; i++)
        {
            EaseMovement em = animatedItems[i].GetComponent<EaseMovement>();
            em.MoveToSecondPos();
        }
    }

    void AnimateOut()
    {
        for (int i = 0; i < animatedItems.Length; i++)
        {
            EaseMovement em = animatedItems[i].GetComponent<EaseMovement>();
            em.MoveToFirstPos();
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        AnimateOut();
        audioSource.PlayOneShot(exitSound);
        Debug.Log("Player exited dispenser zone");
    }
}
