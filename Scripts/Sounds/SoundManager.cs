using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundeffectsVolume"; 

    private float volume =1f;
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += Instance_OnRecipeFail;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickupSomeThing += Instance_OnPickupSomeThing;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyTrashed += TrashCounter_OnAnyObjectPlacedHere;
    }

    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }
    private void TrashCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Instance_OnPickupSomeThing(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectFickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void Instance_OnRecipeFail(object sender, System.EventArgs e)
    {
        DeliveryManager deliveryManager = DeliveryManager.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryManager.transform.position);
    }

    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryManager deliveryManager = DeliveryManager.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryManager.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 posiotion, float Volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], posiotion, Volume);
    } 
    private void PlaySound(AudioClip audioClip,Vector3 posiotion, float VolumeMultiplayer = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, posiotion, VolumeMultiplayer * volume);
    }

    public void FootStepsSound(Vector3 position, float Volume)
    {
        PlaySound(audioClipRefsSO.footStep, position, Volume);
    }  
    public void CountDownSound()
    {
        PlaySound(audioClipRefsSO.warning,Vector3.zero);
    } 
    public void PlayWarningSound(Vector3 Postion)
    {
        PlaySound(audioClipRefsSO.warning,Vector3.zero);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if(volume>1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume;
    }
}
