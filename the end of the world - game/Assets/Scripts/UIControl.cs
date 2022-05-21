using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControl : MonoBehaviour
{
    [SerializeField] Player player;
    Gun gun;

    [SerializeField] TMP_Text health;
    [SerializeField] TMP_Text ammo;

    //[SerializeField] Image crossbow;
    [SerializeField] GameObject crossbow;
    [SerializeField] Image redKey;
    [SerializeField] Image blueKey;

    [SerializeField] GameObject arrow;
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();       

        updateValues();

        //crossbow.enabled = false;
        crossbow.SetActive(false);
        redKey.enabled = false;
        blueKey.enabled = false;

        arrow.SetActive(false);
    }
    void Update()
    {
        if (player.getHaveGun() == true) {
            arrow.SetActive(true);
            gun = player.GetComponentInChildren<Gun>();
        }

        updateValues();
        activatedImages();
    }

    void updateValues()
    {
        health.SetText(player.getHealth().ToString());
        if (player.getHaveGun() == true)
            ammo.SetText(gun.getAmmo().ToString());
        
    }

    void activatedImages()
    {
        if(player.getHaveGun() == true)
            //crossbow.enabled = true;
            crossbow.SetActive(true);
        if (player.getHaveRedKey() == true)
            redKey.enabled = true;
        if(player.getHaveBlueKey() == true)
            blueKey.enabled = true;
    }
}
