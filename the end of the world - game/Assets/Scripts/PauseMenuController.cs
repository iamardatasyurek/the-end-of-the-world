using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject inGame;
    [SerializeField] GameObject pauseMenu;
    public static bool isPaused = false;
    [SerializeField] LoaderScreen loaderScreen;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        inGame.SetActive(true);
        Cursor.visible = false;

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        inGame.SetActive(false);
        Cursor.visible = true;

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;

        loaderScreen.LoadScreenMenu(0);
    }
    public void Exit()
    {
        Application.Quit();
    }

}
