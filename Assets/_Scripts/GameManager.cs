using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject PauseMenu;
    private bool isPaused = false;

    public void PauseListener(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            TogglePause();
        }
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

    }
    private void Start()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        if(GameSettings.Instance == null)
        {
            GameObject g =  new GameObject("Settings", typeof(GameSettings));
            g.GetComponent<GameSettings>().enabled = true;
            g.GetComponent<GameSettings>().LoadAwake();

            //            Instance.gameObject.AddComponent<GameSettings>();

            //GameObject settingsObj = Instantiate(new GameObject("GameSettings"), transform.position, Quaternion.identity); //
            //settingsObj.AddComponent<GameSettings>();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            HandleMenues(true);
            Time.timeScale = 0f;
        }
        else
        {
            HandleMenues(false);
            Time.timeScale = 1f;
        }
    }

    private void HandleMenues(bool active)
    {
        PauseMenu.SetActive(active);
        SettingsMenu.SetActive(active);
    }

}
