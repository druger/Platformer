using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {
    [SerializeField] private GameObject currentHitObject;
    [SerializeField] private float circleRadius;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;

    private Vector2 _origin;
    private Vector2 _direction;
    private float _currentHitDistance;

    private EnemyController _enemyController;

    void Start() {
        _enemyController = GetComponent<EnemyController>();
    }

    void Update() {
        _origin = transform.position;
        _direction = _enemyController.IsFacingRight ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.CircleCast(_origin, circleRadius, _direction, maxDistance, layerMask);
        if (hit) {
            currentHitObject = hit.transform.gameObject;
            _currentHitDistance = hit.distance;
            if (currentHitObject.CompareTag("Player")) {
                _enemyController.StartChasingPlayer();
            }
        } else {
            currentHitObject = null;
            _currentHitDistance = maxDistance;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_origin, _origin + _direction * _currentHitDistance);
        Gizmos.DrawSphere(_origin + _direction * _currentHitDistance, circleRadius);
    }
}