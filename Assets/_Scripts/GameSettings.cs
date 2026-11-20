using UnityEngine;
using UnityEngine.Audio;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    [SerializeField] private bool useScreenShake = true;
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private float musicVolume = 1f;
    [SerializeField] private float effectVolume = 1f;


    public bool UseScreenShake
    {
        get => useScreenShake;
        set => useScreenShake = value;
    }
    public float MasterVolume
    {
        get => masterVolume;
        set => masterVolume = value;
    }
    public float EffectVolume
    {
        get => effectVolume;
        set => effectVolume = value;
    }
    public float MusicVolume
    {
        get => musicVolume;
        set => musicVolume = value;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        if(mainMixer == null)
        {
            mainMixer = AllMixerGroups[0].audioMixer;

        }
    }
    public void LoadAwake()
    {
        Awake();
    }
    public void SetUseScreenShake(bool useShake)
    {
        useScreenShake = useShake;
        PlayerPrefs.SetInt("UseScreenShake", useScreenShake ? 1 : 0);
        PlayerPrefs.Save();
    }



    // Call this from a UI Slider (On Value Changed) or another script
    // value should be between 0.0001f and 1.0f
    public void SetMasterVolume(float value)
    {
        // Convert linear 0-1 value to logarithmic decibels (-80dB to 0dB)
        masterVolume = value;
        float linearValue = value / 100f;
        float dbValue = Mathf.Log10(Mathf.Max(0.0001f, linearValue)) * 20f;
        mainMixer.SetFloat("MasterVolume", dbValue);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.Save();
    }

    public void SetEffectsVolume(float value)
    {
        effectVolume = value;
        float linearValue = value / 100f;
        float dbValue = Mathf.Log10(Mathf.Max(0.0001f, linearValue)) * 20f;
        mainMixer.SetFloat("EffectsVolume", dbValue);
        PlayerPrefs.SetFloat("EffectVolume", effectVolume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        float linearValue = value / 100f;
        float dbValue = Mathf.Log10(Mathf.Max(0.0001f, linearValue)) * 20f;
        mainMixer.SetFloat("MusicVolume", dbValue);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    public void SavePlayerPrefsSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("EffectVolume", effectVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetInt("UseScreenShake", useScreenShake ? 1 : 0);
        PlayerPrefs.Save();
    }

    public AudioMixerGroup[] AllMixerGroups
    {
        get
        {
            return mainMixer.FindMatchingGroups(string.Empty);
            ;
        }
    }
}
