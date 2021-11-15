using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class ScaleUI : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Scale()
    {
        animator.SetBool("isScaleUI",true);
    }

    public void Descale()
    {
        animator.SetBool("isScaleUI", false);
    }
}
