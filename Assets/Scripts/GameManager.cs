using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject LoseWindow;
    public static GameManager instance;
    public GameObject FailPicture;
    public AudioSource explosionAudio;
    private void Start()
    {
        instance = this;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Lose()
    {
        FailPicture.SetActive(true);
        explosionAudio.Play();
        LoseWindow.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        Time.timeScale = 1;
    }
}
