using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SliderBehaviour : MonoBehaviour
{
    public FloatVariable floatvar;
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        floatvar.Value = 5;
        slider.value = floatvar.Value;
    }
    
    public void SetValue()
    {
        floatvar.Value = slider.value;
    }    
}
