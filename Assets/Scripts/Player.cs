using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody;
    [SerializeField] float moveSpeed = 5f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        Run();
    }

    private void Run() {
        float deltaX = Input.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(deltaX * moveSpeed, rigidBody.velocity.y);
        
        rigidBody.velocity = velocity;

        bool isRunning = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        
        animator.SetBool("Running", isRunning);

        if (isRunning) {
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1f);
        }
    }
}
