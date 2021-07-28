using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    const float speedXMultiiplier = 50f;

    [SerializeField] private float speedX = 1f;
    private Rigidbody2D rb;
    private float horizontalInput;
    private bool onGround;
    private bool jumping;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.W) && onGround) {
            jumping = true;
        }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(horizontalInput * speedX * speedXMultiiplier * Time.fixedDeltaTime, rb.velocity.y);
        if (jumping) {
            rb.AddForce(new Vector2(0f, 300f));
            onGround = false;
            jumping = false;
        }

        if (horizontalInput > 0 && !facingRight) {
            Flip();
        } else if (horizontalInput < 0 && facingRight) {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            onGround = true;
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}