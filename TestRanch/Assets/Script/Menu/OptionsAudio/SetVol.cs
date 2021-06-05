using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//Crédits : Marc-André Larouche

public class SetVol : MonoBehaviour
{

    [SerializeField] private AudioMixer audioM;
    [SerializeField] private string nameParameter;

    private Slider slide;
    void Start()
    {
        slide = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(nameParameter, 0);
        SetVolume(v);
    }

    public void SetVolume(float vol)
    {
        audioM.SetFloat(nameParameter, vol);
        slide.value = vol;
        PlayerPrefs.SetFloat(nameParameter, vol);
    }

}
