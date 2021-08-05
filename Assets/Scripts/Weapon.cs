using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    [SerializeField] private int damage = 20;
    [SerializeField] private AudioSource hitSound;

    private AttackController _attackController;
    
    void Start() {
        _attackController = transform.root.GetComponent<AttackController>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        var enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null && _attackController.IsAttack) {
            enemyHealth.ReduceHealth(damage);
            hitSound.Play();
        }
    }
}