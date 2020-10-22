using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody;
    Collider2D collider;
    [SerializeField] float jumpSpeed = 25f;
    [SerializeField] float moveSpeed = 7f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Run();
        Jump();
    }

    private void Run() {
        float deltaX = Input.GetAxis("Horizontal");
        var velocity = new Vector2(deltaX * moveSpeed, rigidBody.velocity.y);
        
        rigidBody.velocity = velocity;

        bool isRunning = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;

        animator.SetBool("Running", isRunning);

        if (isRunning) {
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1f);
        }
    }

    private void Jump() {
        if (!collider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }


        bool isJumping = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;
        
        if (Input.GetButtonDown("Jump")) {
            rigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
}
