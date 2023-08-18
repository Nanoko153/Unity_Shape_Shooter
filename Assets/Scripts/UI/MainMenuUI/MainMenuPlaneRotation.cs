using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuPlaneRotation : MonoBehaviour
{
    Camera mainCamera;

    float middlePosX;
    float minPosX;
    float maxPosX;
    float deltaX;

    float middlePosY;
    float minPosY;
    float maxPosY;
    float deltaY;

    public float maxRotAngleX = 10f;
    public float maxRotAngleY = 5f;

    float tempX;
    float tempY;
    protected void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        middlePosX = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f)).x;
        minPosX = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        maxPosX = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        deltaX = maxPosX- middlePosX;

        middlePosY = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f)).y;
        minPosY = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y;
        maxPosY = mainCamera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y;
        deltaY = maxPosY- middlePosY;
    }

    private void FixedUpdate()
    {
        tempX = middlePosX - GetMouseInWorldPosition().x;
        tempX = Mathf.Clamp(maxRotAngleX * (tempX / deltaX), -maxRotAngleX, maxRotAngleX);

        tempY = GetMouseInWorldPosition().y - middlePosY;
        tempY = Mathf.Clamp(maxRotAngleY * (tempY / deltaY), -maxRotAngleY, maxRotAngleY);

        transform.rotation = Quaternion.Euler(tempY, tempX, 0);
    }
    public Vector3 GetMouseInWorldPosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        return worldPosition;
    }
}
