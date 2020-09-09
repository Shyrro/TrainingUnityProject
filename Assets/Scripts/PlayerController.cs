using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalMove = 0f;
    private bool jump = false;

    public CharacterController2D controller;
    public Animator animator;

    public float speed = 30;
    public float latency = 0;
    
    private void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * speed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

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
        yield return new WaitForSeconds(latency);
        controller.Move(lateMove, false, lateJump);
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
}
