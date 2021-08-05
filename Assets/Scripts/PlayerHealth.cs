using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int totalHealth = 100;
    [SerializeField] private Animator _animator;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private AudioSource hurtSound;

    private int _health;

    private void Start() {
        _health = totalHealth;
        InitHealth();
    }
    
    public void ReduceHealth(int damage) {
        _health -= damage;
        InitHealth();
        _animator.SetTrigger("takeDamage");
        hurtSound.Play();
        if (_health <= 0) Die();
    }

    private void InitHealth() {
        healthSlider.value = (float) _health / totalHealth;
    }

    private void Die() {
        // TODO setup animation
        // _animator.SetTrigger("die");
        gameOverCanvas.SetActive(true);
        Destroy(gameObject);
    }

    public bool IsDead() {
        return _health <= 0;
    }
}