using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float speed = 25;
    public float deadTime = 20;
    public GameObject DestroyParticle;

    public Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(nameof(SelfDestroy));
    }

    public void Shoot()
    {
        rb.velocity = (Tools.MousePosition - (Vector2)transform.position).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(deadTime);
        Destroy(gameObject);
    }


    private void OnDestroy()
    {
        Instantiate(DestroyParticle, transform.position, Quaternion.identity);
    }
}
