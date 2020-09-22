using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalMove = 0f;
    private bool jump = false;

    public Guid Id;

    public CharacterController2D Controller;
    public Animator Animator;

    public float Speed = 30;
    public float Latency = 0;

    private void Start()
    {
        Id = Guid.NewGuid();
    }
    
    private void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * Speed;

        Animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        StartCoroutine(MovePlayer(horizontalMove * Time.fixedDeltaTime, jump));
        jump = false;
    }

    private IEnumerator MovePlayer(float lateMove, bool lateJump) {
        yield return new WaitForSeconds(Latency);
        Controller.Move(lateMove, false, lateJump);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Physics2D.IgnoreCollision(
                collision.collider,
                GetComponent<CircleCollider2D>()
            );
            Physics2D.IgnoreCollision(
                collision.collider,
                GetComponent<BoxCollider2D>()
            );
        }
    }

    public void ApplyBuff(float speed, float jumpForce)
    {
        Debug.Log("Applied");
        Speed += speed;
        Controller.m_JumpForce += jumpForce;
    }
}
