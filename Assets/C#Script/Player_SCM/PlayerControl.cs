using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 控制玩家刚体速度，同时反馈刚体运动信息
/// </summary>
public class PlayerControl : MonoBehaviour
{
    PlayerInfo info;
    PlayerInput input;
    Rigidbody2D rb;
    TouchDetective touch;

    //是否正在下坠
    public bool IsFalling => rb.velocity.y <= 0;

    //是否输入移动
    public bool IsMoving => input.axes.x != 0;

    //是否触地
    public bool IsGround => touch.isGround;
    

    //是否挂墙
    public bool IsWall => touch.isWall;
    public float wallTimer = 0;
    public float wallBufferTime = 0.1f;

    //二段跳
    public int airJumpChance = 1;
    public bool airJump => airJumpChance > 0;

    private bool endFlag;

    private void Awake()
    {
        //初始化
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        touch = GetComponent<TouchDetective>();
        info = GetComponent<PlayerInfo>();
    }

    private void Start()
    {
        //开启动作表
        input.InputEnable(true);
    }

    /// <summary>
    /// 调整速度函数
    /// </summary>
    /// <param name="velocity"></param>
    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    /// <summary>
    /// 调整朝向函数
    /// </summary>
    /// <param name="value"></param>
    public void FaceAdjust(float value)
    {
        var x = Mathf.Abs(transform.localScale.x);
        var y = Mathf.Abs(transform.localScale.y);
        var z = Mathf.Abs(transform.localScale.z);
        if(value > 0.1f)
            transform.localScale = new Vector3(x, y, z);
        else if(value < -0.1f)
            transform.localScale = new Vector3(-x, y, z);
    }

    /// <summary>
    /// 水平移动函数
    /// </summary>
    /// <param name="velocityX"></param>
    public void SetVelocityX(float velocityX)
    {
        FaceAdjust(velocityX);
        velocityX = Mathf.MoveTowards(rb.velocity.x ,velocityX, info.accelerate * Time.fixedDeltaTime);
        SetVelocity(new Vector2(velocityX, rb.velocity.y));
    }

    /// <summary>
    /// 垂直移动函数
    /// </summary>
    /// <param name="velocityY"></param>
    public void SetVelocityY(float velocityY)
    {
        SetVelocity(new Vector2(rb.velocity.x, velocityY));
    }
}
