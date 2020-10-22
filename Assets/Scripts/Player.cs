using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidBody;
    [SerializeField] float moveSpeed = 5f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Run();
    }

    private void Run() {
        float deltaX = Input.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(deltaX * moveSpeed, rigidBody.velocity.y);
        
        rigidBody.velocity = velocity;
        FlipSprite();
    }

    private void FlipSprite() {
        bool playerIsMoving = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;

        if (playerIsMoving) {
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1);
        }
    }
}
