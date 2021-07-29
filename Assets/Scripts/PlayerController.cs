using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    const float speedXMultiiplier = 50f;

    [SerializeField] private float speedX = 1f;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private Finish finish;
    private LeverArm leverArm;

    private float horizontalInput;
    private bool onGround;
    private bool jumping;
    private bool facingRight = true;
    private bool isFinish;
    private bool isLeverArm;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        leverArm = FindObjectOfType<LeverArm>();
    }

    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("speedX", Math.Abs(horizontalInput));
        if (Input.GetKey(KeyCode.W) && onGround) {
            jumping = true;
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            if (isFinish) {
                finish.FinishLevel();
            }

            if (isLeverArm) {
                leverArm.Activate();
            }
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

    private void OnTriggerEnter2D(Collider2D other) {
        var tmpLeverArm = other.GetComponent<LeverArm>();
        if (tmpLeverArm != null) isLeverArm = true;

        if (other.gameObject.CompareTag("Finish")) {
            isFinish = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        var tmpLeverArm = other.GetComponent<LeverArm>();
        if (tmpLeverArm != null) isLeverArm = false;

        if (other.gameObject.CompareTag("Finish")) {
            isFinish = false;
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}