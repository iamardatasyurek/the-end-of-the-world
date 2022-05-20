using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] LoaderScreen loaderScreen;


    public void playGame()
    {
        loaderScreen.LoadScreenMenu(1);
    }
    public void exitGame()
    {
        Application.Quit(); 
    }
}
