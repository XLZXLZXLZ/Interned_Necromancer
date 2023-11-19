using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshEye : Monster
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Transform sprite;
    private FleshEyeBall eyeBall;
    private Lazer lazer;

    public Vector2 eyeDir => eyeBall.eyeDir;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        eyeBall = GetComponentInChildren<FleshEyeBall>();
        lazer = GetComponentInChildren<Lazer>();
        sprite = transform.Find("Sprite");
    }

    private void Start()
    {
        lazer.ShutLazer();
    }

    #region 玩家操纵部(shi)分(shan)
    public float speed = 5f;
    public bool isUp =>
        Physics2D.BoxCast(coll.bounds.center, upBoxCastSize, 0f, Vector2.up, upBoxCastDistance, Tools.GetLayer("Ground")).collider != null;
    public bool isDown => 
        Physics2D.BoxCast(coll.bounds.center, downBoxCastSize, 0f, Vector2.down, downBoxCastDistance, Tools.GetLayer("Ground")).collider != null;

    public bool isLeft =>
    Physics2D.BoxCast(coll.bounds.center, leftBoxCastSize, 0f, Vector2.left, leftBoxCastDistance, Tools.GetLayer("Ground")).collider != null;
    public bool isRight =>
        Physics2D.BoxCast(coll.bounds.center, rightBoxCastSize, 0f, Vector2.right, rightBoxCastDistance, Tools.GetLayer("Ground")).collider != null;
    public bool isUpLeft =>
        Physics2D.BoxCast(coll.bounds.center, upLeftBoxCastSize, 45f, Vector2.up + Vector2.left, upLeftBoxCastDistance*1.414f, Tools.GetLayer("Ground")).collider != null;
    public bool isUpRight =>
        Physics2D.BoxCast(coll.bounds.center, upRightBoxCastSize, -45f, Vector2.up + Vector2.right, upRightBoxCastDistance*1.414f, Tools.GetLayer("Ground")).collider != null;
    public bool isDownLeft =>
        Physics2D.BoxCast(coll.bounds.center, downLeftBoxCastSize, -45f, Vector2.down + Vector2.left, downLeftBoxCastDistance*1.414f, Tools.GetLayer("Ground")).collider != null;
    public bool isDownRight =>
        Physics2D.BoxCast(coll.bounds.center, downRightBoxCastSize, 45f, Vector2.down + Vector2.right, downRightBoxCastDistance*1.414f, Tools.GetLayer("Ground")).collider != null;

    [Header("探测包围盒设置")]
    public Vector2 upBoxCastSize = new Vector2(1f, 0.1f); // 地面包围盒的大小
    public float upBoxCastDistance = 0.1f; // 地面包围盒的投射距离
    public Vector2 downBoxCastSize = new Vector2(0.1f, 1f); // 墙体包围盒大小
    public float downBoxCastDistance = 0.1f; // 墙体包围盒投射距离
    public Vector2 leftBoxCastSize = new Vector2(0.1f, 1f); // 左侧包围盒大小
    public float leftBoxCastDistance = 0.1f; // 左侧包围盒投射距离
    public Vector2 rightBoxCastSize = new Vector2(0.1f, 1f); // 右侧包围盒大小
    public float rightBoxCastDistance = 0.1f; // 右侧包围盒投射距离
    public Vector2 upLeftBoxCastSize = new Vector2(0.1f, 0.1f); // 左上包围盒大小
    public float upLeftBoxCastDistance = 0.1f; // 左上包围盒投射距离
    public Vector2 upRightBoxCastSize = new Vector2(0.1f, 0.1f); // 右上包围盒大小
    public float upRightBoxCastDistance = 0.1f; // 右上包围盒投射距离
    public Vector2 downLeftBoxCastSize = new Vector2(0.1f, 0.1f); // 左下包围盒大小
    public float downLeftBoxCastDistance = 0.1f; // 左下包围盒投射距离
    public Vector2 downRightBoxCastSize = new Vector2(0.1f, 0.1f); // 右下包围盒大小
    public float downRightBoxCastDistance = 0.1f; // 右下包围盒投射距离

    private bool isTouched = false;
    private bool isRealTouched => isLeft || isRight || isUp || isDown || isDownLeft || isDownRight || isUpLeft || isUpRight;

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        // 绘制包围盒，可视化编辑
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + Vector3.up * upBoxCastDistance, upBoxCastSize);
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + Vector3.down * downBoxCastDistance, downBoxCastSize);
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + Vector3.left * leftBoxCastDistance, leftBoxCastSize);
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + Vector3.right * rightBoxCastDistance, rightBoxCastSize);
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + (Vector3.up + Vector3.left) * upLeftBoxCastDistance, upLeftBoxCastSize);
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + (Vector3.up + Vector3.right) * upRightBoxCastDistance, upRightBoxCastSize);
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + (Vector3.down + Vector3.left) * downLeftBoxCastDistance, downLeftBoxCastSize);
        Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center + (Vector3.down + Vector3.right) * downRightBoxCastDistance, downRightBoxCastSize);
