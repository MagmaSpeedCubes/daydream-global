using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    [SerializeField] protected int scene;
    public void LoadSceneClick()
    {
        SceneManager.LoadScene(scene);
    }
}
