using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BurnDeadBody : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadBody"))
        {
            Destroy(other.gameObject,2f);
            Invoke("endGame", 4f);
               
        }
    }

    void endGame()
    {
        SceneManager.LoadScene("Scene_A");
    }
}
