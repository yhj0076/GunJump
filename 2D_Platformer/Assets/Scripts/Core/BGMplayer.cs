using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMplayer : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] BGM;

    public int BgmIndex = 0;

    AudioSource BgmAudioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        BgmAudioPlayer = GetComponent<AudioSource>();
        BgmIndex = 0;
        BgmAudioPlayer.loop = true;
        playMusic();
    }

    public void NextTrack()
    {
        BgmAudioPlayer.Stop();
        BgmIndex++;
        playMusic();
    }

    void playMusic()
    {
        BgmAudioPlayer.clip = BGM[BgmIndex];
        BgmAudioPlayer.Play();
        //BgmAudioPlayer.PlayOneShot(BGM[BgmIndex]);
        BgmAudioPlayer.loop = true;
    }

    public void pauseMusic()
    {
        BgmAudioPlayer.Pause();
    }
}
