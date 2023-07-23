using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private string _levelName;
    [SerializeField] private string _creditsSceneName;
    [SerializeField] private string _howToPlayScene;
    [SerializeField] private string _mainMenuScene;

    public void Play()
    {
        SceneManager.LoadScene(_levelName);
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene(_howToPlayScene);
    }
    public void Credits()
    {
        SceneManager.LoadScene(_creditsSceneName);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(_levelName);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }

}
