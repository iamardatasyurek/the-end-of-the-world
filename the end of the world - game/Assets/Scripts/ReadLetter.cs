using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadLetter : MonoBehaviour
{
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] GameObject letterCanvas;

    [SerializeField] int letterNo;
    Image[] images;
    void Start()
    {
        images = letterCanvas.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            images[i].enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inGameCanvas.SetActive(false);
            letterCanvas.SetActive(true);
            images[letterNo].enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inGameCanvas.SetActive(true);
            letterCanvas.SetActive(false);
            images[letterNo].enabled = false;
        }
    }


}
