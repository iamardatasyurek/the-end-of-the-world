using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BurnDeadBody : MonoBehaviour
{
    [SerializeField] LoaderScreen loaderScreen;
    [SerializeField] GameObject circleFire;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadBody"))
        {
            circleFire.SetActive(true);
            Destroy(other.gameObject,6.5f);
            Invoke("endGame", 9f);
               
        }
    }

    void endGame()
    {
        loaderScreen.LoadScreenMenu(0);
    }
}
