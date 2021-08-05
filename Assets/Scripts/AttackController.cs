using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource attackSound;

    private bool _isAttack;

    public bool IsAttack => _isAttack;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            _isAttack = true;
            animator.SetTrigger("attack");
            attackSound.Play();
        }
    }

    public void FinishAttack() {
        _isAttack = false;
    }
}