using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager I;
    
    private AudioSource bgmAudioSource;
    private AudioSource sfxAudioSource;
    
    [SerializeField] private AudioClip[] bgmClipList;

    [SerializeField] private AudioClip clickClip;
    
    [SerializeField] private AudioClip upgradeActiveClip;
    [SerializeField] private AudioClip upgradeClip;
    
    [SerializeField] private AudioClip hitClip;
    
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private AudioClip arrowClip;
    [SerializeField] private AudioClip bulletClip;
    [SerializeField] private AudioClip knifeClip;
    
    [SerializeField] private AudioClip clearClip;
    [SerializeField] private AudioClip gameOverClip;
    
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    
    [Header("Volume")]
    public float BGMVolume = 1;
    public float SFXVolume = 1;
    
    [Header("Settings Sound Sliders")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        I = this;
        
        bgmAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
        sfxAudioSource = transform.GetChild(1).GetComponent<AudioSource>();
        
        
        if (PlayerPrefs.HasKey("BGMVolume"))
            BGMVolume = PlayerPrefs.GetFloat("BGMVolume");
        else
            BGMVolume = 1;

        if (PlayerPrefs.HasKey("SFXVolume"))
            SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        else
            SFXVolume = 1;
        
        #region Sound Settings
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        #endregion
    }
    
    private void Start()
    {
        bgmSlider.value = BGMVolume;
        sfxSlider.value = SFXVolume;  
        
        SetBGMVolume(BGMVolume);
        SetSFXVolume(SFXVolume);
        
        PlayBGM(0);
    }

    private void OnBGMVolumeChanged(float value)
    {
        SetBGMVolume(value);
    }
    
    private void OnSFXVolumeChanged(float value)
    {
        SetSFXVolume(value);
    }

    #region Volume
    public void SetBGMVolume(float volume)
    {
        BGMVolume = volume;
        
        audioMixer.SetFloat("BGM", Mathf.Log10(BGMVolume) * 20);
        
        PlayerPrefs.SetFloat("BGMVolume", BGMVolume);
    }

    public void SetSFXVolume(float volume)
    {
        SFXVolume = volume;
        
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXVolume) * 20);
        
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
    }
    
    private void SmoothVolume(AudioSource audioSource, float volume, float duration)
    {
        StartCoroutine(SmoothVolumeCoroutine(audioSource, volume, duration));
    }
    
    private IEnumerator SmoothVolumeCoroutine(AudioSource audioSource, float volume, float duration)
    {
        float startVolume = audioSource.volume;
        float startTime = Time.time;
        
        while (Time.time < startTime + duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, volume, (Time.time - startTime) / duration);
            yield return null;
        }
        
        audioSource.volume = volume;
    }
    
    public void FadeOutBGM(float duration)
    {
        if (bgmAudioSource == null)
            return;
        
        SmoothVolume(bgmAudioSource, 0, duration);
    }

    public void FadeInBGM(float duration)
    {
        if (bgmAudioSource == null)
            return;
        
        SmoothVolume(bgmAudioSource, 1, duration);
    }
    
    #endregion

    private bool Check()
    {
        if (GameManager.I.playerBodyCollider.IsDead())
            return false;


        if (GameManager.I.upgradeHandler.IsShowing())
            return false;

        return true;
    }
    public void PlayBGM(int index)
    {
        if (bgmClipList.Length <= index)
        {
            Debug.LogWarning("BGM Clip is null");
            return;
        }
        
        bgmAudioSource.clip = bgmClipList[index];
        bgmAudioSource.Play();
    }

    
    public void PlayClick()
    {
        if (clickClip == null)
        {
            Debug.LogWarning("Click Clip is null");
            return;
        }
        sfxAudioSource.PlayOneShot(clickClip);
    }
    
    public void PlayUpgradeActive()
    {
        if (upgradeActiveClip == null)
        {
            Debug.LogWarning("UpgradeActive Clip is null");
            return;
        }
        sfxAudioSource.PlayOneShot(upgradeActiveClip);
    }
    
    public void PlayUpgrade()
    {
        if (upgradeClip == null)
        {
            Debug.LogWarning("Upgrade Clip is null");
            return;
        }
        sfxAudioSource.PlayOneShot(upgradeClip);
    }
    
    public void PlayHit()
    {
        if (!Check())
            return;

        if (hitClip == null)
        {
            Debug.LogWarning("Hit Clip is null");
            return;
        }
        sfxAudioSource.PlayOneShot(hitClip);
    }

    public void PlayFire()
    {
        if (!Check())
            return;

        if (fireClip == null)
        {
            Debug.LogWarning("Fire Clip is null");
            return;
        }
        sfxAudioSource.PlayOneShot(fireClip);
    }
    
    public void PlayArrow()
    {
        if (!Check())
            return;

        if (arrowClip == null)
        {
            Debug.LogWarning("Arrow Clip is null");
            return;
        }
        sfxAudioSource.PlayOneShot(arrowClip);
    }
    
    public void PlayBullet()
    {
        if (!Check())
            return;

        if (bulletClip == null)
        {
            Debug.LogWarning("Bullet Clip is null");
            return;
        }
        sfxAudioSource.PlayOneShot(bulletClip);
    }
    
    public void PlayKnife()
    {
        if (!Check())
            return;

        if (knifeClip == null)
        {
            Debug.LogWarning("Knife Clip is null");
            return;
        }
        sfxAudioSource.PlayOneShot(knifeClip);
    }
    
    public void PlayClear()
    {
        if (clearClip == null)
        {
            Debug.LogWarning("Clear Clip is null");
            return;
        }
        sfxAudioSource.PlayOneShot(clearClip);
    }
    
    public void PlayGameOver()
    {
        if (gameOverClip == null)
        {
            Debug.LogWarning("GameOver Clip is null");
            return;
        }
        sfxAudioSource.PlayOneShot(gameOverClip);
    }
}
