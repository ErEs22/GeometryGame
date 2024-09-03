using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource audioSource_BGM;
    private AudioSource audioSource_SFX;
    private AudioData currentBGMAudioData;
    private AudioData currentSFXAudioData;
    [Header("AudioClips")]
    public AudioData menuBGM;
    public AudioData inGameBGM;
    public AudioData weaponFireSFX_1;
    public AudioData weaponFireSFX_2;
    public AudioData weaponFireSFX_3;
    public AudioData weaponFireSFX_4;
    public AudioData explodeSFX;
    public AudioData expCollectSFX;
    public AudioData gameoverSFX;
    public AudioData hoverAudioSFX;
    public AudioData clickAudioSFX;

    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource_BGM = transform.Find("AudioSource_BGM").GetComponent<AudioSource>();
        audioSource_SFX = transform.Find("AudioSource_SFX").GetComponent<AudioSource>();
    }

    public void PlayBGM(AudioData data)
    {
        if(data.clip == null) return;
        currentBGMAudioData = data;
        if(audioSource_BGM.clip != null)
        {
            audioSource_BGM.Pause();
        }
        audioSource_BGM.loop = true;
        audioSource_BGM.clip = data.clip;
        audioSource_BGM.volume = data.volume * GameCoreData.GameSetting.backgroundVolume * GameCoreData.GameSetting.mainVolume;
        audioSource_BGM.Play();
    }

    public void PlaySFX(AudioData data)
    {
        if(data.clip == null) return;
        currentSFXAudioData = data;
        audioSource_SFX.PlayOneShot(data.clip,data.volume);
    }

    public void UpdateAudioInfo()
    {
        if(currentBGMAudioData != null)
        {
            audioSource_BGM.volume = currentBGMAudioData.volume * GameCoreData.GameSetting.backgroundVolume * GameCoreData.GameSetting.mainVolume;
        }
        audioSource_SFX.volume = GameCoreData.GameSetting.soundEffectVolume * GameCoreData.GameSetting.mainVolume;
    }

}