using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    [SerializeField] int sceneId;

    
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneId);
    }
}
