using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : StateMachineBehaviour {
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var attackController = animator.transform.root.GetComponent<AttackController>();
        attackController.FinishAttack();
    }
}