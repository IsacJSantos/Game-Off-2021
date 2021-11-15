using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMenuController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowOptions()
    {
        HideMenu();
        animator.SetBool("isShowOptions", true);
    }
    public void HideOptions()
    {
        animator.SetBool("isShowOptions", false);
        ReturnToMenu();
    }

    public void ShowCredits()
    {
        HideMenu();
        animator.SetBool("isShowCredits", true);
    }
    public void HideCredits()
    {
        animator.SetBool("isShowCredits", false);
        ReturnToMenu();
    }

    public void ScaleAnt()
    {
        HideMenu();
        animator.SetBool("isPlayGame", true);
    }

    public void ReturnToMenu()
    {
        animator.SetBool("isHideMenu", false);
    }

    private void HideMenu()
    {
        animator.SetBool("isHideMenu",true);
    }


}
