using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int health = 100;
    [SerializeField] private Animator _animator;

    public void ReduceHealth(int damage) {
        health -= damage;
        _animator.SetTrigger("takeDamage");
        if (health <= 0) Die();
    }

    private void Die() {
        // TODO setup animation
        // _animator.SetTrigger("die");
        Destroy(gameObject);
    }
}