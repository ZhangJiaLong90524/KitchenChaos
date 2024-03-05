using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PlayerPrefMusicVolume = "MusicVolume";
    private AudioSource _audioSource;
    public static int Volume { get; private set; } = 100;
    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();


        Volume = PlayerPrefs.GetInt(PlayerPrefMusicVolume, 100);


        UpdateMusicVolume();
    }

    public void ChangeVolume()
    {
        Volume += 10;
        if (Volume > 100)
        {
            Volume = 0;
        }


        PlayerPrefs.SetInt(PlayerPrefMusicVolume, Volume);
        PlayerPrefs.Save();


        UpdateMusicVolume();
    }

    private void UpdateMusicVolume()
    {
        _audioSource.volume = Volume * 0.01f;
    }
}