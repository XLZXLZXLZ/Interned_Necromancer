using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class Lazer : MonoBehaviour
{
    LineRenderer line;

    public float maxDistance;
    public GameObject targetSwitch;
    public bool isFleshEye; //是否作为血肉眼的子物体

    private Switch target;
    private Transform destination;
    private bool isOn = true;

    private float lineWidth; //线默认长度
    private int width = 1;  //线相对长度

    private FleshEye castFleshEye; //当前投射到某个血肉眼上时，记录他
    private FleshEye CastFleshEye
    {
        get { return castFleshEye; }
        set 
        { 
            if(castFleshEye != value) 
            {
                castFleshEye?.OnDisActive();
                castFleshEye = value;
                castFleshEye?.OnActive();
            } 
        }
    }
    private LazerSwitch lazerSwitch;
    private LazerSwitch LazerSwitch
    {
        get { return lazerSwitch; }
        set
        {
            if (lazerSwitch != value)
            {
                lazerSwitch?.OnDisActive();
                lazerSwitch = value;
                lazerSwitch?.OnActive();
            }
        }
    }

    private void Awake()
    {
        line = GetComponentInChildren<LineRenderer>();
        lineWidth = line.startWidth;
        destination = transform.Find("Destination");

        if (targetSwitch == null)
            return;
        target = targetSwitch.GetComponent<Switch>();
        target.switchOn += ShutLazer;
        target.switchOff += OpenLazer;
    }
    private void Start()
    {
        RaycastHit2D target;
        target = Physics2D.Raycast(transform.position, transform.up, maxDistance, Tools.GetLayer("Ground"));
        Vector2 des;
        if (!target)
            des = transform.position + transform.up * maxDistance;
        else
            des = target.point;

        destination.position = des;
        transform.Find("LazerShooter").gameObject.SetActive(!isFleshEye);
        transform.Find("Start").gameObject.SetActive(!isFleshEye);
    }

    private void Update()
    {
        line.SetPosition(0, transform.position);
        UpdateLazer(transform.position, transform.up,1);
    }

    private void UpdateLazer(Vector2 startPosition, Vector2 dir,int lineIndex)
    {
        line.startWidth = Mathf.MoveTowards(line.startWidth, lineWidth * width, Time.deltaTime * 2);

        //打开时尝试追踪最远的墙体或敌人碰撞体(眼球或僵尸)
        if (!isOn)
        {
            CastFleshEye = null;
            LazerSwitch = null;
            return;
        }

        RaycastHit2D target;
        var hits = Physics2D.RaycastAll(startPosition, dir , maxDistance, LayerMask.GetMask("Ground", "Monster","Gear"));
        Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

        int temp = isFleshEye ? 1 : 0;
        if (hits.Length <= temp)
            target = default;
        else
            target = hits[isFleshEye ? 1 : 0];

        //追踪到目标时设置,否则默认设置到最大距离
        Vector2 des;
        if (!target)
        {
            des = startPosition + dir * maxDistance;
            des = Vector3.Lerp(destination.transform.position, des, 30 * Time.deltaTime);
        }
        else if (target)
        {
            //找到目标，设置目标位置 当距离更远时，提供向更远处移动的动画，当距离更近时，直接修改
            des = target.point;
            if (target.collider.TryGetComponent(out LazerSwitch lazerSwitch))
            {
                CastFleshEye = null;
                LazerSwitch = lazerSwitch;
                des = lazerSwitch.CorePos;
            }
            else if (target.collider.TryGetComponent(out FleshEye fleshEye))
            {
                CastFleshEye = fleshEye;
                LazerSwitch = null;
            }
            else
            {
                CastFleshEye = null;
                LazerSwitch = null;
            }
        }
        else
            des = Vector2.zero;

        line.SetPosition(lineIndex, des);
        //判断是不是血肉眼
        destination.position = des;
        //检测玩家，检测到直接鲨了
        RaycastHit2D player = Physics2D.Linecast(transform.position, des, Tools.GetLayer("Player"));
        if (player)
            player.collider.GetComponent<DieAndRevive>().DeathTrigger(DeathType.Burn);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * Mathf.Min(40f, maxDistance));

        if (targetSwitch != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetSwitch.transform.position);
        }
    }

    public void ShutLazer()
    {
        destination.gameObject.SetActive(false);
        width = 0;
        isOn = false;
        
    }

    public void OpenLazer()
    {
        destination.gameObject.SetActive(true);
        width = 1;
        isOn = true;
    }
}
