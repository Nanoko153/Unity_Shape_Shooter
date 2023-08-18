using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionsInput : MonoBehaviour
{
    public Vector2 MoveInputValue => InputManager.Instance.inputActions.GamePlayingInput.Move.ReadValue<Vector2>();
    public bool IsMove => MoveInputValue != Vector2.zero;

    // public bool IsKeyDown_Shift => InputManager.Instance.inputActions.PlayerActionsInput.Shift.WasPerformedThisFrame();
    // public bool IsKeyUp_Shift => InputManager.Instance.inputActions.PlayerActionsInput.Shift.WasReleasedThisFrame();

    // public bool IsKeyDown_Tap => InputManager.Instance.inputActions.PlayerActionsInput.Tap.WasPerformedThisFrame();
    // public bool IsKeyUp_Tap => InputManager.Instance.inputActions.PlayerActionsInput.Tap.WasReleasedThisFrame();

    // public bool IsKeyDown_Q => InputManager.Instance.inputActions.PlayerActionsInput.Q.WasPerformedThisFrame();
    // public bool IsKeyUp_Q => InputManager.Instance.inputActions.PlayerActionsInput.Q.WasReleasedThisFrame();

    // public bool IsKeyDown_E => InputManager.Instance.inputActions.PlayerActionsInput.E.WasPerformedThisFrame();
    // public bool IsKeyUp_E => InputManager.Instance.inputActions.PlayerActionsInput.E.WasReleasedThisFrame();

    // public bool IsKeyDown_F => InputManager.Instance.inputActions.PlayerActionsInput.F.WasPerformedThisFrame();
    // public bool IsKeyUp_F => InputManager.Instance.inputActions.PlayerActionsInput.F.WasReleasedThisFrame();

    // public bool IsKeyDown_Space => InputManager.Instance.inputActions.PlayerActionsInput.Space.WasPerformedThisFrame();
    // public bool IsKeyUp_Space => InputManager.Instance.inputActions.PlayerActionsInput.Space.WasReleasedThisFrame();

    public bool IsKeyDown_LeftMouse => InputManager.Instance.inputActions.GamePlayingInput.Mouse_Left.WasPerformedThisFrame();
    public bool IsKeyUp_LeftMouse => InputManager.Instance.inputActions.GamePlayingInput.Mouse_Left.WasReleasedThisFrame();

    public bool IsKeyDown_RightMouse => InputManager.Instance.inputActions.GamePlayingInput.Mouse_Right.WasPerformedThisFrame();
    public bool IsKeyUp_RightMouse => InputManager.Instance.inputActions.GamePlayingInput.Mouse_Right.WasReleasedThisFrame();

    Camera mainCamera;

    protected void Awake()
    {
        mainCamera = Camera.main;
    }

    public Vector3 GetMouseInWorldPosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f; // 由于屏幕坐标中没有z轴信息，因此将其设置为0
        return worldPosition;
    }
}
