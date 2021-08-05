using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    const float SpeedXMultiiplier = 50f;

    [SerializeField] private float speedX = 1f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private AudioSource jumpSound;

    private Rigidbody2D _rb;
    private Finish _finish;
    private LeverArm _leverArm;

    private float _horizontalInput;
    private bool _onGround;
    private bool _jumping;
    private bool _facingRight = true;
    private bool _isFinish;
    private bool _isLeverArm;

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _leverArm = FindObjectOfType<LeverArm>();
    }

    private void Update() {
        _horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("speedX", Math.Abs(_horizontalInput));
        if (Input.GetKey(KeyCode.W) && _onGround) {
            _jumping = true;
            jumpSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            if (_isFinish) {
                _finish.FinishLevel();
            }

            if (_isLeverArm) {
                _leverArm.Activate();
            }
        }
    }

    private void FixedUpdate() {
        _rb.velocity = new Vector2(_horizontalInput * speedX * SpeedXMultiiplier * Time.fixedDeltaTime, _rb.velocity.y);
        if (_jumping) {
            _rb.AddForce(new Vector2(0f, 300f));
            _onGround = false;
            _jumping = false;
        }

        if (_horizontalInput > 0 && !_facingRight) {
            Flip();
        } else if (_horizontalInput < 0 && _facingRight) {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            _onGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var tmpLeverArm = other.GetComponent<LeverArm>();
        if (tmpLeverArm != null) _isLeverArm = true;

        if (other.gameObject.CompareTag("Finish")) {
            _isFinish = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        var tmpLeverArm = other.GetComponent<LeverArm>();
        if (tmpLeverArm != null) _isLeverArm = false;

        if (other.gameObject.CompareTag("Finish")) {
            _isFinish = false;
        }
    }

    private void Flip() {
        _facingRight = !_facingRight;
        Vector3 playerScale = playerModelTransform.localScale;
        playerScale.x *= -1;
        playerModelTransform.localScale = playerScale;
    }
}