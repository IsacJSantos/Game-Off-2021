using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void BackToMenu() 
    {
        SceneManager.LoadScene(0);
    }
}
