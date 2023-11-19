using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostArea : MonoBehaviour
{
    public float gravity = 0.5f;
    public float maxDownSpeed = 2f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<DieAndRevive>().deathReason != DeathType.Null)
                return;
            var rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = gravity;
            collision.GetComponent<PlayerControl>().airJumpChance = 1;
            if (rb.velocity.y < maxDownSpeed)
                rb.velocity = new Vector2(rb.velocity.x, -maxDownSpeed);
        }
        
        if(collision.TryGetComponent<Zombie>(out var _))
        {
            var rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = gravity;
            if (rb.velocity.y < maxDownSpeed * 0.75f)
                rb.velocity = new Vector2(rb.velocity.x, -maxDownSpeed * 0.75f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<DieAndRevive>().deathReason != DeathType.Null)
                return;
            collision.GetComponent<Rigidbody2D>().gravityScale = 1.8f;
        }

        if(collision.TryGetComponent<Zombie>(out var _))
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = 1.8f;
        }
        
    }
}
