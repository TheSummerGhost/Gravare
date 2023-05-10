using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider musicVolume;
    [SerializeField] private TMPro.TextMeshProUGUI tmp;

    public void volumeChange(float value)
    {
        AudioManager.instance.SetVolume(value);
        tmp.text = RoundFloat(value).ToString();

    }



    public void Start()
    {
        float audioManagerVolume = AudioManager.instance.GetVolume();
        
        tmp.text = RoundFloat(audioManagerVolume).ToString();
        musicVolume.value = audioManagerVolume;
        AudioManager.instance.SetVolume(audioManagerVolume);
    }

    public float RoundFloat(float value)
    {
        return Mathf.Round(value * 100f) / 100f;
        
    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }

}
