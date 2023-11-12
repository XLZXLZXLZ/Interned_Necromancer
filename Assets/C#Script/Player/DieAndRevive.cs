using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeathType
{
    Null,
    Spike,
    Burn,
    Fall,
    Drown
}
public class DieAndRevive : MonoBehaviour
{
    public DeathType deathReason;
    public Soul[] soul;
    public Vector2 revivePos;

    //触发死亡扳机(即将进入死亡时)
    public void DeathTrigger(DeathType type)
    {
        if (deathReason != DeathType.Null)
            return;
        deathReason = type;
    }
    //死亡事件
    public void OnDeath()
    {
        Soul s;
        switch(deathReason)
        {
            case DeathType.Spike:
                s = Instantiate(soul[0], transform.position, Quaternion.identity);
                break;
            case DeathType.Burn:
                s = Instantiate(soul[1], transform.position, Quaternion.identity);
                break;
            case DeathType.Fall:
                s = Instantiate(soul[2], transform.position, Quaternion.identity);
                break;
            default:
                s = null;
                break;
        }
        s.targetPos = transform.position + new Vector3(0, 2, 0);
    }
    //复活事件
    public void OnRevive()
    {
        transform.position = revivePos;
        deathReason = DeathType.Null;
    }
}
