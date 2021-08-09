using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {
    [SerializeField] private GameObject messageUI;
    
    private bool _isActivated;

    public void Activate() {
        _isActivated = true;
        messageUI.SetActive(false);
    }
    
    public void FinishLevel() {
        if (_isActivated) {
            gameObject.SetActive(false);
        } else {
            messageUI.SetActive(true);
        }
    }
}