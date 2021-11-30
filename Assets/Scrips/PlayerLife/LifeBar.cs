using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxLife(float life)
    {
        slider.maxValue = life;
        slider.value = life;

        fill.color =  gradient.Evaluate(1f);
    }

    public void SetLife(float life)
    {
        slider.value = life;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
