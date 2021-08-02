using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private int health = 100;

    private Animator _animator;

    private void Start() {
        _animator = GetComponent<Animator>();
    }

    public void ReduceHealth(int damage) {
        health -= damage;
        _animator.SetTrigger("takeDamage");
        if (health <= 0) Die();
    }

    private void Die() {
        Destroy(gameObject);
    }
}