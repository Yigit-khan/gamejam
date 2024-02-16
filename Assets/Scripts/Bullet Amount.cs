using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletAmount : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Image[] bullets;

    private int ammo;
    private int maxAmmo = 9;
    

    private void Update()
    {
        ammo = playerMovement.BulletAmount();

        if(ammo>0)
        {
            for (int i = 0; i < ammo; i++)
            {
                bullets[i].gameObject.SetActive(true);
            }
        }
        
        if(ammo<maxAmmo)
        {
            for (int j = ammo; j <= (maxAmmo-1); j++)
            {
                bullets[j].gameObject.SetActive(false);
            }
        }
    }


}
