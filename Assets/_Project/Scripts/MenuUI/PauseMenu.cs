using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject gameScreen;

    private void Awake()
    {
        gameScreen.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void ContinueGame()
    {
        gameScreen.SetActive(true);
        pauseScreen.SetActive(false);
    }
    public void PauseGame()
    {
        gameScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
