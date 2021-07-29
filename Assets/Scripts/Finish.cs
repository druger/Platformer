using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {
    private bool isActivated;

    public void Activate() {
        isActivated = true;
    }
    
    public void FinishLevel() {
        if (isActivated) {
            gameObject.SetActive(false);
        }
    }
}