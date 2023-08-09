using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    public int Volume { get; private set; } = 3;

    private AudioSource _audioSource;

    private const string PlayerPrefsMusicVolume = "MusicVolume";
    
    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();

        Volume = PlayerPrefs.GetInt(PlayerPrefsMusicVolume, 3);
        SetAudioSourceVolume();
    }
    
    public void ChangeVolume()
    {
        Volume += 1;

        if (Volume > 10)
        {
            Volume = 0;
        }
        
        PlayerPrefs.SetInt(PlayerPrefsMusicVolume, Volume);
        PlayerPrefs.Save();
        
        SetAudioSourceVolume();
    }

    private void SetAudioSourceVolume()
    {
        _audioSource.volume = Volume / 10f;
    }
}
