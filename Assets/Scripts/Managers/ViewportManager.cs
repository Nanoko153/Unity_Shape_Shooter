using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportManager : Singleton<ViewportManager>
{
    float minX;
    float maxX;
    float minY;
    float maxY;

    float outOfViewMinX;
    float outOfViewMaxX;
    float outOfViewMinY;
    float outOfViewMaxY;

    float middleX;

    [HideInInspector]
    public Vector2 Left_Position;
    [HideInInspector]
    public Vector2 Right_Position;
    [HideInInspector]
    public Vector2 Up_Position;
    [HideInInspector]
    public Vector2 Down_Position;
    [HideInInspector]
    public Vector2 Left_Up_Position;
    [HideInInspector]
    public Vector2 Right_Up_Position;
    [HideInInspector]
    public Vector2 Right_Down_Position;
    [HideInInspector]
    public Vector2 Left_Down_Position;
    [HideInInspector]
    public Vector2 Middle_Position;
    public Vector2[] ninePositionInView = new Vector2[9];

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        Camera mainCamera = Camera.main;

        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f));  //将摄像机视口左下角坐标转换为世界坐标存到bottomLeft
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f));    //将摄像机视口右上角坐标转换为世界坐标存到topRight
        middleX = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f)).x;       //获取一半的屏幕转世界坐标

        minX = bottomLeft.x;
        minY = bottomLeft.y;
        maxX = topRight.x;
        maxY = topRight.y;

        outOfViewMinX = minX - 3;
        outOfViewMaxX = maxX + 3;
        outOfViewMinY = minY - 3;
        outOfViewMaxY = maxY + 3;

        ninePositionInView[0] = Left_Position = mainCamera.ViewportToWorldPoint(new Vector3(0.2f, 0.5f));
        ninePositionInView[1] = Right_Position = mainCamera.ViewportToWorldPoint(new Vector3(0.8f, 0.5f));
        ninePositionInView[2] = Up_Position = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.8f));
        ninePositionInView[3] = Down_Position = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.2f));
        ninePositionInView[4] = Left_Up_Position = mainCamera.ViewportToWorldPoint(new Vector3(0.2f, 0.8f));
        ninePositionInView[5] = Right_Up_Position = mainCamera.ViewportToWorldPoint(new Vector3(0.8f, 0.8f));
        ninePositionInView[6] = Right_Down_Position = mainCamera.ViewportToWorldPoint(new Vector3(0.8f, 0.2f));
        ninePositionInView[7] = Left_Down_Position = mainCamera.ViewportToWorldPoint(new Vector3(0.2f, 0.2f));
        ninePositionInView[8] = Middle_Position = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
    }

    /// <summary>
    /// 一个公开给Player能够移动范围的函数
    /// </summary>
    public Vector3 PlayerMoveablePosition(Vector3 playerPosition,float paddingX,float paddingY)
    {
        Vector3 positon = Vector3.zero;

        positon.x = Mathf.Clamp(playerPosition.x, minX+paddingX, maxX-paddingX);
        positon.y = Mathf.Clamp(playerPosition.y, minY+paddingY, maxY-paddingY);

        return positon;
    }

    /// <summary>
    /// 随机屏幕中的任意一点
    /// </summary>
    /// <param name="paddingX"></param>
    /// <param name="paddingY"></param>
    /// <returns></returns>
    public Vector3 RandomAllPosition(float paddingX, float paddingY)
    {
        Vector3 position = Vector3.zero;                            //申明一个三维的中间变量

        position.x = Random.Range(minX+paddingX, maxX - paddingX);      //随机获取最右的x
        position.y = Random.Range(minY + paddingY, maxY - paddingY);    //随机获取y

        return position;
    }

    public Vector3 GetOutOfViewPosition()
    {
        Vector3 position = Vector3.zero;

        int i = Random.Range(0,4);
        if(i == 0)
        {
            position.x = Random.Range(outOfViewMinX, minX);
            position.y = Random.Range(outOfViewMinY, outOfViewMaxY);
        }
        else if(i == 1)
        {
            position.x = Random.Range(maxX, outOfViewMaxX);
            position.y = Random.Range(outOfViewMinY, outOfViewMaxY);
        }
        else if(i == 2)
        {
            position.x = Random.Range(outOfViewMinX, outOfViewMaxX);
            position.y = Random.Range(maxY, outOfViewMaxY);    //随机获取y
        }
        else
        {
            position.x = Random.Range(outOfViewMinX, outOfViewMaxX);
            position.y = Random.Range(outOfViewMinY, minY);
        }
        return position;
    }
}
