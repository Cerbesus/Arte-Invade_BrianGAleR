using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ControladorNiebla : MonoBehaviour
{
    public PinchSlider sliderNiebla;
    public float minDensidad = 0.0f;
    public float maxDensidad = 0.5f;

    void Start()
    {
        // Establecer el valor inicial del slider a la densidad actual de la niebla
        sliderNiebla.SliderValue = RenderSettings.fogDensity;
    }

    public void CambiarDensidadNiebla()
    {
        // Cambiar la densidad de la niebla según el valor del slider
        RenderSettings.fogDensity = Mathf.Lerp(minDensidad, maxDensidad, sliderNiebla.SliderValue);
    }
}
