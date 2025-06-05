using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource soundPoint;
    [SerializeField] private AudioSource soundScribble;
    [SerializeField] private AudioMixer audioMixer;
    private Slider musicSlider;
    private Slider sfxSlider;


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
    /// Reads music volume of the slider and sets the value in the audio mixer.
    /// </summary>
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    /// <summary>
    /// Reads sfx volume of the slider and sets the value in the audio mixer.
    /// </summary>
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    /// <summary>
    /// Plays beep sound while scales field bigger.
    /// </summary>
    public void PlaySoundPoint()
    {
        soundPoint.PlayOneShot(soundPoint.clip);
    }

    /// <summary>
    /// Initializes the music slider on the at the momement running scene.
    /// </summary>
    /// <param name="slider"></param>
    public void InitializeMusicSlider(Slider slider)
    {
        musicSlider = slider;

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float volume = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = volume;
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        }
        else
        {
            SetMusicVolume();
        }
    }
    
    /// <summary>
    /// Initializes the sfx slider on the at the momement running scene.
    /// </summary>
    /// <param name="slider"></param>
    public void InitializeSFXSlider(Slider slider)
    {
        sfxSlider = slider;

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float volume = PlayerPrefs.GetFloat("SFXVolume");
            sfxSlider.value = volume;
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        }
        else
        {
            SetSFXVolume();
        }
    }
}
