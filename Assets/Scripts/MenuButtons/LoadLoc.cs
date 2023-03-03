using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLoc : MonoBehaviour
{
    public void Load()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetString("location") == null)
            NewGame();
        SceneManager.LoadScene(PlayerPrefs.GetString("location"));
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetString("location", "Test Room");
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
