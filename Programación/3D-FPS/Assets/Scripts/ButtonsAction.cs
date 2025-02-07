using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsAction : MonoBehaviour
{

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
