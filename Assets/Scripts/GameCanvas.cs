using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour {
    [SerializeField] private GameObject pauseMenuCanvas;

    public void ShowPauseMenu() {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}