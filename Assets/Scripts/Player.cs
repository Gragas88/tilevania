using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    float gravityScale;
    [SerializeField] float jumpSpeed = 28f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float climbSpeed = 4f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScale = rigidBody.gravityScale;
    }

    void Update()
    {
        Run();
        Jump();
        Climb();
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
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }
        
        if (Input.GetButtonDown("Jump")) {
            rigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void Climb() {
        if (!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladders"))) {
            rigidBody.gravityScale = gravityScale;
            animator.SetBool("Climbing", false);
            return;
        }
    
        float deltaY = Input.GetAxis("Vertical");
        var velocity = new Vector2(rigidBody.velocity.x, deltaY * climbSpeed);
        rigidBody.gravityScale = 0;
        rigidBody.velocity = velocity;

        bool isClimbing = Mathf.Abs(rigidBody.velocity.y) > Mathf.Epsilon;
        animator.SetBool("Climbing", isClimbing);
    }
}
