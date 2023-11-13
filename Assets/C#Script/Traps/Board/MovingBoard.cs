using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoard : Board
{
    public Vector3 destination;
    public Vector3 origin;
    public bool isLerp = false;
    public float distance;
    
    private Vector3 currentTarget;
    private bool isMoving;

    public float onSpeed;//运行时速度 
    public float offSpeed;//恢复时速度
    public bool alwaysWorking;

    private void Start()
    {
        origin = transform.position;
        destination = transform.position + destination;
        if (alwaysWorking)
            SwitchOn();
    }

    private void FixedUpdate()
    {
        var speed = target.isOn ? onSpeed : offSpeed;
        if(isMoving)
        {
            transform.position = isLerp? Vector3.Lerp(transform.position, currentTarget, speed * Time.fixedDeltaTime) : Vector3.MoveTowards(transform.position, currentTarget, speed * Time.fixedDeltaTime);
            if ((transform.position - currentTarget).sqrMagnitude <= 0.01f)
            {
                isMoving = false;
            }
        }
    }

    protected override void SwitchOn()
    {
        base.SwitchOn();

        currentTarget = destination;
        isMoving = true;
    }

    protected override void SwitchOff()
    {
        base.SwitchOff();

        currentTarget = origin;
        isMoving = true;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + destination, 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + destination);

        if (targetSwitch != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(targetSwitch.transform.position, 0.5f);
            Gizmos.DrawLine(transform.position, targetSwitch.transform.position);
        }
    }
}
