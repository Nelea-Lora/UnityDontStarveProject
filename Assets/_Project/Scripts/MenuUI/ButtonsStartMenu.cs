using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsStartMenu : MonoBehaviour
{
    public GameObject _startScreen;
    public bool IsStarted { get; private set; }

    public void StartGame()
    {
        IsStarted = true;
        _startScreen.SetActive(false);
        SceneManager.LoadScene(1);
    }
    public void StartGameOn()
    {
        _startScreen.SetActive(true);
    }
}
