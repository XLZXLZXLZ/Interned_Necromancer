using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBoard : Board
{
    public Vector3 destination;
    public Vector3 origin;
    public float rotationSpeed;
    public bool isLerp;

    private Vector3 currentTarget;
    private bool isRotating;

    private void Start()
    {
        origin = transform.eulerAngles;
    }
    private void FixedUpdate()
    {
        if (isRotating)
        {
            transform.eulerAngles = isLerp? Vector3.Lerp(transform.eulerAngles, currentTarget, Time.deltaTime * rotationSpeed) : Vector3.MoveTowards(transform.eulerAngles, currentTarget, Time.deltaTime * rotationSpeed);
            if ((transform.position - currentTarget).sqrMagnitude <= 0.01f)
            {
                isRotating = false;
            }
        }
    }

    protected override void SwitchOn()
    {
        base.SwitchOn();

        currentTarget = destination;
        isRotating = true;
    }

    protected override void SwitchOff()
    {
        base.SwitchOff();

        currentTarget = origin;
        isRotating = true;
    }

    private void OnDrawGizmos()
    {
        float radius = 0.5f;

        Gizmos.color = Color.red;
        if (targetSwitch != null)
        {
            Gizmos.DrawLine(transform.position, targetSwitch.transform.position);
            Gizmos.DrawWireSphere(targetSwitch.transform.position, radius);
        }
    }
}
