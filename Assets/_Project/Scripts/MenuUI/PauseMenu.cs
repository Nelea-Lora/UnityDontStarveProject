using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Memento;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject gameScreen;
    private GameSaveManager gameSaveManager;
    public TMP_Text saveMessageText;

    private void Awake()
    {
        gameScreen.SetActive(true);
        pauseScreen.SetActive(false);
        saveMessageText.gameObject.SetActive(false);
        gameSaveManager = FindObjectOfType<GameSaveManager>();
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
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SaveGame()
    {
        if (gameSaveManager != null)
        {
            gameSaveManager.SaveGame();
            StartCoroutine(ShowSaveMessage());
        }
        else
        {
            Debug.LogWarning("GameSaveManager not found!");
        }
    }
    private IEnumerator ShowSaveMessage()
    {
        saveMessageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f); // показать на 2 секунды
        saveMessageText.gameObject.SetActive(false);
    }
    
    public void RestartSavedGame()
    {
        if (gameSaveManager != null)
        {
            gameSaveManager.LoadLastSave();
            Debug.Log("Game restored from last save!");
        }
        else
        {
            Debug.LogWarning("GameSaveManager not found!");
        }
    }

}
