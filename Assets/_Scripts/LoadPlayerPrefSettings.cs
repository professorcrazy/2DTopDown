using UnityEngine;
using UnityEngine.UI; // Husk denne for Sliders

public class LoadPlayerPrefSettings : MonoBehaviour
{
    

    [Header("UI Elementer i denne scene")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle screenShakeToggle;

    void Start()
    {
        // Tjek om vores GameSettings overhovedet eksisterer i spillet lige nu
        if (GameSettings.Instance == null)
        {
            Debug.LogWarning("GameSettings Instance blev ikke fundet! Husk at starte spillet fra startmenuen.");
            return;
        }

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            GameSettings.Instance.MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            GameSettings.Instance.MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
        if (PlayerPrefs.HasKey("EffectVolume"))
        {
            GameSettings.Instance.EffectVolume = PlayerPrefs.GetFloat("EffectVolume");
        }
        if (PlayerPrefs.HasKey("UseScreenShake"))
        {
            GameSettings.Instance.UseScreenShake = PlayerPrefs.GetInt("UseScreenShake") == 1 ? true : false;
        }

        masterSlider.value = GameSettings.Instance.MasterVolume;
        // 2. Forbind slidernes ændringer direkte til GameSettings via kode (AddListener)
        masterSlider.onValueChanged.AddListener(GameSettings.Instance.SetMasterVolume);

        musicSlider.value = GameSettings.Instance.MusicVolume;
        musicSlider.onValueChanged.AddListener(GameSettings.Instance.SetMusicVolume);
        effectsSlider.value = GameSettings.Instance.EffectVolume;
        effectsSlider.onValueChanged.AddListener(GameSettings.Instance.SetEffectsVolume);
        screenShakeToggle.isOn = GameSettings.Instance.UseScreenShake;

        screenShakeToggle.onValueChanged.AddListener(GameSettings.Instance.SetUseScreenShake);
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        // Det er god stil at fjerne listeners igen, når panelet lukkes/lukkes ned, for at undgå fejl i hukommelsen
        if (GameSettings.Instance != null)
        {
            masterSlider.onValueChanged.RemoveListener(GameSettings.Instance.SetMasterVolume);
            musicSlider.onValueChanged.RemoveListener(GameSettings.Instance.SetMusicVolume);
            effectsSlider.onValueChanged.RemoveListener(GameSettings.Instance.SetEffectsVolume);
            screenShakeToggle.onValueChanged.RemoveListener(GameSettings.Instance.SetUseScreenShake);
        }
    }
}

