using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    
    public void PlayGame() {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}