using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float timeToWait = 5f;

    private Rigidbody2D rb;
    private Vector2 _leftBoundary;
    private Vector2 _rightBoundary;

    private bool _isFacingRight = true;
    private bool _isWait;
    private float _waitTime;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        _leftBoundary = transform.position;
        _rightBoundary = _leftBoundary + Vector2.right * walkDistance;
        _waitTime = timeToWait;
    }

    private void Update() {
        if (_isWait) Wait();
        if (ShouldWait()) _isWait = true;
    }

    private bool ShouldWait() {
        var positionX = transform.position.x;
        var isOutOfRightBoundary = _isFacingRight && positionX >= _rightBoundary.x;
        var isOutOfLeftBoundary = !_isFacingRight && positionX <= _leftBoundary.x;
        return isOutOfRightBoundary || isOutOfLeftBoundary;
    }

    private void Wait() {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f) {
            _waitTime = timeToWait;
            _isWait = false;
            Flip();
        }
    }

    private void FixedUpdate() {
        var nextPoint = Vector2.right * (walkSpeed * Time.fixedDeltaTime);
        if (!_isFacingRight) nextPoint.x *= -1;

        if (!_isWait) {
            rb.MovePosition((Vector2) transform.position + nextPoint);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftBoundary, _rightBoundary);
    }

    private void Flip() {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}