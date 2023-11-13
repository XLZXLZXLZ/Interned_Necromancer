using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private ParticleSystem[] respawnPointParticle;

    [SerializeField]
    private GameObject enterParticle;

    [SerializeField]
    private bool defaultPoint;

    private void Awake()
    {
        respawnPointParticle = GetComponentsInChildren<ParticleSystem>();
    }

    private void Start()
    {
        if (defaultPoint)
        {
            DieAndRevive.Instance.RevivePos = this;
        }
        else
        {
            foreach (var particle in respawnPointParticle)
            {
                var m = particle.emission;
                m.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            DieAndRevive.Instance.RevivePos = this;
        }
    }

    public void SetRespawnPoint()
    {
        Instantiate(enterParticle, transform.position, Quaternion.identity);
        foreach (var particle in respawnPointParticle) 
        {
            var m = particle.emission;
            m.enabled = true;
        }
    }

    public void CancelRespawnPoint()
    {
        foreach (var particle in respawnPointParticle)
        {
            var m = particle.emission;
            m.enabled = false;
        }
    }
}
