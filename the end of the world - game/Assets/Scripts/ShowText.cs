using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShowText : MonoBehaviour
{
    [SerializeField] string txt=" ";
    [SerializeField] TextMeshPro tm;
 
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tm.text = txt;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tm.text = " ";
        }
    }
}
