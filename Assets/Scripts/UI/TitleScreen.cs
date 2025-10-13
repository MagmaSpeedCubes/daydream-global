using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class TitleScreen : MonoBehaviour
{
    [SerializeField] protected AudioClip titleMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GetComponent<AudioSource>().PlayOneShot(titleMusic);
    }

    public void LoadChallengeMode()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadFreePlayMode()
    {

    }

    public void LoadTutorial()
    {
        Application.OpenURL("https://www.alex-li.xyz/game-dev/micro-builders/how-to-play");
    }


}
