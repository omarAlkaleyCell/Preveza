using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneDriver : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
