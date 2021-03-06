using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] LoaderScreen loaderScreen;

    private void Start()
    {
        Cursor.visible = true;
    }
    public void restart()
    {
        loaderScreen.LoadScreenMenu(1);
    }
    public void mainMenu()
    {
        loaderScreen.LoadScreenMenu(0);
    }
    public void exit()
    {
        Application.Quit();
    }
}
