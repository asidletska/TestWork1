using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public GameObject panelPause;
    public void PauseButtonPressed()
    {
        Time.timeScale = 0f;
        panelPause.SetActive(true);
    }
    public void ContinueHandler()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1.0f;

    }
    public void RestartHandler()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
