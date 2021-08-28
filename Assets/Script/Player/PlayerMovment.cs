using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Header("Player Movment")]
    public float speed = 5;
    private Rigidbody2D rb;

    [Header("Player Animator")]
    public Animator animator;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void FixedUpdate()
    {
        MovePlayer();
    }


    void MovePlayer()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(xMove, yMove).normalized * speed * Time.deltaTime;

        SetAnimation(xMove, yMove);
    }

    void SetAnimation(float xMove, float yMove)
    {
        animator.SetFloat("PlayerSpeedX", xMove);
        animator.SetFloat("PlayerSpeedY", yMove);
    }
}
