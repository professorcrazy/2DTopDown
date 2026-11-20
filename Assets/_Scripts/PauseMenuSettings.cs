using UnityEngine;
using UnityEngine.UI; // Husk denne for Sliders

public class PauseMenuSettings : MonoBehaviour
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

        // 1. Sæt sliderne til at have de rigtige værdier, som spillet husker lige nu
        masterSlider.value = GameSettings.Instance.MasterVolume;
        musicSlider.value = GameSettings.Instance.MusicVolume;
        effectsSlider.value = GameSettings.Instance.EffectVolume;
        screenShakeToggle.isOn = GameSettings.Instance.UseScreenShake;

        // 2. Forbind slidernes ændringer direkte til GameSettings via kode (AddListener)
        masterSlider.onValueChanged.AddListener(GameSettings.Instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(GameSettings.Instance.SetMusicVolume);
        effectsSlider.onValueChanged.AddListener(GameSettings.Instance.SetEffectsVolume);
        screenShakeToggle.onValueChanged.AddListener(GameSettings.Instance.SetUseScreenShake);
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