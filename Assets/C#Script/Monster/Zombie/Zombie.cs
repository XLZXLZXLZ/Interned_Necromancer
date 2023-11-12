using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : Monster
{
    public float speed;

    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!controllable)
            return;

        float horizontalInput = Input.GetAxis("Horizontal");
        anim.Play(horizontalInput != 0 ? "Run" : "Idle");

        var s = transform.localScale;
        if(horizontalInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(s.x), s.y, s.z);
        else if(horizontalInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(s.x), s.y, s.z);
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    public override void OnControlEnd()
    {
        base.OnControlEnd();
        anim.Play("Idle");
    }
}
