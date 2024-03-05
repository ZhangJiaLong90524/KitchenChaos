using System.Collections.Generic;
using Counter;
using ScriptableObject;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    private const string PlayerPrefSoundEffectsVolume = "SoundEffectsVolume";
    [SerializeField] private AudioClips audioClips;
    public static int Volume { get; private set; } = 100;
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;


        Volume = PlayerPrefs.GetInt(PlayerPrefSoundEffectsVolume, 100);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeFailed += (_, _)=>
        {
            PlaySound(audioClips.deliveryFail, DeliveryCounter.Instance.transform.position);
        };
        DeliveryManager.Instance.OnRecipeSuccess += (_, _)=>
        {
            PlaySound(audioClips.deliverySuccess, DeliveryCounter.Instance.transform.position);
        };
        CuttingCounter.OnCut += (sender, _)=>
        {
            PlaySound(audioClips.chop, ((CuttingCounter)sender).transform.position);
        };
        Player.Player.OnPickedSomething += (sender, _)=>
        {
            PlaySound(audioClips.objectPickup, ((Player.Player)sender).transform.position);
        };
        Counter.Counter.OnAnyObjectPlacedHere += (sender, _)=>
        {
            PlaySound(audioClips.objectDrop, ((Counter.Counter)sender).transform.position);
        };
        TrashCounter.OnAnyObjectTrashed += (sender, _)=>
        {
            PlaySound(audioClips.trash, ((TrashCounter)sender).transform.position);
        };
    }

    private static void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * Volume * 0.01f);
    }

    private static void PlaySound(IReadOnlyList<AudioClip> audioClipArray, Vector3 position,
        float volumeMultiplier = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Count)], position, volumeMultiplier);
    }

    public void PlayFootstepSound(Vector3 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClips.footstep, position, volumeMultiplier);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClips.warning, Vector3.zero);
    }

    public void PlayStoveBurnWarningSound()
    {
        PlaySound(audioClips.warning, Vector3.zero);
    }

    public static void ChangeVolume()
    {
        Volume += 10;

        if (Volume > 100)
        {
            Volume = 0;
        }


        PlayerPrefs.SetInt(PlayerPrefSoundEffectsVolume, Volume);
        PlayerPrefs.Save();
    }
}