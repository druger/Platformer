using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    private const float MINChaseDistance = 0.2f;

    [SerializeField] private Transform enemyModelTransform;
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private float timeToWait = 5f;
    [SerializeField] private float timeToChase = 3f;
    [SerializeField] private float minDistanceToPlayer = 1.5f;

    private Rigidbody2D _rb;
    private Vector2 _leftBoundary;
    private Vector2 _rightBoundary;
    private Transform _playerPosition;
    private Vector2 _nextPoint;

    private bool _isFacingRight = true;
    private bool _isWait;
    private bool _isChasingPlayer;
    private float _waitTime;
    private float _chaseTime;
    private float _currentSpeed;

    public bool IsFacingRight => _isFacingRight;

    public void StartChasingPlayer() {
        _isChasingPlayer = true;
        _chaseTime = timeToChase;
        _currentSpeed = chaseSpeed;
    }

    void Start() {
        _playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _leftBoundary = transform.position;
        _rightBoundary = _leftBoundary + Vector2.right * walkDistance;
        _waitTime = timeToWait;
        _chaseTime = timeToChase;
        _currentSpeed = patrolSpeed;
    }

    private void Update() {
        if (_isChasingPlayer) StartChasingTimer();
        if (_isWait && !_isChasingPlayer) StartWaitingTimer();
        if (ShouldWait()) _isWait = true;
    }

    private bool ShouldWait() {
        var positionX = transform.position.x;
        var isOutOfRightBoundary = _isFacingRight && positionX >= _rightBoundary.x;
        var isOutOfLeftBoundary = !_isFacingRight && positionX <= _leftBoundary.x;
        return isOutOfRightBoundary || isOutOfLeftBoundary;
    }

    private void StartWaitingTimer() {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f) {
            _waitTime = timeToWait;
            _isWait = false;
            Flip();
        }
    }

    private void StartChasingTimer() {
        _chaseTime -= Time.deltaTime;
        if (_chaseTime < 0f) {
            _isChasingPlayer = false;
            _chaseTime = timeToChase;
            _currentSpeed = patrolSpeed;
        }
    }

    private void FixedUpdate() {
        if (_isChasingPlayer && Mathf.Abs(DistanceToPlayer()) < minDistanceToPlayer) return;

        _nextPoint = Vector2.right * (_currentSpeed * Time.fixedDeltaTime);

        if (_isChasingPlayer) ChasePlayer();

        if (!_isWait && !_isChasingPlayer) Patrol();
    }

    private float DistanceToPlayer() {
        return _playerPosition.position.x - transform.position.x;
    }

    private void Patrol() {
        if (!_isFacingRight) _nextPoint.x *= -1;
        MoveEnemy();
    }

    private void ChasePlayer() {
        var distance = DistanceToPlayer();
        if (distance < 0) _nextPoint.x *= -1;

        if (distance > MINChaseDistance && !_isFacingRight) Flip();
        else if (distance < MINChaseDistance && _isFacingRight) Flip();
        MoveEnemy();
    }

    private void MoveEnemy() {
        _rb.MovePosition((Vector2) transform.position + _nextPoint);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftBoundary, _rightBoundary);
    }

    private void Flip() {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = enemyModelTransform.localScale;
        playerScale.x *= -1;
        enemyModelTransform.localScale = playerScale;
    }
}