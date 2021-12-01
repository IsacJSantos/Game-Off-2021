using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSpecialManager : MonoBehaviour
{
    [SerializeField] Canvas _cv;

    private void Awake()
    {
        Events.OnShowChooseSpecialPanel += ShowPanel;
    }

    private void OnDestroy()
    {
        Events.OnShowChooseSpecialPanel -= ShowPanel;
    }
    private void Start()
    {
        ShowPanel();
    }

    void ShowPanel()
    {
        _cv.enabled = true;
        Time.timeScale = 0;
    }

    public void ChooseOne(int specialType)
    {
        _cv.enabled = false;
        Time.timeScale = 1;
        switch (specialType)
        {
            case 0:
                Events.OnChooseSpecial?.Invoke(SpecialType.Healing);
                break;
            case 1:
                Events.OnChooseSpecial?.Invoke(SpecialType.Bomb);
                break;
            case 2:
                Events.OnChooseSpecial?.Invoke(SpecialType.SuperShot);
                break;
            default:
                break;
        }
        Events.OnStartWaves?.Invoke();
    }
   
}
