using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScreen : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] Canvas canvas;
    public void LoadScreenMenu(int index)
    {
        canvas.sortingOrder = 2;
        StartCoroutine(LoadMenu(index));
    }

    IEnumerator LoadMenu(int index)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(index);
    }

}

