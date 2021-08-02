using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    [SerializeField] private int damage = 30;
    [SerializeField] private float timeToDamage = 1f;

    private float _damageTime;
    private bool isDamage = true;

    private void Start() {
        _damageTime = timeToDamage;
    }

    private void Update() {
        if (!isDamage) {
            _damageTime -= Time.deltaTime;
            if (_damageTime <= 0f) {
                isDamage = true;
                _damageTime = timeToDamage;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && isDamage) {
            playerHealth.ReduceHealth(damage);
            isDamage = false;
        }
    }
}