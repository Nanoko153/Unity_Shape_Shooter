using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public InputActions inputActions;
    [HideInInspector]
    //public Camera mainCamera;

    // public Vector3 mousePosition = Vector3.zero;
    // public Vector3 mouseInGamePosition = Vector3.zero;
    protected override void Awake()
    {
        base.Awake();

        inputActions = new InputActions();

        // mainCamera = Camera.main;
    }

    public void DisableAllInput()
    {
        DisableGamePlayingInputInput();
    }

    #region GamePlayingInput
    public void EnableGamePlayingInputInput()
    {
        inputActions.GamePlayingInput.Enable();
    }

    public void DisableGamePlayingInputInput()
    {
        inputActions.GamePlayingInput.Disable();
    }
    #endregion

    #region GamePausedInput
    public void EnableGamePausedInput()
    {
        inputActions.GamePausedInput.Enable();
    }

    public void DisableGamePausedInput()
    {
        inputActions.GamePausedInput.Disable();
    }
    #endregion
}
