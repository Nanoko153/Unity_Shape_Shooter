using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public PlayerController player;
    public void RegisterPlayer(PlayerController player)
    {
        this.player = player;
        Debug.Log("PlayerÒÑ×¢²á");
    }

    public GameState gameState = GameState.GamePlaying;

    Coroutine startGamePlayingCoroutine;

    public PlayerScore tempPlayerScore;

    protected override void Awake()
    {
        base.Awake();
        EventCenter.Instance.AddEventListener<PlayerController>("RegisterPlayer", RegisterPlayer);
        EventCenter.Instance.AddEventListener("PauseGame", PauseGame);
        EventCenter.Instance.AddEventListener("ContinueGame", ReturnGame);
        EventCenter.Instance.AddEventListener("ReturnMainMenu", ReturnMainMenu);
        EventCenter.Instance.AddEventListener("GameOver", GameOver);
    }

    private void Start()
    {
        tempPlayerScore = new PlayerScore("","","",0);
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<PlayerController>("RegisterPlayer", RegisterPlayer);
        EventCenter.Instance.RemoveEventListener("PauseGame", PauseGame);
        EventCenter.Instance.RemoveEventListener("ContinueGame", ReturnGame);
        EventCenter.Instance.RemoveEventListener("ReturnMainMenu", ReturnMainMenu);
        EventCenter.Instance.RemoveEventListener("GameOver", GameOver);
    }

    public void PauseGame()
    {
        gameState = GameState.GamePaused;
        Time.timeScale = 0;
    }

    public void ReturnGame()
    {
        gameState = GameState.GamePlaying;
        Time.timeScale = 1;
    }

    public void ReturnMainMenu()
    {
        gameState = GameState.MainMenu;
        Time.timeScale = 1;
    }

    public void StartGamePlaying()
    {
        gameState = GameState.GamePlaying;
        if(startGamePlayingCoroutine != null)
            StopCoroutine(startGamePlayingCoroutine);
        startGamePlayingCoroutine = StartCoroutine(StartGamePlayingCoroutine());
    }

    IEnumerator StartGamePlayingCoroutine()
    {
        yield return new WaitUntil(() => SceneSystem.Instance.currentSceneName == "GamePlay");
        yield return new WaitForSeconds(2f);
        EventCenter.Instance.EventTrigger("PlayWaveUpdateUI");
        EventCenter.Instance.EventTrigger("StartTimer");
        Debug.Log("GamePlaying!");
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
        Time.timeScale = 1;
        Debug.Log("GameOver");
    }
}

public enum GameState
{
    MainMenu,
    GamePlaying,
    GamePaused,
    GameOver
}
