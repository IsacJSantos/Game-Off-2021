using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] Canvas _cv;
    private void Awake()
    {
        Events.OnPlayerDie += ShowGameOverPanel;
        Events.OnAnthillDie += ShowGameOverPanel;
    }
    private void OnDestroy()
    {
        Events.OnPlayerDie -= ShowGameOverPanel;
        Events.OnAnthillDie -= ShowGameOverPanel;
    }
    // Start is called before the first frame update
    void Start()
    {
        _cv.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    void ShowGameOverPanel() 
    {
        _cv.enabled = true;
        Time.timeScale = 0;
    }
}
