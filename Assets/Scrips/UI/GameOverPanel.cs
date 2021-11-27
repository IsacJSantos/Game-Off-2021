using System.Collections;
using System.Collections.Generic;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowGameOverPanel() 
    {
        _cv.enabled = true;
        Time.timeScale = 0;
    }
}