#endif
    }

    private void Update()
    {
        //未接触墙体时，快速下落直到碰撞到墙体
        if(!isTouched)
        {
            if (isRealTouched)
            {
                rb.gravityScale = 0;
                isTouched = true;
            }
        }

        if(isTouched)
        {
            if (!isRealTouched)
            {
                rb.gravityScale = 8;
                isTouched = false;
            }
        }

        if(!controllable||!isTouched) return;

        bool left = false, right = false,up = false,down = false;
        // 检测输入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        /*
        Debug.Log("isUp: " + isUp + "  isDown: " + isDown + "  isLeft: " + isLeft + "  isRight: " + isRight);
        Debug.Log("isUpLeft: " + isUpLeft + "  isUpRight: " + isUpRight + "  isDownLeft: " + isDownLeft + "  isDownRight: " + isDownRight);
        */

        // 检测各个方向是否允许移动
        if(!isLeft&&!isRight&&!isUp&&!isDown)
        {
            if(isUpLeft)
            {
                up = true;
                left = true;
            }
            if(isUpRight)
            {
                up = true;
                right = true;
            }
            if(isDownLeft)
            {
                down = true;
                left = true;
            }
            if( isDownRight)
            {
                down = true;
                right = true;
            }
        }
        else
        {
            if (isLeft || isRight)
            {
                up = true;
                down = true;
            }
            if(isUp || isDown)
            {
                left = true;
                right = true;
            }
        }
        if(!left && horizontalInput < 0)
            horizontalInput = 0;
        if(!right && horizontalInput > 0)
            horizontalInput = 0;
        if(!up && verticalInput > 0)
            verticalInput = 0;
        if(!down && verticalInput < 0)
            verticalInput= 0;

        rb.velocity = new Vector2(horizontalInput, verticalInput) * speed;

        float rotateEffect = 0;
        if (isDown)
            rotateEffect += -horizontalInput;
        else if (isUp)
            rotateEffect += horizontalInput;
        if(isLeft)
            rotateEffect += verticalInput;
        else if (isRight)
            rotateEffect += -verticalInput;

        sprite.Rotate(0, 0, Time.deltaTime * 180 * rotateEffect);
        lazer.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(eyeDir.y, eyeDir.x) * Mathf.Rad2Deg - 90);
    }

    #endregion

    #region 激光转向部分

    private int input;

    public void OnActive()
    {
        input++;
        lazer.OpenLazer();
    }

    public void OnDisActive()
    {
        input--;
        if(input <= 0)
            lazer.ShutLazer();
    }

    #endregion


    public override void OnControlEnd()
    {
        base.OnControlEnd();
        rb.velocity = Vector2.zero; 
    }
}
