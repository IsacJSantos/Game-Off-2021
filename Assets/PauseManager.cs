using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] Canvas _cv;
    bool _isOpen;

    private void Start()
    {
        _cv.enabled = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _cv.enabled = true;
    }

    public void PLayGame()
    {
        Time.timeScale = 1;
        _cv.enabled = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
