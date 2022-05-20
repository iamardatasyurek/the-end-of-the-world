using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSlider : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] Slider slider;
    void Update()
    {
        audio.volume = slider.value;
    }
}
