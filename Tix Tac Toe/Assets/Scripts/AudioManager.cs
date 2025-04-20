using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource soundX;
    [SerializeField] private AudioSource soundO;
    [SerializeField] private AudioSource soundPoint;
    [SerializeField] private AudioSource soundScribble;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    
    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject); 
        }

        Instance = this;
    }

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            SetMusicVolume();
        }
        else
        {
            SetMusicVolume();
        } 
        
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            SetSFXVolume();
        }
        else
        {
            SetSFXVolume();
        }
    }
    
    /// <summary>
    /// Sets music volume.
    /// </summary>
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    /// <summary>
    /// Sets sfx volume.
    /// </summary>
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    /// <summary>
    /// Plays sound while sets X.
    /// </summary>
    public void PlaySoundX()
    {
        soundX.PlayOneShot(soundX.clip);
    }
    
    /// <summary>
    /// Plays sound while sets O.
    /// </summary>
    public void PlaySoundO()
    {
        soundO.PlayOneShot(soundO.clip);
    }
    
    /// <summary>
    /// Plays beep sound while scales field bigger.
    /// </summary>
    public void PlaySoundPoint()
    {
        soundPoint.PlayOneShot(soundPoint.clip);
    }
}
