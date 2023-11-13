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
public class DieAndRevive : Singleton<DieAndRevive>
{
    [SerializeField]
    private GameObject respawnParticle;

    [HideInInspector]
    public DeathType deathReason;
    public Soul[] soul;
    private RespawnPoint revivePos;
    public RespawnPoint RevivePos
    {
        get { return revivePos; }
        set
        {
            if (revivePos != value)
            {
                revivePos?.CancelRespawnPoint();
                revivePos = value;
                revivePos?.SetRespawnPoint();
            }
        }
    }

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
        transform.position = RevivePos.transform.position + Vector3.up * 3;
        Instantiate(respawnParticle,RevivePos.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        deathReason = DeathType.Null;
    }
}
