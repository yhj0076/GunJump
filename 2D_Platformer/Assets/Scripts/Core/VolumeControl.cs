using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeControl : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider audioSlider;
    
    public void AudioControl(string Vol)
    {
        float volume = audioSlider.value;

        if(volume == -40f)
        {
            masterMixer.SetFloat(Vol,-80);
        }
        else
        {
            masterMixer.SetFloat(Vol, volume);
        }
    }

    public void muteOn()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 0 : 1;
    }
}
