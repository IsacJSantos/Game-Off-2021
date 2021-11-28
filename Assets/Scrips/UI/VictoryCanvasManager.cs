using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCanvasManager : MonoBehaviour
{
    [SerializeField] Canvas _cv;

    private void Start()
    {
        _cv.enabled = false;
    }
    private void Awake()
    {
        Events.OnWinGame += OnWinGame;
    }
    private void OnDestroy()
    {
        Events.OnWinGame -= OnWinGame;
    }
    void OnWinGame()
    {
        _cv.enabled = true;
    }
}
