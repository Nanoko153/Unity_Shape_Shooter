using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingManager : MonoBehaviour
{
    public Canvas gamePlayingCanvas;
    public Canvas gamePausedCanvas;
    public Canvas waveInformationCanvas;
    public Canvas GameOverCanvas;

    [SerializeField] AudioClip inPausedSFX;
    [SerializeField] AudioClip outPausedSFX;

    public bool escKeyDown =>InputManager.Instance.inputActions.GamePausedInput.Esc.WasPerformedThisFrame();

    void Start()
    {
        InputManager.Instance.EnableGamePausedInput();

        EnableGamePlayingCanvas();
        DisableGamePausedCanvas();
        //DisableGameOverCanvas();
        //DisableWaveInformationCanvas();

        EventCenter.Instance.AddEventListener("PauseGame", EnableGamePausedCanvas);
        EventCenter.Instance.AddEventListener("PauseGame", PlayInPausedSFX);

        EventCenter.Instance.AddEventListener("ContinueGame", DisableGamePausedCanvas);
        EventCenter.Instance.AddEventListener("ContinueGame", PlayOutPausedSFX);

        EventCenter.Instance.AddEventListener("GameOver", EnableGameOverCanvas);
        //EventCenter.Instance.AddEventListener("StartWave", EnableWaveInformationCanvas);
    }

    void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener("PauseGame", EnableGamePausedCanvas);
        EventCenter.Instance.RemoveEventListener("PauseGame", PlayInPausedSFX);
        EventCenter.Instance.RemoveEventListener("ContinueGame", DisableGamePausedCanvas);
        EventCenter.Instance.RemoveEventListener("ContinueGame", PlayOutPausedSFX);
        EventCenter.Instance.RemoveEventListener("GameOver", EnableGameOverCanvas);
        //EventCenter.Instance.RemoveEventListener("StartWave", EnableWaveInformationCanvas);
    }
    private void Update()
    {
        if(escKeyDown)
        {
            //≤•∑≈“Ù–ßSFX
            if(GameManager.Instance.gameState == GameState.GamePlaying)
                EventCenter.Instance.EventTrigger("PauseGame");
            else if(GameManager.Instance.gameState == GameState.GamePaused)
                EventCenter.Instance.EventTrigger("ContinueGame");
        }
    }

    public void PlayInPausedSFX()
    {
        AudioManager.Instance.PlayUI(inPausedSFX);
    }
    public void PlayOutPausedSFX()
    {
        AudioManager.Instance.PlayUI(outPausedSFX);
    }

    public void EnableGamePlayingCanvas()
    {
        gamePlayingCanvas.enabled = true;
    }
    public void DisableGamePlayingCanvas()
    {
        gamePlayingCanvas.enabled = false;
    }

    public void EnableGamePausedCanvas()
    {
        gamePausedCanvas.enabled = true;
    }
    public void DisableGamePausedCanvas()
    {
        gamePausedCanvas.enabled = false;
    }

    public void EnableWaveInformationCanvas()
    {
        waveInformationCanvas.enabled = true;
    }
    public void DisableWaveInformationCanvas()
    {
        waveInformationCanvas.enabled = false;
    }
    public void EnableGameOverCanvas()
    {
        GameOverCanvas.enabled = true;
    }
    public void DisableGameOverCanvas()
    {
        GameOverCanvas.enabled = false;
    }
}
