using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundedObjects : MonoBehaviour
{
    [SerializeField] foundWhat foundObject;
    [SerializeField] Player player;
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (foundObject == foundWhat.Crossbow)
            {
                player.setHaveGun(true);
                Destroy(this.gameObject, 0.2f);
            }
            else if (foundObject == foundWhat.BlueKey)
            {
                player.setHaveBlueKey(true);
                Destroy(this.gameObject, 0.2f);
            }
            else if (foundObject == foundWhat.RedKey)
            {
                player.setHaveRedKey(true);
                Destroy(this.gameObject, 0.2f);
            }
        }
    }

    enum foundWhat
    {
        RedKey, BlueKey, Crossbow
    }
}


