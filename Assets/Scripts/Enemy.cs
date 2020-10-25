using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigidBody;
    CapsuleCollider2D bodyCollider;
    [SerializeField] float moveSpeed = 2f;

    void Start()
    {
        bodyCollider = GetComponent<CapsuleCollider2D>();   
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }


    void onCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
         flipSprite();
    }

    private void Run() {
        bool isFacingRight = transform.localScale.x > 0;

        if (isFacingRight) {
            rigidBody.velocity = new Vector2(moveSpeed, 0f);
        } else {
            rigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private void flipSprite() {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidBody.velocity.x)), 1f);
    }
}
