using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] LoaderScreen loaderScreen;

    private void Start()
    {
        Cursor.visible = true;
    }
    public void playGame()
    {
        loaderScreen.LoadScreenMenu(1);
    }
    public void exitGame()
    {
        Application.Quit(); 
    }
}
