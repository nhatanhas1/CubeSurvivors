using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{

    public Slider slider;

    public void SetUpBar(int maxValue,int startValue)
    {
        slider.maxValue = maxValue;
        slider.value = startValue;        
    }

    //public void SetStartBar(int startBar)
    //{
    //    slider.value = startBar;
    //}

    public void SetBar(int value)
    {
        slider.value = value;
    }
}
