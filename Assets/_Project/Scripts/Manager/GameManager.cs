using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public enum GameState
{
    Initializing,
    MainMenu,
    Gameplay,
    Pause
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] // <-- only for debugging, remove in production
    private GameState _currentState;

    public GameState CurrentState
    {
        get { return _currentState; }
        private set { _currentState = value; }
    }

    public UnityEvent<GameState> OnGameStateChanged;


    protected override void Awake()
    {
        base.Awake();

        SetState(GameState.Initializing);
    }

    public void NewStart()
    {
        //Testing Direct SceneLoad
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        SetState(GameState.MainMenu);
    }


    public void SetState(GameState newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;
        HandleStateChange(newState);

        // Notify listeners of state change
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Initializing:
                Debug.Log("Game is initializing...");
                break;

            case GameState.MainMenu:
                Debug.Log("Entered Main Menu");
                break;

            case GameState.Gameplay:
                Debug.Log("Entered Gameplay");
                break;

            case GameState.Pause:
                Debug.Log("Game Paused");
                break;
        }
    }

    public static void SetLanguage(bool localeCode)
    {
        if (localeCode) {
            Locale locale = LocalizationSettings.AvailableLocales.GetLocale("en-US");
            LocalizationSettings.SelectedLocale = locale;
        }
        else
        {
            Locale locale = LocalizationSettings.AvailableLocales.GetLocale("es-ES");
            LocalizationSettings.SelectedLocale = locale;
        }
    }
}
