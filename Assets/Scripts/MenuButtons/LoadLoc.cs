using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLoc : MonoBehaviour
{
    public void Load()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(PlayerPrefs.GetString("location"));
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Introduction cutscene");
    }

    public void Scene(string location)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(location);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
