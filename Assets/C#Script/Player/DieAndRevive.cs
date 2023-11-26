using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public partial class EventManager:Singleton<EventManager>
{
    public UnityAction<DieAndRevive> OnLifeChange;
}

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
    [SerializeField]
    private GameObject bloodParticle;
    [SerializeField]
    private GameObject lazerParticle;

    public int lifeLeft = 5;
    public Soul[] soul;

    [HideInInspector]
    public DeathType deathReason;

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            lifeLeft++;
            EventManager.Instance.OnLifeChange?.Invoke(this);
        }
    }

    //触发死亡扳机(即将进入死亡时)
    public void DeathTrigger(DeathType type)
    {
        if (deathReason != DeathType.Null)
            return;
        deathReason = type;

        switch (deathReason)
        {
            case DeathType.Spike:
                Instantiate(bloodParticle, transform.position, Quaternion.identity);
                CameraActions.Instance.CameraShake(0.1f, 0.15f);
                AudioManager.Instance.PlaySe("SpikeKill");
                break;
            case DeathType.Burn:
                Instantiate(lazerParticle, transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySe("LazerKill");
                break;
            case DeathType.Fall:
                Instantiate(bloodParticle, transform.position, Quaternion.identity);
                CameraActions.Instance.CameraShake(0.2f, 0.25f);
                AudioManager.Instance.PlaySe("FallKill");
                break;
            default:
                break;
        }

        lifeLeft--;
        EventManager.Instance.OnLifeChange?.Invoke(this);
    }
    //死亡事件
    public void OnDeath()
    {
        if (lifeLeft <= 0)
        {
            FadeUI.Instance.Fade(SceneManager.GetActiveScene().name);
            return;
        }

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
        if(lifeLeft <= 0) //生命值归零，重载场景
            return;
        AudioManager.Instance.PlaySe("Respawn");

        transform.position = RevivePos.transform.position + Vector3.up * 3;
        Instantiate(respawnParticle,RevivePos.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        deathReason = DeathType.Null;
    }
}
