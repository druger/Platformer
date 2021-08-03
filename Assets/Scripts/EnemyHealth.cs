using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private int totalHealth = 100;
    [SerializeField] private Slider healthSlider;

    private Animator _animator;
    private int _health;

    private void Start() {
        _animator = GetComponent<Animator>();
        _health = totalHealth;
        InitHealth();
    }

    public void ReduceHealth(int damage) {
        _health -= damage;
        InitHealth();
        _animator.SetTrigger("takeDamage");
        if (_health <= 0) Die();
    }
    
    private void InitHealth() {
        healthSlider.value = (float) _health / totalHealth;
    }

    private void Die() {
        Destroy(gameObject);
    }
}