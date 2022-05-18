using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riseHealth : MonoBehaviour
{
    [SerializeField] Player player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.getHealth()< player.getStartingHealth())
        {
            player.risesHealth();
            Destroy(this.gameObject);
        }
    }
}
